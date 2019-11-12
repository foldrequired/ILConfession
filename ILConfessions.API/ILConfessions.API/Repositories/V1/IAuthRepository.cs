using ILConfessions.API.Contracts.V1.Requests;
using ILConfessions.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ILConfessions.API.Repositories.V1
{
    public interface IAuthRepository
    {
        Task<AuthenticationResult> RegisterAsync(UserRegisterRequest req);
        Task<AuthenticationResult> LoginAsync(string email, string password);
        Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken);
    }
}
