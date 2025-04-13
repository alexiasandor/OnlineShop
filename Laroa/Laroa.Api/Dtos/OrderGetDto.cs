using Laroa.Domain;

namespace Laroa.Api.Dtos
{
    public class OrderGetDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public double Price { get; set; }
        public List<ProductGetDto> Products { get; set; }

        //Aici mai vine user

    }
}
