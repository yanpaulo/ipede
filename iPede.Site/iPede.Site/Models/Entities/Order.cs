using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iPede.Site.Models.Entities
{
    public class Order
    {
        public int OrderId { get; set; }

        public virtual ICollection<OrderItem> Items { get; set; }

        public int OrderStatusId { get; set; }

        public OrderStatus Status { get; set; }

        public Order()
        {
            Items = new List<OrderItem>();
        }
    }
}