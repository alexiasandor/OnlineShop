using System.ComponentModel.DataAnnotations;

namespace Laroa.Api.Dtos
{
    public class UserGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Birthday { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
        public string Role {  get; set; }
    }
}
