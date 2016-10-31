using CarsFactory.Data;
using CarsFactory.Data.Contracts;
using CarsFactory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace CarsFactory.SQLDataPopulator
{
    public class SQLPopulatorEngine
    {
        private ICarsFactoryDbContext context;
        private IWritter writter;

        public SQLPopulatorEngine(ICarsFactoryDbContext dbContext, IWritter writter)
        {
            this.context = dbContext;
            this.writter = writter;
        }

        public void Start()
        {
            if (IsDBPopulated() == false)
            {
                this.writter.WriteLine("Seeding of Shops entries into Sql Db initialized.");
                CreateShop(context);
                context.SaveChanges();
                this.writter.WriteLine("Seeding of Shops entries into Sql Db Completed.");
            }
        }

        public bool IsDBPopulated()
        {
            return context.Cars.Select(c => c.Model).FirstOrDefault() != null;
        }

        private void CreateShop(ICarsFactoryDbContext context)
        {
            for (int i = 0; i < 5; i++)
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

        private void CreateCars(ICarsFactoryDbContext context, Shop shop)
        {
            for (int i = 0; i < 4; i++)
            {
                var car = new Car()
                {
                    Price = 3333 + i * i,
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

        private void CreateSales(ICarsFactoryDbContext context, SaleReport saleReport)
        {
            for (int i = 0; i < 4; i++)
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

        private void AddPartsToCar(ICarsFactoryDbContext context, Car car)
        {
            for (int i = 0; i < 3; i++)
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
