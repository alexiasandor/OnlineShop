using System.ComponentModel.DataAnnotations;

namespace Laroa.Api.Dtos
{
    public class CategoryPostDto
    {
       
        [Required]
        public string Name { get; set; }
    }
}
