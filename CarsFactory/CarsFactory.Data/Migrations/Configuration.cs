using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using CarsFactory.Models;

namespace CarsFactory.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<CarsFactoryDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
            this.ContextKey = "CarsFactory.Data.CarsFactoryDbContext";
        }

        protected override void Seed(CarsFactoryDbContext context)
        {
            //This method will be called after migrating to the latest version.

            //You can use the DbSet<T>.AddOrUpdate() helper extension method 
            // to avoid creating duplicate seed data. E.g.

            // context.People.AddOrUpdate(
            //   p => p.FullName,
            //   new Person { FullName = "Andrew Peters" },
            //   new Person { FullName = "Brice Lambson" },
            //   new Person { FullName = "Rowan Miller" }
            // );



            context.Cars.AddOrUpdate(
                c => c.Model,
                new Car { Model = "Astra" },
                new Car { Model = "Lancer" });

            context.Shops.AddOrUpdate(
                s => s.Name,
                new Shop { Name = "Banishora" },
                new Shop { Name = "Gorublqne" });
        }
    }
}