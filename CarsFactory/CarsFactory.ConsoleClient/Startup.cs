using System;
using CarsFactory.Data;
using CarsFactory.MongoDB;

namespace CarsFactory.ConsoleClient
{
    public class Startup
    {
        public static void Main()
        {
            var db = new CarsFactoryDbContext();

            MongoDbImporter.Connect();
        }
    }
}
