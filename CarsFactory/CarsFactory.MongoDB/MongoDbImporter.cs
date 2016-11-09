using CarsFactory.Data;
using CarsFactory.Models;
using MongoDB.Driver;
using Utils;

namespace CarsFactory.MongoDB
{
    public class MongoDbImporter
    {
        private readonly IMongoClient client;
        private readonly CarsFactoryDbContext dbContext;
        
        private static IMongoDatabase database;
        private IWritter writter;

        public MongoDbImporter(CarsFactoryDbContext dbContext, IWritter writter)
        {
            this.dbContext = dbContext;
            this.writter = writter;
            this.client = new MongoClient();
        }

        public void Transfer()
        {
            database = client.GetDatabase("carShops");

            var cars = database.GetCollection<Car>("cars");
            var shops = database.GetCollection<Shop>("shops");
            var parts = database.GetCollection<Part>("parts");

            TransferCarShops(shops, dbContext);
            this.writter.WriteLine("Collection 'shops' successfuly transffered to MSSQL server");

            TransferCars(cars, dbContext);
            this.writter.WriteLine("Collection 'cars' successfuly transffered to MSSQL server");

            TransferParts(parts, dbContext);
            this.writter.WriteLine("Collection 'parts' successfuly transffered to MSSQL server");
        }

        private void TransferCars(IMongoCollection<Car> collection, CarsFactoryDbContext dbContext)
        {
            var cars = collection.Find(c => true).ToList();

            foreach (var car in cars)
            {
                dbContext.Cars.Add(car);
            }

            dbContext.SaveChanges();
        }

        private void TransferParts(IMongoCollection<Part> collection, CarsFactoryDbContext dbContext)
        {
            var parts = collection.Find(p => true).ToList();

            foreach (var part in parts)
            {
                dbContext.Parts.Add(part);
            }

            dbContext.SaveChanges();
        }

        private void TransferCarShops(IMongoCollection<Shop> collection, CarsFactoryDbContext dbContext)
        {
            var shops = collection.Find(s => true).ToList();

            foreach (var entity in shops)
            {
                dbContext.Shops.Add(entity);
            }

            dbContext.SaveChanges();
        }
    }
}
