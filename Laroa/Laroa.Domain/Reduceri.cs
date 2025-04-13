using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laroa.Domain
{
    public class Reduceri
    {
        public int Id {  get; set; }   
        public float Procent {  get; set; }    
        public DateTime Perioada { get; set; }
        public string Tip {  get; set; }    
        
        public List<Product> Products { get; set; } 
    }
}
