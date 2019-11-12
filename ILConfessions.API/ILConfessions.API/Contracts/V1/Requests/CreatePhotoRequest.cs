using System;
using Microsoft.AspNetCore.Http;

namespace ILConfessions.API.Contracts.V1.Requests
{
    public class CreatePhotoRequest
    {
        public string Url { get; set; }

        public IFormFile File { get; set; }

        public string Description { get; set; }

        public DateTime DateUploaded { get; set; }
        
        public string PublicId { get; set; }

        public CreatePhotoRequest()
        {
            DateUploaded = DateTime.Now;
        }
    }
}