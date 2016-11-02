using System.Collections.Generic;

namespace CarsFactory.Models.Contracts
{
    public interface ICar
    {
        int Id { get; set; }
        string Model { get; set; }
        string YearOfManufacture { get; set; }
        string Details { get; set; }
        decimal? Price { get; set; }
        Sale Sale { get; set; }
        int ShopId { get; set; }
        Shop Shop { get; set; }
        ICollection<Part> Parts { get; set; }
    }
}