using System.Linq;
using Utils;

namespace CarsFactory.Sqlite
{
    public class Startup
    {
        static void Main(string[] args)
        {
            var db = new ExpensesEntities();
            IRandomProvider randomProvider = new Utils.RandomProvider();
            SeedSqliteDB(db, randomProvider);
            //foreach (var item in db.Expenses)
            //{
            //    System.Console.WriteLine(item.RentExpenses);
            //}
            System.Console.WriteLine(db.Expenses.Count());
            db.SaveChanges();
        }

        private static void SeedSqliteDB(ExpensesEntities db, IRandomProvider randomProvider)
        {

            for (int i = 0; i < 100; i++)
            {
                db.Expenses.Add(new Expenses()
                {
                    RentExpenses = randomProvider.GetRandomInRange(i + 1000, i + 10000),
                    SalaryExpenses = randomProvider.GetRandomInRange(i + 10000, i + 15000)
                });
            }
        }
    }
}
