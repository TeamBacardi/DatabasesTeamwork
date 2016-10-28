using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsFactory.Models
{
    public class SaleReport
    {
        private ICollection<Sale> sales;

        public SaleReport()
        {
            this.sales = new List<Sale>();
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime? Date { get; set; }

        public virtual ICollection<Sale> Sales
        {
            get { return this.sales; }
            set { this.sales = value; }
        }
    }
}
