using System.ComponentModel.DataAnnotations;

namespace CarsFactory.Models
{
    public class CarManufacturer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
