using System.ComponentModel.DataAnnotations;

namespace Laroa.Api.Dtos
{
    public class AdminPostDto
    {
        [Required]
        public string Nume { get; set; }

        [Required]
        public string Prenume { get; set; }

        [Required]    
        public string Email { get; set; }

        [Required]
        public string Permisiuni { get; set; }
    }
}
