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

            //string filename = "../../20-Aug-2015.zip";

            //var excelImporter = new ExcelImproter(db);
            //excelImporter.ImportToMssql(filename);

            //var mongo = new MongoDbImporter(db);
            //mongo.Transfer();

            //MongoDbSeeder.ConnectAndSeed();

            var partsReporter = new MySqlData();

            MySqlSeed.Seed(partsReporter);

            //var sqlite = new PartsReportsEntities();

            // change the password
            //var mysqlContex = new MySqlContext("server = localhost; database = carsfactory; uid = root; pwd =9208266264s; ");
            //ExcelExporter.Generate(sqlite, mysqlContex);
        }
    }
}
