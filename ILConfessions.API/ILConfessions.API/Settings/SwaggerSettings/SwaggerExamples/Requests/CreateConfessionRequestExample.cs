using ILConfessions.API.Contracts.V1.Requests;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ILConfessions.API.Settings.SwaggerSettings.SwaggerExamples.Requests
{
    public class CreateConfessionRequestExample : IExamplesProvider<CreateConfessionRequest>
    {
        public CreateConfessionRequest GetExamples()
        {
            return new CreateConfessionRequest
            {
                Title = "new title",
                Description = "new description",
                City = "all cities"
            };
        }
    }
}
