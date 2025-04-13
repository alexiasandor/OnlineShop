using Laroa.Domain;

namespace Laroa.Api.Dtos
{
    public class ReducereGetDto
    {
        public int Id { get; set; }
        public float Procent { get; set; }
        public DateTime Perioada { get; set; }
        public string Tip { get; set; }

        public CategoryGetDto Category { get; set; }
    }
}
