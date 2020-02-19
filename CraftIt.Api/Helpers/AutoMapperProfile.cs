using AutoMapper;
using CraftIt.Api.Models;

namespace WebApi.Helpers
{
    /// <summary>Class <c>AutoMapperProfile</c>Class required by Automapper used to map a class and domain transfer object.</summary>
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<User, UserRegistrationDto>();
            CreateMap<UserRegistrationDto, User>();

        }
    }
}