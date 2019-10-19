using ILConfessions.API.Contracts.V1.Responses;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ILConfessions.API.Settings.SwaggerSettings.SwaggerExamples.Responses
{
    public class ConfessionResponseExample : IExamplesProvider<ConfessionResponse>
    {
        public ConfessionResponse GetExamples()
        {
            return new ConfessionResponse
            {
                Title = "new title"
            };
        }
    }
}
