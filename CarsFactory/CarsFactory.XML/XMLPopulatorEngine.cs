using CarsFactory.Data.Contracts;
using CarsFactory.Models;
using System.Collections.Generic;
using System.Xml;
using Utils;

namespace CarsFactory.XML
{
    public class XMLPopulatorEngine
    {
        private const string XmlLocation = "../../../CarsFactoryXML.xml";
        private ICarsFactoryDbContext dbContext;
        private IWritter writter;

        public XMLPopulatorEngine(ICarsFactoryDbContext dbContext, IWritter writter)
        {
            this.dbContext = dbContext;
            this.writter = writter;
        }

        public void Start()
        {
            this.writter.WriteLine("Writting to From SQL To XML");
            PopulateXmlWithDb();
            this.writter.WriteLine("Writting to XML Completed");
        }

        private void PopulateXmlWithDb()
        {
            using (XmlWriter writer = XmlWriter.Create(XmlLocation))
            {
                writer.WriteStartDocument();

                WriteShops(writer);

                writer.WriteEndDocument();
            }
        }

        private void WriteShops(XmlWriter writer)
        {
            writer.WriteStartElement("Shops");

            foreach (var shop in this.dbContext.Shops)
            {
                WriteShop(writer, shop);
            }

            writer.WriteEndElement();
        }

        private void WriteShop(XmlWriter writer, Shop shop)
        {
            writer.WriteStartElement("Shop");

            writer.WriteAttributeString("id", shop.Id.ToString());

            writer.WriteStartElement("Name");
            writer.WriteString(shop.Name);
            writer.WriteEndElement();

            WriteCars(writer, shop.Cars);

            WriteSaleReport(writer, shop.SaleReport);

            writer.WriteEndElement();
        }

        private void WriteSaleReport(XmlWriter writer, SaleReport saleReport)
        {
            writer.WriteStartElement("SaleReport");
            writer.WriteAttributeString("id", saleReport.Id.ToString());
            writer.WriteAttributeString("date", saleReport.Date.ToString());

            writer.WriteStartElement("Name");

            writer.WriteString(saleReport.Name);

            writer.WriteEndElement();

            WriteSales(writer, saleReport);

            writer.WriteEndElement();
        }

        private void WriteSales(XmlWriter writer, SaleReport saleReport)
        {
            writer.WriteStartElement("Sales");

            foreach (var sale in saleReport.Sales)
            {
                WriteSale(writer, sale);
            }

            writer.WriteEndElement();
        }

        private void WriteSale(XmlWriter writer, Sale sale)
        {
            writer.WriteStartElement("Sale");
            writer.WriteAttributeString("id", sale.Id.ToString());
            writer.WriteAttributeString("quantity", sale.Quantity.ToString());
            writer.WriteAttributeString("price", sale.Price.ToString());
            writer.WriteAttributeString("sum", sale.Sum.ToString());

            WriteCar(writer, sale.Car);

            writer.WriteEndElement();
        }

        private void WriteCar(XmlWriter writer, Car car)
        {
            writer.WriteStartElement("Car");
            writer.WriteAttributeString("id", car.Id.ToString());
            writer.WriteAttributeString("details", car.Details);
            writer.WriteAttributeString("model", car.Model);
            writer.WriteAttributeString("year", car.YearOfManufacture);
            writer.WriteAttributeString("price", car.Price.ToString());

            WriteParts(writer, car.Parts);

            writer.WriteEndElement();
        }

        private void WriteParts(XmlWriter writer, ICollection<Part> parts)
        {
            writer.WriteStartElement("parts");

            foreach (var part in parts)
            {
                WritePart(writer, part);
            }

            writer.WriteEndElement();
        }

        private void WritePart(XmlWriter writer, Part part)
        {
            writer.WriteStartElement("part");
            writer.WriteAttributeString("id", part.Id.ToString());
            writer.WriteAttributeString("name", part.Name.ToString());
            writer.WriteAttributeString("weight", part.Weight.ToString());
            writer.WriteAttributeString("price", part.Price.ToString());

            writer.WriteEndElement();
        }

        private void WriteCars(XmlWriter writer, IEnumerable<Car> cars)
        {
            writer.WriteStartElement("Cars");

            foreach (var car in cars)
            {
                WriteCar(writer, car);
            }

            writer.WriteEndElement();
        }
    }
}
