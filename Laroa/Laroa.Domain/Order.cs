using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laroa.Domain
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public double Price { get; set; }
        public List<Product>Products {get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
