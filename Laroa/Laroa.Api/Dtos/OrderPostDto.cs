using Laroa.Domain;
using System.ComponentModel.DataAnnotations;

namespace Laroa.Api.Dtos
{
    public class OrderPostDto
    {
        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public int UserId { get; set; }

    }
}
