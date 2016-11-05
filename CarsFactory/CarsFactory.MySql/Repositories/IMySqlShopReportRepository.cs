using CarsFactory.MySql.Models;
using System.Collections.Generic;
using System.Linq;

namespace CarsFactory.MySql.Repositories
{
    public interface IMySqlShopReportRepository
    {
        void Add(ShopReport entity);

        void AddMany(IEnumerable<ShopReport> entities);

        void DeleteAllReports();

        IQueryable<ShopReport> All();

        void SaveChanges();
    }
}
