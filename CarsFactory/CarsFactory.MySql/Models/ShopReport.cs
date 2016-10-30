using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsFactory.MySql.Models
{
    public class ShopReport
    {
        public int Id { get; set; }

        public string ShopName { get; set; }

        public decimal TurnOver { get; set; }

    }
}
