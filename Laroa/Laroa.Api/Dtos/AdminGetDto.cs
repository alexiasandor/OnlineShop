using Laroa.Domain;

namespace Laroa.Api.Dtos
{
    public class AdminGetDto
    {
        public int Id { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Email { get; set; }
        public string Permisiuni { get; set; }
    }
}
