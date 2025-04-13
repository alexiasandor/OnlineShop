namespace Laroa.Domain
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }  //sezon, material, marime,tip
        public double Price {  get; set; }
        public double PriceR { get; set; }
        public int Stock {  get; set; }
        
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ProductImage ProductImage { get; set; }
        public List<Order> Orders { get; set; }
    }
}