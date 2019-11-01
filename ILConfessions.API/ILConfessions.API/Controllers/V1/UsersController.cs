using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ILConfessions.API.Contracts.V1.Responses;
using ILConfessions.API.MagicStringHandlers.V1;
using ILConfessions.API.Repositories.V1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ILConfessions.API.Controllers.V1
{
    [Authorize]
    [Produces("application/json")]
    public class UsersController : Controller
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;
        public UsersController(IUserRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet(ApiRoutes.Users.GetUsers)]
        public async Task<IActionResult> Get()
        {
            var users = await _repo.GetUsers();

            if (users == null)
            {
                return NotFound();
            }

            var usersResponse = _mapper.Map<IEnumerable<UserListResponse>>(users);

            return Ok(usersResponse);
        }

        [HttpGet(ApiRoutes.Users.Get)]
        public async Task<IActionResult> GetSingle([FromRoute]string userId)
        {
            var user = await _repo.GetUser(userId);

            if (user == null)
            {
                return NotFound();  
            }

            var userResponse = _mapper.Map<UserListResponse>(user);

            return Ok(userResponse);
        }
    }
}