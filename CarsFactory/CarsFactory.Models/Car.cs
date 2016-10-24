using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarsFactory.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public string YearOfManufacture { get; set; }

        public string Details { get; set; }

        [ForeignKey("Manufacturer")]
        public int ManufacturerId { get; set; }

        public virtual CarManufacturer Manufacturer { get; set; }

        [ForeignKey("Colour")]
        public int ColourId { get; set; }

        public virtual CarColour Colour { get; set; }
    }
}
