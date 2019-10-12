using ILConfessions.API.Dtos.V1;
using ILConfessions.API.MagicStringHandlers.V1;
using ILConfessions.API.Models;
using ILConfessions.API.Repositories.V1;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ILConfessions.API.Controllers
{
    [Produces("application/json")]
    public class ConfessionsController : Controller
    {
        #region Private Readonly Properties

        private readonly IConfessionRepository _repo;

        #endregion

        #region CTOR
        
        public ConfessionsController(IConfessionRepository repo)
        {
            _repo = repo;
        }

        #endregion

        [HttpGet(ApiRoutes.Confessions.GetConfessions)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _repo.GetConfessionsAsync());
        }

        [HttpGet(ApiRoutes.Confessions.Get)]
        public async Task<IActionResult> GetSingle([FromRoute]int confessionId)
        {
            var confession = await _repo.GetConfessionByIdAsync(confessionId);

            if (confession == null)
            {
                return NotFound();
            }

            return Ok(confession);
        }

        [HttpPost(ApiRoutes.Confessions.Create)]
        public async Task<IActionResult> Create([FromBody] CreateConfessionReqDto confessionDto)
        {
            var confession = new Confession { Title = confessionDto.Title, Description = confessionDto.Description };

            await _repo.CreateConfessionAsync(confession);

            // Get the current Url of the browser
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";

            // Get the complete url with the new confession object and the Id of it -- localhost:5000://confession/{the new Id}
            var locationUrl = baseUrl + "/" + ApiRoutes.Confessions.Get.Replace("{confessionId}", confession.Id.ToString());

            var response = new CreateConfessionResDto
            {
                Id = confession.Id,
                Title = confession.Title,
                Description = confession.Description,
                CreatedDate = confession.CreatedDate
            };

            return Created(locationUrl, response);
        }

        [HttpPut(ApiRoutes.Confessions.Update)]
        public async Task<IActionResult> Update([FromRoute]int confessionId, [FromBody] UpdateConfessionDto confessionDto)
        {
            var confession = new Confession
            {
                Id = confessionId,
                Title = confessionDto.Title,
                Description = confessionDto.Description,
            };

            var update = await _repo.UpdatConfessionAsync(confession);

            if (update)
            {
                return Ok(confession);
            }

            return NotFound();
        }

        [HttpDelete(ApiRoutes.Confessions.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int confessionId)
        {
            var delete = await _repo.DeleteConfessionAsync(confessionId);

            if (delete)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
