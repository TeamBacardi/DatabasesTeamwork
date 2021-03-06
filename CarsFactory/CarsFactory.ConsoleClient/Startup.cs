﻿using CarsFactory.Data;
using CarsFactory.Excel;
using CarsFactory.MySql;
using CarsFactory.Sqlite;
using CarsFactory.XML;
using CarsFactory.SQLDataPopulator;
using CarsFactory.PDF;
using Utils;
using CarsFactory.JSON;

namespace CarsFactory.ConsoleClient
{
    public class Startup
    {
        public static void Main()
        {
            IWritter writter = new ConsoleWritter();
            IReader reader = new ConsoleReader();
            var db = new CarsFactoryDbContext();

            if (db.Database.Exists() == false)
            {
                writter.WriteLine("Creating SQL Database");
                db.Database.CreateIfNotExists();

                SQLPopulatorEngine populator = new SQLPopulatorEngine(db, writter);

                populator.Start();
                Main();
                return;
            }

            /* Read from XML and import in db */
            //var xmlReader = new XmlDataReader(db);           
           // var carsList = xmlReader.DeserializeXmlFileToObjects("../../cars.xml");
            //xmlReader.SaveXmlToDb(carsList);

            string filename = "../../20-Aug-2015.zip";

            //ExcelImproter.ImportToMssql(filename, db);
            
            //MongoDbSeeder.ConnectAndSeed();

            //var partsReporter = new MySqlData();

            //MySqlSeed.Seed(partsReporter);
            //var sqlite = new ExpensesEntities();

            // change the password
            //var mysqlContex = new MySqlContext("server = localhost; database = carsfactory; uid = root; pwd =9409; ");

            //ExcelExporter.Generate(sqlite, mysqlContex);

            //XMLPopulatorEngine xmlPopulator = new XMLPopulatorEngine(db, writter);
            //xmlPopulator.Start();

            //PDFPopulatorEngine pdfPopulator = new PDFPopulatorEngine(db, writter);
            //pdfPopulator.Start();

            JSONPopulationEngine jsonPopulator = new JSONPopulationEngine(db, writter);
            jsonPopulator.Start();
        }
    }
}
