using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace iPede.Site.Models.Entities
{
    public class iPedeContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Service> Services { get; set; }

        public DbSet<AboutUs> AboutUs { get; set; }

        public DbSet<BannerItem> BannerItems { get; set; }

        public DbSet<ProductImage> ProductImages { get; set; }

        public iPedeContext()
            : base("DefaultConnection")
        {
            
        }
    }
}