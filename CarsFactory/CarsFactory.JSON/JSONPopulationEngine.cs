using CarsFactory.Data.Contracts;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Utils;

namespace CarsFactory.JSON
{
    public class JSONPopulationEngine
    {
        private const string url = "../../../JsonReports/{0}.json";

        private ICarsFactoryDbContext contex;
        private IWritter writter;

        public JSONPopulationEngine(ICarsFactoryDbContext context, IWritter writter)
        {
            this.contex = context;
            this.writter = writter;
        }

        public void Start()
        {
            foreach (var sale in contex.Sales)
            {


                var currentSale = new
                {
                    SaleId = sale.Id,
                    CarId = sale.Car,
                    Quantity = sale.Quantity,
                    TotalIncomes = sale.Sum
                };
                

                var serializedObject = JsonConvert.SerializeObject(currentSale, Formatting.Indented);

                using (var writer = new StreamWriter(string.Format(url, currentSale.SaleId)))
                {
                    writer.WriteLine(serializedObject);
                }
            }
        }

    }
}
