using AutoMapper;
using Laroa.Api.Dtos;
using Laroa.Domain;

namespace Laroa.Api.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryGetDto>();
        }
    }
}
