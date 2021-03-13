using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Models
{
    class Order
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public double TotalAmount { get; set; }
    }
}
