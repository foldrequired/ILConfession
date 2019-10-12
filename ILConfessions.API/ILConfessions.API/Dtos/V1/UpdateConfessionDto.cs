using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ILConfessions.API.Dtos.V1
{
    public class UpdateConfessionDto
    {
        public string Title { get; set; }

        public string Description { get; set; }
    }
}
