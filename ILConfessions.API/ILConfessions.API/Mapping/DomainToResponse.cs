using AutoMapper;
using ILConfessions.API.Contracts.V1.Responses;
using ILConfessions.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ILConfessions.API.Mapping
{
    public class DomainToResponse : Profile
    {
        public DomainToResponse()
        {
            CreateMap<Confession, ConfessionResponse>();
        }
    }
}
