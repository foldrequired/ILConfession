using ILConfessions.API.Contracts.V1.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ILConfessions.API.Repositories.V1
{
    public interface IUriRepository
    {
        Uri GetConfessionUri(string confessionId);

        Uri GetAllConfessionsUri(PaginationQuery pagination = null);

        Uri GetPhotoUri(string userId, int photoId);
    }
}
