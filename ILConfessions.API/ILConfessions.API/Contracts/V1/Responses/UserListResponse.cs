using System;
using System.Collections.Generic;
using ILConfessions.API.Contracts.V1.Requests;

namespace ILConfessions.API.Contracts.V1.Responses
{
    public class UserListResponse
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string KnownAs { get; set; }

        public int Age { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastActive { get; set; }

        public string Gender { get; set; }

        public string Country { get; set; }

        public string PhotoUrl { get; set; }

        public ICollection<GetPhotoRequest> Photos { get; set; }
    }
}