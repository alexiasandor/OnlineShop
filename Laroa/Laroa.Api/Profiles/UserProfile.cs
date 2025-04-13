using AutoMapper;
using Laroa.Api.Dtos;
using Laroa.Domain;

namespace Laroa.Api.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserGetDto>();
        }
    }
}
