namespace iPede.Site.Migrations
{
    using Models.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<iPede.Site.Models.Entities.iPedeContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(iPede.Site.Models.Entities.iPedeContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.OrderStatuses.AddOrUpdate(
              p => p.Name,
              new OrderStatus { Name = OrderStatus.Placed },
              new OrderStatus { Name = OrderStatus.Confirmed },
              new OrderStatus { Name = OrderStatus.Canceled }

            );
        }
    }
}
