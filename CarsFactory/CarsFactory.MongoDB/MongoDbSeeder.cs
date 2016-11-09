using System.Collections.Generic;
using CarsFactory.Models;
using MongoDB.Driver;
using Utils;

namespace CarsFactory.MongoDB
{
    public class MongoDbSeeder
    {
        private IMongoClient client;
        private IMongoDatabase database;
        private IWritter writter;

        public MongoDbSeeder(IWritter writter)
        {
            this.writter = writter;
        }

        /// <summary>
        /// This method only seeds a clean db, if it's populated it will throw silently
        /// </summary>
        public void ConnectAndSeed()
        {
            client = new MongoClient();

            database = client.GetDatabase("carShops");
            this.writter.WriteLine("Creating databse - carShops.");

            database.CreateCollection("cars");
            this.writter.WriteLine("Creating collection - cars.");

            database.CreateCollection("shops");
            this.writter.WriteLine("Creating collection - shops.");

            database.CreateCollection("parts");
            this.writter.WriteLine("Creating collection - parts.");


            var cars = database.GetCollection<Car>("cars");
            var shops = database.GetCollection<Shop>("shops");
            var parts = database.GetCollection<Part>("parts");

            cars.InsertMany(CreateCars());
            shops.InsertMany(CreateCarShops());
            parts.InsertMany(CreateParts());

            this.writter.WriteLine("Seeding complete.");
        }

        private static IEnumerable<Car> CreateCars()
        {
            var carLancer = new Car
            {
                Model = "Lancer",
                Id = 1,
                Price = 50000,
                Details = "A ver good car.",
                YearOfManufacture = "2016",
                ShopId = 1
            };

            var carBmw = new Car
            {
                Model = "M3",
                Id = 2,
                Price = 70000,
                Details = "Does not have indicators.",
                YearOfManufacture = "2015",
                ShopId = 1
            };

            ICollection<Car> collectionOfCars = new List<Car>
            {
                carBmw,carLancer
            };

            return collectionOfCars;
        }

        private static IEnumerable<Part> CreateParts()
        {
            var partGearBox = new Part
            {
                Id = 1,
                Name = "Gear box",
                Price = 2000,
                Weight = 1000
            };

            var partWindScreen = new Part
            {
                Id = 2,
                Name = "Wind Screen",
                Price = 100,
                Weight = 300
            };

            IEnumerable<Part> collectionOfParts = new List<Part>
            {
                partWindScreen,partGearBox
            };

            return collectionOfParts;
        }

        private static IEnumerable<Shop> CreateCarShops()
        {
            var carShopGorublqne = new Shop
            {
                Id = 1,
                Name = "Gorublqne Car Dealership"
            };

            var carShopBanishora = new Shop
            {
                Id = 2,
                Name = "Banishora Car Dealership"
            };

            IEnumerable<Shop> shops = new List<Shop> { carShopBanishora, carShopGorublqne };
            return shops;
        }
    }
}
