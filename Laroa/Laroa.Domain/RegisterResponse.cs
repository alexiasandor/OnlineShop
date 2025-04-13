using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laroa.Domain
{
    public class RegisterResponse
    {
        public bool Success { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }
    }
}
