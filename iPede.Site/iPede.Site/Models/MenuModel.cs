using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iPede.Site.Models
{
    public class MenuModel
    {
        public MenuModel(IEnumerable<MenuItem> menuItems)
        {
            this.MenuItems = menuItems;
        }

        public IEnumerable<MenuItem> MenuItems { get; private set; }
    }
}