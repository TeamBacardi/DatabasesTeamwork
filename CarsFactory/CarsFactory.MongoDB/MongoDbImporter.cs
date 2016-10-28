using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarsFactory.Data;
using CarsFactory.Models;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;

namespace CarsFactory.MongoDB
{
    public class MongoDbImporter
    {
        private static IMongoDatabase mongoDatabase;

        public static IMongoDatabase GetInstance(string serverName, string databaseName)
        {
            if (mongoDatabase == null)
            {
                var client = new MongoClient(serverName);
                mongoDatabase = client.GetDatabase(databaseName);
            }

            return mongoDatabase;
        }

        public static void Connect()
        {
            var client = new MongoClient();

            var database = client.GetDatabase("test");
            var collection = database.GetCollection<BsonDocument>("carShops");

            var cars = (from r in collection.AsQueryable()
                        select r["carShops"]).ToList();

            var car = CreateCar(cars.ToJson());

            Console.WriteLine(collection.AsQueryable().Select(r => r["carShops"]).ToList()[0]);
            // Console.WriteLine(s[0]);
        }

        private static IEnumerable<Car> ParseShit(string json)
        {
            var listCars = new List<Car>();
            var carShops = JArray.Parse(json);

            foreach (var carShop in carShops)
            {
                string s = carShop.Value<string>();
            }

            var cars = carShops["cars"];
            return listCars;
        }

        private static Car CreateCar(string json)
        {
           IEnumerable<Car> cars = ParseShit(json);
            var car = new Car();
            var jObject = JArray.Parse(json);
            car.Details = (string)jObject["details"];
            car.Model = (string)jObject["model"];
            car.Price = decimal.Parse(jObject["price"].ToString());
            return car;
        }

        private void AddCarToMsSql(CarsFactoryDbContext dbContext, Car car)
        {
            dbContext.Cars.Add(car);
        }
    }
}
