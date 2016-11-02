using System;
using System.Collections.Generic;

namespace CarsFactory.Models.Contracts
{
    public interface ISaleReport
    {
        DateTime? Date { get; set; }
        int Id { get; set; }
        string Name { get; set; }
        ICollection<Sale> Sales { get; set; }
        Shop Shop { get; set; }
    }
}