using System;
using CarsFactory.Data;
using CarsFactory.MongoDB;
using CarsFactory.Excel;
using CarsFactory.MySql;

namespace CarsFactory.ConsoleClient
{
    public class Startup
    {
        public static void Main()
        {
            //var db = new CarsFactoryDbContext();

            //string filename = "../../20-Aug-2015";

            //ExcelImproter.ImportToMssql(filename, db);
            //MongoDbImporter.Connect();

            var reporter = new MySqlData();

            MySqlSeed.Seed(reporter);
        }
    }
}
