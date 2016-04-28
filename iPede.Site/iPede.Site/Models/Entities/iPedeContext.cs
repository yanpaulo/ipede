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

        public DbSet<OrderStatus> OrderStatuses { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<ProductImage> ProductImages { get; set; }

        public virtual DbSet<SuggestedProduct> SuggestedProducts { get; set; }

        public iPedeContext()
            : base("DefaultConnection")
        {
            
        }
    }
}