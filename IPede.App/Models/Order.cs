using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPede.App.Models
{
    public class Order
    {

        public int Id { get; set; } = new Random().Next(10000, 20000);

        public string StatusName { get; set; }

        public virtual IList<OrderItem> Items { get; set; } = new List<OrderItem>();

        public string DisplayName => $"Pedido {Id}";

        public void AddItem(Product p)
        {
            var item = Items.SingleOrDefault(_p => _p.Product.Id == p.Id);
            if (item != null)
            {
                item.Quantity++;
            }
            else
            {
                Items.Add(new OrderItem { Product = p, Quantity = 1 });
            }
        }
    }
}
