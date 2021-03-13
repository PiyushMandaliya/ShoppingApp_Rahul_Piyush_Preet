using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Models
{
    class Cart
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public long UserId { get; set; }
    }
}
