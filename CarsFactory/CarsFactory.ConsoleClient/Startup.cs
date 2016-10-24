using System;
using CarsFactory.Data;

namespace CarsFactory.ConsoleClient
{
    public class Startup
    {
        public static void Main()
        {
            var db = new CarsFactoryDbContext();

            // Query stuff with LINQ
            Console.WriteLine("DB initialized");
        }
    }
}
