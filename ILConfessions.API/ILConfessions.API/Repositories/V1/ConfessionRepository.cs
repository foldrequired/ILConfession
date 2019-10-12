using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ILConfessions.API.Data;
using ILConfessions.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ILConfessions.API.Repositories.V1
{
    public class ConfessionRepository : IConfessionRepository
    {
        #region Private Properties

        private readonly ApplicationDbContext _db;

        #endregion

        #region CTOR

        public ConfessionRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        #endregion

        public async Task<List<Confession>> GetConfessionsAsync()
        {
            return await _db.Confessions.ToListAsync();
        }

        public async Task<Confession> GetConfessionByIdAsync(int id)
        {
            return await _db.Confessions.SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> CreateConfessionAsync(Confession confession)
        {
            confession.CreatedDate = DateTime.Now;
            await _db.Confessions.AddAsync(confession);
            var create = await _db.SaveChangesAsync();

            return create > 0;
        }

        public async Task<bool> UpdatConfessionAsync(Confession confessionToUpdate)
        {
            //var conf = await GetConfessionByIdAsync(confessionToUpdate.Id);
            //confessionToUpdate.CreatedDate = conf.CreatedDate;

            _db.Confessions.Update(confessionToUpdate);
            var update = await _db.SaveChangesAsync();

            return update > 0;
        }

        public async Task<bool> DeleteConfessionAsync(int id)
        {
            var confession = await GetConfessionByIdAsync(id);

            if (confession == null)
            {
                return false;
            }

            _db.Confessions.Remove(confession);
            var delete = await _db.SaveChangesAsync();

            return delete > 0;
        }
    }
}
