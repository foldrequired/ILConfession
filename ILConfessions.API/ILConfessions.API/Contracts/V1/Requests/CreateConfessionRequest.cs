using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ILConfessions.API.Contracts.V1.Requests
{
    public class CreateConfessionRequest
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string City { get; set; }
    }
}
