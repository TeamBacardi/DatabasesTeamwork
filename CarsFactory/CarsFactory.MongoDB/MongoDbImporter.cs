using CarsFactory.Data;
using CarsFactory.Models;
using MongoDB.Driver;

namespace CarsFactory.MongoDB
{
    public class MongoDbImporter
    {
        private readonly IMongoClient client;
        private readonly CarsFactoryDbContext dbContext;
        
        private static IMongoDatabase database;

        public MongoDbImporter(CarsFactoryDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.client = new MongoClient();
        }

        public void Transfer()
        {
            database = client.GetDatabase("carShops");

            var cars = database.GetCollection<Car>("cars");
            var shops = database.GetCollection<Shop>("shops");
            var parts = database.GetCollection<Part>("parts");

            TransferCars(cars, dbContext);
            TransferParts(parts, dbContext);
            TransferCarShops(shops, dbContext);

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
