using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace iPede.Site.Models.Entities
{
    public class iPedeContext : DbContext
    {
        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<Table> Tables { get; set; }

        public virtual DbSet<OrderStatus> OrderStatuses { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<OrderItem> OrderItems { get; set; }

        public virtual DbSet<ProductImage> ProductImages { get; set; }

        public virtual DbSet<SuggestedProduct> SuggestedProducts { get; set; }

        public OrderStatus GetOrgerStatusByName(string name)
        {
            return OrderStatuses.SingleOrDefault(os => os.Name == name);
        }

        public iPedeContext()
            : base("DefaultConnection")
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Maps Product's MainImage
            //The other side of the mapping is done via class annotation.
            modelBuilder.Entity<Product>()
                .HasOptional(p => p.MainImage)
                .WithMany()
                .HasForeignKey(p => p.MainImageId);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Images)
                .WithRequired(im => im.Product)
                .HasForeignKey(im => im.ProductId)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Order>()
                .HasMany(o => o.Items)
                .WithRequired()
                .HasForeignKey(oi => oi.OrderId)
                .WillCascadeOnDelete();


            base.OnModelCreating(modelBuilder);
        }
    }
}