using System.ComponentModel.DataAnnotations;

namespace CarsFactory.Models
{
    public class PartManufacturer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
