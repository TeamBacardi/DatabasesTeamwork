using System;
using CarsFactory.Data;
using CarsFactory.MongoDB;
using CarsFactory.Excel;
using CarsFactory.MySql;
using CarsFactory.Sqlite;

namespace CarsFactory.ConsoleClient
{
    public class Startup
    {
        public static void Main()
        {
            var db = new CarsFactoryDbContext();

            string filename = "../../20-Aug-2015.zip";

            ExcelImproter.ImportToMssql(filename, db);
            //MongoDbSeeder.ConnectAndSeed();

            var partsReporter = new MySqlData();

            MySqlSeed.Seed(partsReporter);

            var sqlite = new ExpensesEntities();

            // change the password
            var mysqlContex = new MySqlContext("server = localhost; database = carsfactory; uid = root; pwd =9409; ");
            ExcelExporter.Generate(sqlite, mysqlContex);
        }
    }
}
