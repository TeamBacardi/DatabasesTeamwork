using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsFactory.Sqlite
{
    class Startup
    {
        static void Main(string[] args)
        {
            var db = new ExpensesEntities();

            db.Expenses.Add(new Expenses()
            {

            });

            var ex = db.Expenses.ToList();
            foreach (var item in ex)
            {
              
            }
        }
    }
}
