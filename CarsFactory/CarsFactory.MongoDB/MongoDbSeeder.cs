using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using CarsFactory.Models;

using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace CarsFactory.MongoDB
{
    public class MongoDbSeeder
    {
        private static IMongoClient client;
        private static IMongoDatabase database;


        /// <summary>
        /// This method only seeds a clean db, if it's populated it will throw silently
        /// </summary>
        public static async void ConnectAndSeed()
        {
            client = new MongoClient();

            database = client.GetDatabase("carShops");
            
            database.CreateCollection("cars");
            database.CreateCollection("shops");
            database.CreateCollection("parts");

            var cars = database.GetCollection<Car>("cars");
            var shops = database.GetCollection<Shop>("shops");
            var parts = database.GetCollection<Part>("parts");

            cars.InsertMany(CreateCars());
            shops.InsertMany(CreateCarShops());
            parts.InsertMany(CreateParts());
        }

        private static IEnumerable<Car> CreateCars()
        {
            var carLancer = new Car
            {
                Model = "Lancer",
                Id = 1,
                Price = 50000,
                Details = "A ver good car.",
                YearOfManufacture = "2016"
            };

            var carBmw = new Car
            {
                Model = "M3",
                Id = 2,
                Price = 70000,
                Details = "Does not have indicators.",
                YearOfManufacture = "2015"
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
