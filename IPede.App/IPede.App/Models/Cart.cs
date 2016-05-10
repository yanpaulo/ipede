using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPede.App.Models
{
    public class Cart
    {
        private static Cart instance = new Cart();
        private List<CartItem> _items;

        private Cart()
        {
            _items = new List<CartItem>();
        }

        public static Cart GetInstance()
        {
            return instance;
        }

        public CartItem[] Items
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
                _items.Add(new CartItem { Product = p, Quantity = 1 }); 
            }
        }


    }
}
