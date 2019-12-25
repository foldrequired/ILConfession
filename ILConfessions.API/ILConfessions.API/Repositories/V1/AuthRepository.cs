using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ILConfessions.API.Contracts.V1.Requests;
using ILConfessions.API.Data;
using ILConfessions.API.Models;
using ILConfessions.API.Settings.JwtSettings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ILConfessions.API.Repositories.V1
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JwtOptions _jwtOptions;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _db;

        public AuthRepository(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            JwtOptions jwtOptions,
            TokenValidationParameters tokenValidationParameters,
            IMapper mapper,
            ApplicationDbContext db
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtOptions = jwtOptions;
            _tokenValidationParameters = tokenValidationParameters;
            _mapper = mapper;
            _db = db;
        }

        public async Task<AuthenticationResult> RegisterAsync(UserRegisterRequest req)
        { 
            var userExist = await _userManager.FindByEmailAsync(req.Email);

            var newAccount = new ApplicationUser
            {
                Email = req.Email,
                UserName = req.Email,
                Gender = req.Gender,
                KnownAs = req.KnownAs,
                DateOfBirth = req.DateOfBirth,
                Country = req.Country,
                Created = req.Created,
                LastActive = req.LastActive
            };

            var createdAccount = await _userManager.CreateAsync(newAccount, req.Password);

            if (!createdAccount.Succeeded)
                return new AuthenticationResult
                {
                    Errors = createdAccount.Errors.Select(e => e.Description)
                };

            return await AuthenticationResultAsync(newAccount);
        }

        public async Task<AuthenticationResult> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return new AuthenticationResult
                {
                    Errors = new[] { "User does not exist" }
                };

            var userValidPassword = await _userManager.CheckPasswordAsync(user, password);

            var userForReturn = await _db.ApplicationUsers.FirstOrDefaultAsync(u => u.UserName == user.UserName);

            if (!userValidPassword)
                return new AuthenticationResult
                {
                    Errors = new[] { "The Email & Password combination are wrong" }
                };

            return await AuthenticationResultAsync(user);
        }

        public async Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken)
        {
            var validatedToken = PrincipalFromToken(token);

            if (validatedToken == null)
                return new AuthenticationResult { Errors = new[] { "Invalid Token" } };

            var expiryDate = long.Parse(validatedToken.Claims.Single(c => c.Type == JwtRegisteredClaimNames.Exp).Value);

            var expiryDateTimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(expiryDate);

            if (expiryDateTimeUtc > DateTime.Now)
            {
                return new AuthenticationResult { Errors = new[] { "Token hasn't expired yet" } };
            }

            var jti = validatedToken.Claims.Single(c => c.Type == JwtRegisteredClaimNames.Jti).Value;

            var storedRefreshToken = await _db.RefreshTokens.SingleOrDefaultAsync(r => r.Token == refreshToken);

            if (storedRefreshToken == null)
                return new AuthenticationResult { Errors = new[] { "Refresh Token does not exist" } };

            if (DateTime.UtcNow > storedRefreshToken.ExpiryDate)
                return new AuthenticationResult { Errors = new[] { "Refresh Token has been expired" } };

            if (storedRefreshToken.Invalidated)
                return new AuthenticationResult { Errors = new[] { "Refresh Token has been invalidated" } };

            if (storedRefreshToken.IsUsed)
                return new AuthenticationResult { Errors = new[] { "Refresh Token has been used" } };

            if (storedRefreshToken.JwtId != jti)
                return new AuthenticationResult { Errors = new[] { "Refresh Token does not match this JWT" } };

            storedRefreshToken.IsUsed = true;
            _db.RefreshTokens.Update(storedRefreshToken);
            await _db.SaveChangesAsync();

            var user = await _userManager.FindByIdAsync(validatedToken.Claims.Single(c => c.Type == "id").Value);

            return await AuthenticationResultAsync(user);
        }

        #region Helpers

        private ClaimsPrincipal PrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var principal = tokenHandler.ValidateToken(token, _tokenValidationParameters, out var validatedToken);
                if (!IsJwtWithValidSecurityAlgorithm(validatedToken))
                    return null;

                return principal;
            }
            catch
            {
                return null;
            }
        }

        private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
        {
            return (validatedToken is JwtSecurityToken jwtSecurityToken) && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase);
        }

        private async Task<AuthenticationResult> AuthenticationResultAsync(ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtOptions.TokenSecret);

            var claims = new List<Claim>
            {
                    new Claim(JwtRegisteredClaimNames.Sub, user.KnownAs),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim("id", user.Id)
            };

            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
                var role = await _roleManager.FindByNameAsync(userRole);
                if (role == null) continue;
                var roleClaims = await _roleManager.GetClaimsAsync(role);

                foreach (var roleClaim in roleClaims)
                {
                    if (claims.Contains(roleClaim))
                        continue;

                    claims.Add(roleClaim);
                }
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(_jwtOptions.TokenLifecycle),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var refreshToken = new RefreshToken
            {
                JwtId = token.Id,
                UserId = user.Id,
                CreatedDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMonths(6)
            };

            await _db.RefreshTokens.AddAsync(refreshToken);
            await _db.SaveChangesAsync();

            return new AuthenticationResult
            {
                Success = true,
                Token = tokenHandler.WriteToken(token),
                RefreshToken = refreshToken.Token,
            };
        }

        #endregion
    }
}
