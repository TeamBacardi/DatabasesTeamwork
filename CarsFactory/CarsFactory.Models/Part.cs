using System.Collections.Generic;
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

        //[ForeignKey("Car")]
        //public int CarId { get; set; }

        //public virtual Car Car { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
