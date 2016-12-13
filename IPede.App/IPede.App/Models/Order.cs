﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPede.App.Models
{
    public class Order
    {

        public int Id { get; set; }

        public string StatusName { get; set; }
        
        public virtual IList<OrderItem> Items { get; set; }
    }
}