﻿using IPede.WindowsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPede.WindowsApp.Models
{
    public class CartItem
    {
        public Product Product { get; set; }
        public float Quantity { get; set; }
    }
}
