using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using CarsFactory.Models;
using System.Linq;

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

            if (context.Cars.FirstOrDefault() == null)
            {
                CreateShop(context);
                context.SaveChanges();
            }
        }

        private void CreateShop(CarsFactoryDbContext context)
        {
            for (int i = 0; i < 10; i++)
            {
                var shop = new Shop()
                {
                    Name = "Car Shop " + i
                };

                var saleReport = new SaleReport()
                {
                    Shop = shop,
                    Name = "Sale Report for shop " + i,
                    Date = DateTime.Now
                };

                CreateSales(context, saleReport);

                CreateCars(context, shop);

                shop.SaleReport = saleReport;

                context.SaleReports.Add(saleReport);

                context.Shops.Add(shop);
            }
        }

        private void CreateCars(CarsFactoryDbContext context, Shop shop)
        {
            for (int i = 0; i < 13; i++)
            {
                var car = new Car()
                {
                    Price = 3333+i*i,
                    Details = "Car for sale" + i,
                    Model = "Opel " + i,
                    Sale = null,
                    Shop = shop,
                    ShopId = shop.Id,
                    YearOfManufacture = (2000 + i).ToString()
                };

                AddPartsToCar(context, car);

                context.Cars.Add(car);
            }
        }

        private void CreateSales(CarsFactoryDbContext context, SaleReport saleReport)
        {
            for (int i = 0; i < 20; i++)
            {
                var sale = new Sale()
                {
                    Price = 200000 + i * i * i + 21,
                    Sum = i + 1 * 31 * i,
                    Quantity = i + 1,
                    SaleReport = saleReport
                };

                var car = new Car()
                {
                    Price = sale.Price,
                    Details = "Sold car" + i,
                    Model = "Ferrari " + i,
                    Sale = sale,
                    Shop = saleReport.Shop,
                    ShopId = saleReport.Shop.Id,
                    YearOfManufacture = (2000 + i).ToString()             
                };

                AddPartsToCar(context, car);

                context.Cars.Add(car);

                context.Sales.Add(sale);
            }
        }

        private void AddPartsToCar(CarsFactoryDbContext context, Car car)
        {
            for (int i = 0; i < 4; i++)
            {
                var part = new Part()
                {
                    Car = car,
                    CarId = car.Id,
                    Name = car.Model + " Part",
                    Price = i * 10,
                    Weight = i * i + 1
                };

                car.Parts.Add(part);

                context.Parts.Add(part);
            }
        }
    }
}