using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ILConfessions.API.Models;

namespace ILConfessions.API.Repositories.V1
{
    public interface IUserRepository
    {
        void Add<T>(T entity) where T: class;
        void Delete<T>(T entity) where T: class;
        Task<bool> SaveAll();
        Task<IEnumerable<ApplicationUser>> GetUsers();
        Task<ApplicationUser> GetUser(string id);
        Task<Photo> GetPhoto(int id);
    }
}