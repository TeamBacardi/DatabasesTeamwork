using System.ComponentModel.DataAnnotations;

namespace CarsFactory.Models
{
    public class PartType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
