﻿using System.ComponentModel.DataAnnotations;
using CarsFactory.Models.Contracts;

namespace CarsFactory.Models
{
    public class Sale : ISale
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public virtual Car Car { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal Sum { get; set; }

        public SaleReport SaleReport { get; set; }
    }
}