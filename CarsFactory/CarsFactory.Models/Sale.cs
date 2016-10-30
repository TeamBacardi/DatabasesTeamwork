using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarsFactory.Models
{
    public class Sale
    {
        [Key, ForeignKey("Car")]
        public int Id { get; set; }

        public int? CarId { get; set; }

        public virtual Car Car { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal Sum { get; set; }

        public int? SaleReportId { get; set; }

        public SaleReport SaleReport { get; set; }
    }
}