using Laroa.Domain;

namespace Laroa.Api.Dtos
{
    public class ProductGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public double Price { get; set; }
        public double PriceR {  get; set; }
        public int Stock {  get; set; }

        public CategoryGetDto Category { get; set; }
        public ProductImageGetDto ProductImage { get; set; }
    }
}
