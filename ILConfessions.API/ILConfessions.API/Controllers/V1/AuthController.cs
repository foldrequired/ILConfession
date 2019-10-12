using ILConfessions.API.Repositories.V1;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ILConfessions.API.Controllers.V1
{
    public class AuthController : Controller
    {
        #region Private Readonly Properties

        private readonly IAuthRepository _repo;

        #endregion

        #region CTOR

        public AuthController(IAuthRepository repo)
        {
            _repo = repo;
        }

        #endregion
    }
}
