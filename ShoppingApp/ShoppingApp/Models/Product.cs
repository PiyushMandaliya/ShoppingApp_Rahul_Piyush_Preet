using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Models
{
    class Product
    {
        public long ProductId { get; }
        public long CategoryId { get; }
        public string Name { get; }
        public string Description { get; }
        public double Price { get; }
        public int InventoryCount { get; }
    }
}
