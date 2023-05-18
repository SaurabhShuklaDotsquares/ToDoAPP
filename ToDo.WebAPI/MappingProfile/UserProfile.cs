using AutoMapper;
using ToDo.Core.Entites;
using ToDo.WebAPI.DTO;

namespace ToDo.WebAPI.MappingProfile
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();

            CreateMap<UserRegistrationDto, User>();
        }
    }
}
