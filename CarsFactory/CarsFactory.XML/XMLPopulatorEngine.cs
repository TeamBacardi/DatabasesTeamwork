using CarsFactory.Data.Contracts;
using CarsFactory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CarsFactory.XML
{
    public class XMLPopulatorEngine
    {
        private const string XmlLocation = "../../CarsFactoryXML.xml";
        private ICarsFactoryDbContext dbContext;

        public XMLPopulatorEngine(ICarsFactoryDbContext dbContext)
        {
            if (this.dbContext == null)
            {
                throw new ArgumentNullException("The db cannot be null");
            }

            this.dbContext = dbContext;
        }

        public void Start()
        {
            PopulateXmlWithDb();
        }

        private void PopulateXmlWithDb()
        {
            using (XmlWriter writer = XmlWriter.Create(XmlLocation))
            {
                writer.WriteStartDocument();

                WriteShopsToXml(writer);

                writer.WriteEndDocument();
            }
        }

        private void WriteShopsToXml(XmlWriter writer)
        {
            writer.WriteStartElement("Shops");

            foreach (var shop in this.dbContext.Shops)
            {
                writer.WriteStartElement("Shop");

                writer.WriteAttributeString("id", shop.Id.ToString());

                writer.WriteStartElement("Name");
                writer.WriteString(shop.Name);
                writer.WriteEndElement();

                WriteCarsToXml(writer, shop.Cars);

                writer.WriteEndElement();
            }

            writer.WriteEndElement();
        }

        private void WriteCarsToXml(XmlWriter writer, IEnumerable<Car> cars)
        {
            writer.WriteStartElement("Cars");

            foreach (var car in cars)
            {

            }

            writer.WriteEndElement();
        }
    }
}
