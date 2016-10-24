using System.ComponentModel.DataAnnotations;

namespace CarsFactory.Models
{
    public class CarColour
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
