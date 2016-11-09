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

            var saleReports = contex.SaleReports;

            var jsonSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            foreach (var saleReport in saleReports)
            {
                var serializedObject = JsonConvert.SerializeObject(saleReports, Formatting.Indented, jsonSettings);
                this.writter.WriteLine($"Creating sale report {saleReport.Id}");

                var filepath = $"../../../JsonReports/Report-{saleReport.Id}.json";

                using (var file = File.CreateText(filepath))
                {
                    file.Write(serializedObject);
                }
            }
            this.writter.WriteLine($"Finished");
        }
    }
}
