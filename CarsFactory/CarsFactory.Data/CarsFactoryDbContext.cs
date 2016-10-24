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

        public IDbSet<CarColour> CarColours { get; set; }

        public IDbSet<CarManufacturer> CarManufacturers { get; set; }

        public IDbSet<Car> Cars { get; set; }

        public IDbSet<PartManufacturer> PartManufacturers { get; set; }

        public IDbSet<PartType> PartTypes { get; set; }

        public IDbSet<PartSupplier> PartSuppliers { get; set; }

        public IDbSet<Part> Parts { get; set; }              
    }
}
