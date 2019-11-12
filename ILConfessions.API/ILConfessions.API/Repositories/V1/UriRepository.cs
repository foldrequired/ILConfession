using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ILConfessions.API.Contracts.V1.Requests.Queries;
using ILConfessions.API.MagicStringHandlers.V1;
using Microsoft.AspNetCore.WebUtilities;

namespace ILConfessions.API.Repositories.V1
{
    public class UriRepository : IUriRepository
    {
        private readonly string _baseUri;
        public UriRepository(string baseUri)
        {
            _baseUri = baseUri;
        }

        public Uri GetConfessionUri(string confessionId)
        {
            return new Uri(_baseUri + ApiRoutes.Confessions.Get.Replace("{confessionId}", confessionId));
        }

        public Uri GetAllConfessionsUri(PaginationQuery pagination = null)
        {
            var uri = new Uri(_baseUri);

            if (pagination == null)
                return uri;

            var modifiedUri = QueryHelpers.AddQueryString(_baseUri, "pageNumber", pagination.PageNumber.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", pagination.PageSize.ToString());

            return new Uri(modifiedUri);
        }

        public Uri GetPhotoUri(string userId, int photoId)
        {
            return new Uri(_baseUri + ApiRoutes.Photos.Get.Replace("{userId}", userId).Replace("{photoId}", photoId.ToString()));
        }
    }
}
