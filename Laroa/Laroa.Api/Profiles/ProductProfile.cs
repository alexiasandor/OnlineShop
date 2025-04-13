using AutoMapper;
using Laroa.Api.Dtos;
using Laroa.Domain;

namespace Laroa.Api.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductGetDto>();
        }
    }
}
