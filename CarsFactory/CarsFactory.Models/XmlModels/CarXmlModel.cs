using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using CarsFactory.Models.Contracts;

namespace CarsFactory.Models.XmlModels
{
    [XmlRoot("CarsList")]
    [Serializable, XmlType("Car")]
    public class CarXmlModel : ICar
    {
        [XmlIgnore]
        private ICollection<Part> parts;

        public CarXmlModel()
        {
            this.parts = new HashSet<Part>();
        }

        [Key]
        [XmlIgnore]
        public int Id { get; set; }

        [Required]
        public string Model { get; set; }

        public string YearOfManufacture { get; set; }

        public string Details { get; set; }

        public decimal? Price { get; set; }


        [XmlIgnore]
        public virtual Sale Sale { get; set; }

        public int ShopId { get; set; }

        [XmlIgnore]
        public virtual Shop Shop { get; set; }

        [XmlIgnore]
        public virtual ICollection<Part> Parts
        {
            get { return this.parts; }
            set { this.parts = value; }
        }
    }
}