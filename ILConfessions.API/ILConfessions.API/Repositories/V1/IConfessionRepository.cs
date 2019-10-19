using ILConfessions.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ILConfessions.API.Repositories.V1
{
    public interface IConfessionRepository
    {
        Task<List<Confession>> GetConfessionsAsync(PaginationFilter paginationFilter = null);

        Task<Confession> GetConfessionByIdAsync(int id);

        Task<bool> CreateConfessionAsync(Confession confessionToCreate);

        Task<bool> UpdatConfessionAsync(Confession confessionToUpdate);

        Task<bool> DeleteConfessionAsync(int id);

        Task<bool> UserOwnsConfessionAsync(int confessionId, string getUserId);
    }
}
