using AutoMapper;
using ILConfessions.API.Contracts.V1.Requests;
using ILConfessions.API.Contracts.V1.Responses;
using ILConfessions.API.Helpers;
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

            CreateMap<ApplicationUser, UserListResponse>()
                .ForMember(dest => dest.Age, 
                           options => options.MapFrom(src => src.DateOfBirth.CalculateAge()));

            CreateMap<UpdateUserProfileRequest, ApplicationUser>();

            CreateMap<Photo, GetPhotoRequest>();

            CreateMap<CreatePhotoRequest, Photo>();

            CreateMap<UserRegisterRequest, ApplicationUser>();

            //CreateMap<UserAuthRequest, ApplicationUser>();
        }
    }
}
