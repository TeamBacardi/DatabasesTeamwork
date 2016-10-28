using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

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
    }
}
