using System.ComponentModel.DataAnnotations;

namespace Laroa.Api.Dtos
{
    public class ReduceriPostDto
    {
        [Required]
        public int Id {  get; set; }

        [Required]
        public string Tip { get; set; }
        [Required]
        public float Procent { get; set; }
        [Required]
        public DateTime Perioada { get; set; }
    }
}
