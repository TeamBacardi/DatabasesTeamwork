using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsFactory.Sqlite
{
    public class Startup
    {
        static void Main(string[] args)
        {
            var seeder = new SqliteSeeder();

            seeder.Seed();
        }
    }
}
