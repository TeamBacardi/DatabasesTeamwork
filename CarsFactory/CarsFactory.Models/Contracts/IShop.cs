using System.Collections.Generic;

namespace CarsFactory.Models.Contracts
{
    public interface IShop
    {
        ICollection<Car> Cars { get; set; }
        int Id { get; set; }
        string Name { get; set; }
        SaleReport SaleReport { get; set; }
    }
}