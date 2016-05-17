﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iPede.Site.Models.DTOs
{
    public class OrderDTO
    {
        public int OrderId { get; set; }

        public ICollection<OrderItemDTO> Items { get; set; }

        public int OrderStatusId { get; set; }

        
    }
}