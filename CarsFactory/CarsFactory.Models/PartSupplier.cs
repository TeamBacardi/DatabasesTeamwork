using System.ComponentModel.DataAnnotations;

namespace CarsFactory.Models
{
    public class PartSupplier
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Address { get; set; }
    }
}
