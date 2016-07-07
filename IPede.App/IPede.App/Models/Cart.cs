﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPede.App.Models
{
    public class Cart
    {
        private List<OrderItem> _items;

        public Cart()
        {
            _items = new List<OrderItem>();
        }
        
        public OrderItem[] Items
        {
            get { return _items.ToArray(); }
        }

        public void AddItem(Product p)
        {
            var item = _items.SingleOrDefault(_p => _p.Product.ProductId == p.ProductId);
            if (item != null)
            {
                item.Quantity++;
            }
            else
            {
                _items.Add(new OrderItem { Product = p, Quantity = 1 }); 
            }
        }


    }
}
