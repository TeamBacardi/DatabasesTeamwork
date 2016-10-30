using System.Data.Entity;
using CarsFactory.Models;

namespace CarsFactory.Data.Contracts
{
    public interface ICarsFactoryDbContext
    {
        IDbSet<Car> Cars { get; set; }
        IDbSet<Part> Parts { get; set; }
        IDbSet<SaleReport> SaleReports { get; set; }
        IDbSet<Sale> Sales { get; set; }
        IDbSet<Shop> Shops { get; set; }
    }
}