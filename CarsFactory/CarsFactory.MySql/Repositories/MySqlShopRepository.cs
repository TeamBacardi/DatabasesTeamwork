using CarsFactory.MySql.Models;
using System.Collections.Generic;
using System.Linq;

namespace CarsFactory.MySql.Repositories
{
    public class MySqlShopRepository : IMySqlShopRepository
    {
        private readonly MySqlContext context;

        public MySqlShopRepository(MySqlContext context)
        {
            this.context = context;
        }

        public void Add(ShopReport entity)
        {
            this.context.Add(entity);
        }

        public void AddMany(IEnumerable<ShopReport> entities)
        {
            this.context.Add(entities);
        }

        public void DeleteAllReports()
        {
            this.context.Delete(this.All());
        }

        public IQueryable<ShopReport> All()
        {
            return this.context.GetAll<ShopReport>();
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }
    }
}
