using AutoMapper;
using ILConfessions.API.Contracts.V1.Requests;
using ILConfessions.API.Contracts.V1.Responses;
using ILConfessions.API.MagicStringHandlers.V1;
using ILConfessions.API.Models;
using ILConfessions.API.Repositories.V1;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ILConfessions.API.Controllers.V1
{
    //[ApiController]
    public class AuthController : Controller
    {
        #region Private Readonly Properties

        private readonly IAuthRepository _repo;
        private readonly IMapper _mapper;

        #endregion

        #region CTOR

        public AuthController(IAuthRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        #endregion

        [HttpPost(ApiRoutes.Auth.Register)]
        public async Task<IActionResult> Register([FromBody]UserRegisterRequest req)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthFailResponse
                {
                    Errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage)
                });
            }

            //var userCreateRes = _mapper.Map<ApplicationUser>(req);

            var authRes = await _repo.RegisterAsync(req);

            if (!authRes.Success)
            {
                return BadRequest(new AuthFailResponse
                {
                    Errors = authRes.Errors
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Token = authRes.Token,
                RefreshToken = authRes.RefreshToken
            });
        }

        [HttpPost(ApiRoutes.Auth.Login)]
        public async Task<IActionResult> Login([FromBody]UserLoginRequest req)
        {
            var authRes = await _repo.LoginAsync(req.Email, req.Password);

            if (!authRes.Success)
            {
                return BadRequest(new AuthFailResponse
                {
                    Errors = authRes.Errors
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Token = authRes.Token,
                RefreshToken = authRes.RefreshToken
            });
        }

        [HttpPost(ApiRoutes.Auth.Refresh)]
        public async Task<IActionResult> Refresh([FromBody]RefreshTokenRequest req)
        {
            var authRes = await _repo.RefreshTokenAsync(req.Token, req.RefreshToken);

            if (!authRes.Success)
            {
                return BadRequest(new AuthFailResponse
                {
                    Errors = authRes.Errors
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Token = authRes.Token,
                RefreshToken = authRes.RefreshToken
            });
        }
    }
}
