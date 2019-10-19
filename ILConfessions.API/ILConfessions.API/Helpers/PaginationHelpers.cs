using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ILConfessions.API.Contracts.V1.Requests.Queries;
using ILConfessions.API.Contracts.V1.Responses;
using ILConfessions.API.Models;
using ILConfessions.API.Repositories.V1;

namespace ILConfessions.API.Helpers
{
    public class PaginationHelpers
    {
        public static PagedResponse<T> CreatePaginationResponse<T>(IUriRepository uriRepository, PaginationFilter pagination, List<T> response)
        {
            var nextPage = pagination.PageNumber >= 1 ? uriRepository
                .GetAllConfessionsUri(new PaginationQuery(pagination.PageNumber + 1, pagination.PageSize)).ToString() : null;

            var previousPage = pagination.PageNumber - 1 >= 1 ? uriRepository
                .GetAllConfessionsUri(new PaginationQuery(pagination.PageNumber - 1, pagination.PageSize)).ToString() : null;

            // Solution without Mapper
            /*var confessionResponse = confessions.Select(conf => new ConfessionResponse
            {
                Id = conf.Id,
                Title = conf.Title,
                Description = conf.Description,
                UserId = conf.UserId,
                CreatedDate = conf.CreatedDate
            }).ToList();*/

            // Mapper
            /*var confessionResponse = _mapper.Map<List<ConfessionResponse>>(confessions);

            return Ok(new PagedResponse<ConfessionResponse>(confessionResponse));*/

           return new PagedResponse<T>
            {
                Data = response,
                PageNumber = pagination.PageNumber >= 1 ? pagination.PageNumber : (int?)null,
                PageSize = pagination.PageSize >= 1 ? pagination.PageSize : (int?)null,
                NextPage = response.Any() ? nextPage : null,
                PreviousPage = previousPage
            };
        }
    }
}
