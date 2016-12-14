using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace iPede.Site.Models.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public OrderStatus Status { get; set; }

        public virtual Table Table { get; set; }
        
        public virtual ICollection<OrderItem> Items { get; set; }

        #region FKs
        public int OrderStatusId { get; set; }

        [ForeignKey("Table")]
        public int TableId { get; set; }
        #endregion

        #region Constructor
        public Order()
        {
            Items = new List<OrderItem>();
        } 
        #endregion
    }
}