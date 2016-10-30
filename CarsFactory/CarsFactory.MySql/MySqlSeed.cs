using CarsFactory.Data;
using CarsFactory.MySql.Models;
using System;
using System.Linq;

namespace CarsFactory.MySql
{
    public class MySqlSeed
    {
        public static void Seed(MySqlData mySqlData)
        {
            var db = new CarsFactoryDbContext();
            var randomTurnOverGenerator = GetRandomTurnOver();

                var reports = db.Shops
                .Select(s => new ShopReport
                {
                    ShopName = s.Name,
                    TurnOver = randomTurnOverGenerator
                })
                .ToList();

            Console.WriteLine("Seeding of Shops entries into MySql Db initialized.");
            //// mySqlData.SalesReport.DeleteAllReports();
            mySqlData.ShopReports.AddMany(reports);
            mySqlData.ShopReports.SaveChanges();
            Console.WriteLine("Seeding of Shops entries completed!");
        }

        private static int GetRandomTurnOver()
        {
            var random = new Random();
            var number = random.Next(30000, 100000);
            return number;
        }
    }
}
