using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using CarsFactory.Data.Contracts;
using CarsFactory.Models;
using CarsFactory.Models.Contracts;
using CarsFactory.Models.XmlModels;
using Utils;

namespace CarsFactory.XML
{
    public class XmlDataReader
    {
        private ICarsFactoryDbContext dbContext;
        private IWritter writter;

        public XmlDataReader(ICarsFactoryDbContext context, IWritter writter)
        {
            this.dbContext = context;
            this.writter = writter;
        }

        public void SaveXmlToDb(IEnumerable<ICar> cars)
        {
            foreach (var car in cars)
            {
                Car carToAdd = new Car
                {
                    Details = car.Details,
                    Id = car.Id,
                    Model = car.Model,
                    ShopId = car.ShopId,
                    YearOfManufacture = car.YearOfManufacture
                };

                this.dbContext.Cars.Add(carToAdd);
                this.dbContext.SaveChanges();
                writter.WriteLine($"Added car with id-{carToAdd.Id} to database.");
            }
            writter.WriteLine("XML to MSSQL transfer finished.");
        }

        public IEnumerable<CarXmlModel> DeserializeXmlFileToObjects(string xmlFilename)
        {
            IEnumerable<CarXmlModel> carsList;

            using (var reader = new StreamReader(xmlFilename))
            {
                var deserializer = new XmlSerializer(typeof(List<CarXmlModel>), new XmlRootAttribute("CarsList"));
                carsList = (IEnumerable<CarXmlModel>)deserializer.Deserialize(reader);
            }

            return carsList;
        }
    }
}
