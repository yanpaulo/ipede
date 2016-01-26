using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iPede.Site.Models
{
    public class ShoppingCartViewModel
    {
        public ShoppingCartViewModel()
        {
            Items = new List<ShoppingCartViewModelItem>();
        }
        public List<ShoppingCartViewModelItem> Items { get; private set; }
    }
}