using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ILConfessions.API.Models;

namespace ILConfessions.API.Contracts.V1.Responses
{
    public class ConfessionResponse
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string City { get; set; }
        
        //User Id
        public string UserId { get; set; }

        //User KnownAs
        public string KnownAs { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
