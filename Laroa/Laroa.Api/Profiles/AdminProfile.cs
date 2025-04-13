using AutoMapper;
using Laroa.Api.Dtos;
using Laroa.Domain;

namespace Laroa.Api.Profiles
{
    public class AdminProfile : Profile
    {
            public AdminProfile()
            {
                CreateMap<Admin, AdminGetDto>();
            }
        
    }
}
