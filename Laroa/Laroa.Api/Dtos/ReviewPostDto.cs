namespace Laroa.Api.Dtos
{
    public class ReviewPostDto
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public string Comment { get; set; }
        public string UserName { get; set; }
    }
}
