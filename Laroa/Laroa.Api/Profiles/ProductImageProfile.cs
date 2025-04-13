using AutoMapper;
using Laroa.Api.Dtos;
using Laroa.Domain;

namespace Laroa.Api.Profiles
{
    public class ProductImageProfile : Profile
    {
        public ProductImageProfile()
        {
            CreateMap<ProductImage, ProductImageGetDto>();
        }
    }
}
