using iPede.Site.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iPede.Site.Models
{
    public class HomePageViewModel
    {
        public IEnumerable<Category> Categories { get; set; }

        public IEnumerable<BannerItem> BannerItems { get; set; }

        public IEnumerable<Product> Products { get; set; }

    }
}