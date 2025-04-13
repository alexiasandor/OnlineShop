using AutoMapper;
using Laroa.Api.Dtos;
using Laroa.Domain;

namespace Laroa.Api.Profiles
{
    public class ReduceriProfile : Profile
    {
        public ReduceriProfile()
        {
            CreateMap<Reduceri, ReducereGetDto>();
        }
    }
}
