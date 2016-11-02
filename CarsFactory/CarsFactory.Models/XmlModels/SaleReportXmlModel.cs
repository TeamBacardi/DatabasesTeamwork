using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using CarsFactory.Models.Contracts;

namespace CarsFactory.Models.XmlModels
{

    [Serializable, XmlType("SaleReport")]
    public class SaleReportXmlModel : ISaleReport
    {
        private ICollection<Sale> sales;

        public SaleReportXmlModel()
        {
            this.sales = new HashSet<Sale>();
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

        [Required]
        public virtual Shop Shop { get; set; }
    }
}
