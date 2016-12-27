using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iPede.Site.Models.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }

        public int TableId { get; set; }

        public string StatusName { get; set; }

        public ICollection<OrderItemDTO> Items { get; set; }
    }
}