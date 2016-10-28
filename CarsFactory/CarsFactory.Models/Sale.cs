using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarsFactory.Models
{
    public class Sale
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Shop")]
        public int ShopId { get; set; }

        public virtual Shop Shop { get; set; }

        [ForeignKey("Car")]
        public int CarId { get; set; }

        public virtual Car Car { get; set; }

        //[ForeignKey("Part")]
        //public int PartId { get; set; }

        //public virtual Part Part { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal Sum { get; set; }
    }
}