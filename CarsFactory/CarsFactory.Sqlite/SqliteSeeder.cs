using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsFactory.Sqlite
{
    public class SqliteSeeder
    {
        public void Seed()
        {
            var db = new PartsReportsEntities();

            db.PartsReports.Add(new PartsReports()
            {
                Id = 2,
                PartName = "Side Mirrors",
                Quaintity = 111,
                Price = 24
            });

            db.PartsReports.Add(new PartsReports()
            {
                Id = 2,
                PartName = "Spoilers",
                Quaintity = 50,
                Price = 124
            });

            db.PartsReports.Add(new PartsReports()
            {
                Id = 2,
                PartName = "Roof scoops",
                Quaintity = 50,
                Price = 50
            });

            db.PartsReports.Add(new PartsReports()
            {
                Id = 2,
                PartName = "Front Bumber",
                Quaintity = 142,
                Price = 1111
            });

            db.SaveChanges();
        }
    }
}
