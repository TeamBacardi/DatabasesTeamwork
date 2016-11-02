using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using CarsFactory.Data.Contracts;
using CarsFactory.Models;

namespace CarsFactory.XML
{
    public class XMLDataReader
    {
        private ICarsFactoryDbContext dbContext;

        public XMLDataReader(ICarsFactoryDbContext context)
        {
            this.dbContext = context;
        }

        public void SaveXmlToDb(IEnumerable<Car> cars)
        {
            foreach (var car in cars)
            {
                this.dbContext.Cars.Add(car);
                this.dbContext.SaveChanges();
            }
        }

        public IEnumerable<Car> DeserializeXmlFileToObjects(string xmlFilename)
        {                                
            IEnumerable<Car> carsList;
            using (var reader = new StreamReader(xmlFilename))
            {
                var deserializer = new XmlSerializer(typeof(List<Car>), new XmlRootAttribute("CarsList"));
                carsList = (IEnumerable<Car>)deserializer.Deserialize(reader);
            }

            return carsList;
        }
    }
}
