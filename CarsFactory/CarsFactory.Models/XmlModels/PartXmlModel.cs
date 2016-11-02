using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using CarsFactory.Models.Contracts;

namespace CarsFactory.Models.XmlModels
{

    [Serializable, XmlType("Part")]
    public class PartXmlModel : IPart
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
