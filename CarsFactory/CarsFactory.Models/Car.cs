using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarsFactory.Models
{
    public class Car
    {
        private ICollection<Part> parts;

        public Car()
        {
            this.parts = new HashSet<Part>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Model { get; set; }

        public string YearOfManufacture { get; set; }

        public string Details { get; set; }
        
        public decimal? Price { get; set; }
        
        public virtual Sale Sale { get; set; }

        public int ShopId { get; set; }

        public virtual Shop Shop { get; set; }

        public virtual ICollection<Part> Parts
        {
            get { return this.parts; }
            set { this.parts = value; }
        }
    }
}
