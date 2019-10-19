using AutoMapper;
using ILConfessions.API.Contracts.V1.Requests.Queries;
using ILConfessions.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ILConfessions.API.Mapping
{
    public class RequestToModel : Profile
    {
        public RequestToModel()
        {
            CreateMap<PaginationQuery, PaginationFilter>();
        }
    }
}
