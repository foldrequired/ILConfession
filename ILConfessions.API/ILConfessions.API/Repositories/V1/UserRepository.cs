using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ILConfessions.API.Data;
using ILConfessions.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ILConfessions.API.Repositories.V1
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public void Add<T>(T entity) where T : class
        {
            _db.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _db.Remove(entity);
        }

        public async Task<ApplicationUser> GetUser(string id)
        {
            var user = await _db.Users.Include(p => p.Photos).FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public async Task<IEnumerable<ApplicationUser>> GetUsers()
        {
            var users = await _db.Users.Include(p => p.Photos).ToListAsync();

            return users;
        }

        public async Task<bool> SaveAll()
        {
            return await _db.SaveChangesAsync() > 0;
        }
    }
}