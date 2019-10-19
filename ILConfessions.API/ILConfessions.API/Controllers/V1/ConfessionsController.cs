using AutoMapper;
using ILConfessions.API.Contracts.V1.Requests;
using ILConfessions.API.Contracts.V1.Requests.Queries;
using ILConfessions.API.Contracts.V1.Responses;
using ILConfessions.API.ExtensionMethods;
using ILConfessions.API.Helpers;
using ILConfessions.API.MagicStringHandlers.V1;
using ILConfessions.API.Models;
using ILConfessions.API.Repositories.V1;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ILConfessions.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] //Bearer
    [Produces("application/json")]
    public class ConfessionsController : Controller
    {
        #region Private Readonly Properties

        private readonly IConfessionRepository _repo;
        private readonly IMapper _mapper;
        private readonly IUriRepository _uriRepository;

        #endregion

        #region CTOR
        
        public ConfessionsController(IConfessionRepository repo, IMapper mapper, IUriRepository uriRepository)
        {
            _repo = repo;
            _mapper = mapper;
            _uriRepository = uriRepository;
        }

        #endregion

        //[AllowAnonymous]
        /// <summary>
        /// Returns all the Confessions
        /// </summary>
        /// <response code="200">Returns all the Confessions</response>
        [HttpGet(ApiRoutes.Confessions.GetConfessions)]
        public async Task<IActionResult> Get([FromQuery]PaginationQuery paginationQuery)
        {
            var pagination = _mapper.Map<PaginationFilter>(paginationQuery);

            var confessions = await _repo.GetConfessionsAsync(pagination);

            var confessionsResponse = _mapper.Map<List<ConfessionResponse>>(confessions);

            if (pagination == null || pagination.PageNumber < 1 || pagination.PageSize < 1)
            {
                return Ok(new PagedResponse<ConfessionResponse>(confessionsResponse));
            }

            var paginationResponse = PaginationHelpers.CreatePaginationResponse(_uriRepository, pagination, confessionsResponse);

            return Ok(paginationResponse);
        }

        [HttpGet(ApiRoutes.Confessions.Get)]
        public async Task<IActionResult> GetSingle([FromRoute]int confessionId)
        {
            var confession = await _repo.GetConfessionByIdAsync(confessionId);

            if (confession == null)
            {
                return NotFound();
            }

            return Ok(new ConfessionResponse
            {
                Id = confession.Id,
                Title = confession.Title,
                Description = confession.Description,
                UserId = confession.UserId,
                CreatedDate = confession.CreatedDate
            });
        }

        /// <summary>
        /// Creates a new Confession
        /// </summary>
        /// <response code="201">New Confession has been created</response>
        /// <response code="400">Unable to create a new confession</response>
        [HttpPost(ApiRoutes.Confessions.Create)]
        public async Task<IActionResult> Create([FromBody] CreateConfessionRequest confessionDto)
        {
            var confession = new Confession { Title = confessionDto.Title, Description = confessionDto.Description, UserId = HttpContext.GetUserId() };

            var createdConfession = await _repo.CreateConfessionAsync(confession);
            if (!createdConfession)
            {
                return BadRequest(new { Error = "Unable to create a new confession" });
            }

            // Get the current Url of the browser
            //var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";

            // Get the complete url with the new confession object and the Id of it -- localhost:5000://confession/{the new Id}
            //var locationUri = baseUrl + "/" + ApiRoutes.Confessions.Get.Replace("{confessionId}", confession.Id.ToString());

            var locationUri = _uriRepository.GetConfessionUri(confession.Id.ToString());

            var response = new ConfessionResponse
            {
                Id = confession.Id,
                Title = confession.Title,
                Description = confession.Description,
                UserId = confession.UserId,
                CreatedDate = confession.CreatedDate
            };

            return Created(locationUri, response);
        }

        [HttpPut(ApiRoutes.Confessions.Update)]
        public async Task<IActionResult> Update([FromRoute]int confessionId, [FromBody] UpdateConfessionRequest confessionDto)
        {
            var userOwnsConfession = await _repo.UserOwnsConfessionAsync(confessionId, HttpContext.GetUserId());

            if (!userOwnsConfession)
                return BadRequest(new { Error = "You can't access this confession because you don't own it" });

            //var confession = new Confession
            //{
            //    Id = confessionId,
            //    Title = confessionDto.Title,
            //    Description = confessionDto.Description,
            //};

            var confession = await _repo.GetConfessionByIdAsync(confessionId);

            confession.Title = confessionDto.Title;

            var update = await _repo.UpdatConfessionAsync(confession);

            if (update)
            {
                return Ok(new ConfessionResponse
                {
                    Id = confession.Id,
                    Title = confession.Title,
                    Description = confession.Description,
                    UserId = confession.UserId,
                    CreatedDate = confession.CreatedDate
                });
            }

            return NotFound();
        }

        [HttpDelete(ApiRoutes.Confessions.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int confessionId)
        {
            var userOwnsConfession = await _repo.UserOwnsConfessionAsync(confessionId, HttpContext.GetUserId());

            if (!userOwnsConfession)
                return BadRequest(new { Error = "You can't access this confession because you don't own it" });

            var delete = await _repo.DeleteConfessionAsync(confessionId);

            if (delete)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
