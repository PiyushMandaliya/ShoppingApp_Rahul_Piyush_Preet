using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Models
{
    class Cart
    {
        public long CartId { get;  }
        public long ProductId { get;}
        public long UserId { get;}
        
        public Cart(long CartId, long ProductId, long UserId)
        {

        }
    }
}
