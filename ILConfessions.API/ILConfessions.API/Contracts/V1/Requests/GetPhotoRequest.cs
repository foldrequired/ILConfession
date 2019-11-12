using System;

namespace ILConfessions.API.Contracts.V1.Requests
{
    public class GetPhotoRequest
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public string Description { get; set; }

        public DateTime DateUploaded { get; set; }
        
        public string IsMain { get; set; }

        public string PublicId { get; set; }

        public GetPhotoRequest()
        {
            DateUploaded = DateTime.Now;
        }
    }
}