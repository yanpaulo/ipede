﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iPede.Site.Models.Entities
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}