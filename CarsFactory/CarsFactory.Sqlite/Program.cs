using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsFactory.Sqlite
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new PartsReportsEntities();

            db.PartsReports.Add(new PartsReports()
            {
                Id = 23123,
                PartName = "Test",
                Price = 22,
                Quaintity = 100
            });

            db.SaveChanges();
            var parts = db.PartsReports.ToList();

            foreach (var item in parts)
            {
                Console.WriteLine(item.PartName);
            }
        }
    }
}
