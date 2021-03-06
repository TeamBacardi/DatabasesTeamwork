﻿using System.ComponentModel.DataAnnotations;
using CarsFactory.Models.Contracts;

namespace CarsFactory.Models
{
    public class Part : IPart
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Weight { get; set; }
        
        public int? CarId { get; set; }

        public virtual Car Car { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
