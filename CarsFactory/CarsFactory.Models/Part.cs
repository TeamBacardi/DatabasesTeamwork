using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarsFactory.Models
{
    public class Part
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Weight { get; set; }

        public string Condition { get; set; }

        public string Details { get; set; }

        [ForeignKey("Car")]
        public int CarId { get; set; }

        public virtual Car Car { get; set; }

        [ForeignKey("PartManufacturer")]
        public int ManufacturerId { get; set; }

        public virtual PartManufacturer PartManufacturer { get; set; }

        [ForeignKey("PartType")]
        public int TypeId { get; set; }

        public virtual PartType PartType { get; set; }

        [ForeignKey("PartSupplier")]
        public int SupplierId { get; set; }

        public virtual PartSupplier PartSupplier { get; set; }
    }
}
