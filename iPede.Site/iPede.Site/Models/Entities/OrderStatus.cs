using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace iPede.Site.Models.Entities
{
    public class OrderStatus
    {
        public int OrderStatusId { get; set; }
        [DisplayName("Status")]
        public string Name { get; set; }
    }
}