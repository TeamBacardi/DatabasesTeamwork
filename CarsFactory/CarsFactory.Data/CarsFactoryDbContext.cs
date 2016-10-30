using System.Data.Entity;
using CarsFactory.Data.Migrations;
using CarsFactory.Models;

namespace CarsFactory.Data
{
    public class CarsFactoryDbContext : DbContext
    {
        private const string CarsFactoryDatabaseName = "CarParts";

        public CarsFactoryDbContext()
            : base(CarsFactoryDatabaseName)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<CarsFactoryDbContext, Configuration>());

        }

        public IDbSet<Shop> Shops { get; set; }
        
        public IDbSet<Car> Cars { get; set; }

        public IDbSet<Part> Parts { get; set; }

        public IDbSet<Sale> Sales { get; set; }

        public IDbSet<SaleReport> SaleReports { get; set; }
    }
}
