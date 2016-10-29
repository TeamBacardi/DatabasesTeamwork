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

            MongoDbSeeder.ConnectAndSeed();

            var mongoImporter = new MongoDbImporter(db);
            mongoImporter.Transfer();
        }
    }
}
