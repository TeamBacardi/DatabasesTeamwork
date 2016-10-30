using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsFactory.Models
{
    public class Shop
    {
        private ICollection<Car> cars;

        public Shop()
        {
            this.cars = new HashSet<Car>();
        }

        [Key, ForeignKey("SaleReport")]
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Car> Cars
        {
            get { return this.cars; }
            set { this.cars = value; }
        }

        public int? SaleReportId { get; set; }

        public virtual SaleReport SaleReport { get; set; }
    }
}
