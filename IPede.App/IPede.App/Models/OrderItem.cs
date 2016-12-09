using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPede.App.Models
{
    public class OrderItem
    {
        public int? Id { get; set; }
        public Product Product { get; set; }
        public float Quantity { get; set; }
    }
}
