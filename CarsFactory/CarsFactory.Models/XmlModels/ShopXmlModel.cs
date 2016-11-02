using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using CarsFactory.Models.Contracts;

namespace CarsFactory.Models.XmlModels
{

    [Serializable, XmlType("Shop")]
    public class ShopXmlModel : IShop
    {
        private ICollection<Car> cars;

        public ShopXmlModel()
        {
            this.cars = new HashSet<Car>();
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Car> Cars
        {
            get { return this.cars; }
            set { this.cars = value; }
        }

        public virtual SaleReport SaleReport { get; set; }
    }
}
