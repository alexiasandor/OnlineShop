using System.ComponentModel.DataAnnotations;

namespace Laroa.Api.Dtos
{
    public class ProductPostDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public int Stock { get; set; }

        public int CategoryId { get; set; }
    }
}
