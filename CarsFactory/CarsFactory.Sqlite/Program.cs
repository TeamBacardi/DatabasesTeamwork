using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsFactory.Sqlite
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new ExpensesEntities();

            SeedSqliteDB(db);

            db.SaveChanges();
            Console.WriteLine(db.Expenses.Count());
        }

        private static void SeedSqliteDB(ExpensesEntities db)
        {
            for (int i = 0; i < 100000; i += 5000)
            {
                db.Expenses.Add(new Expenses()
                {
                    RentExpenses = i,
                    SalaryExpenses = i * 2
                });
            }
        }
    }
}
