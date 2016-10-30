using CarsFactory.Data;
using CarsFactory.MySql.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace CarsFactory.MySql
{
    public class MySqlSeed
    {
        public static void Seed(MySqlData mySqlData)
        {
            var db = new CarsFactoryDbContext();

            var shops = db.Shops.ToList();
            var reports = new List<ShopReport>();
            IRandomProvider randomProvider = new RandomProvider();

            for (int i = 0; i < shops.Count; i++)
            {
                var shop = shops[i];
                var shopName = shop.Name;
                var turnOver = randomProvider.GetRandomInRange(25000, 50000);

                var report = new ShopReport()
                {
                    ShopName = shopName,
                    TurnOver = turnOver
                };

                reports.Add(report);
            };

            Console.WriteLine("Seeding of Shops entries into MySql Db initialized.");
            //// mySqlData.SalesReport.DeleteAllReports();
            mySqlData.ShopReports.AddMany(reports);
            mySqlData.ShopReports.SaveChanges();
            Console.WriteLine("Seeding of Shops entries completed!");
        }
    }
}
