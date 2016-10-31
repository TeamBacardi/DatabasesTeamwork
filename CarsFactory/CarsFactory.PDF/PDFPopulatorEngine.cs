using CarsFactory.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using Utils;

namespace CarsFactory.PDF
{
    public class PDFPopulatorEngine
    {
        private const string url = "../../../Cars-Factory.pdf";
        private ICarsFactoryDbContext context;
        private IWritter writter;

        public PDFPopulatorEngine(ICarsFactoryDbContext context, IWritter writter)
        {
            this.context = context;
            this.writter = writter;
        }

        public void Start()
        {
            writter.WriteLine("Writting From SQL DB To PDF");

            Document doc = new Document(PageSize.LETTER, 10, 10, 42, 35);
            var fileStream = new FileStream(url, FileMode.Create);
            PdfWriter pdfWritter = PdfWriter.GetInstance(doc, fileStream);

            doc.Open();

            WriteShops(doc);

            doc.Add(new Paragraph(" "));
            doc.Add(new Paragraph(" "));

            WriteCars(doc);

            doc.Add(new Paragraph(" "));
            doc.Add(new Paragraph(" "));

            WriteSaleReports(doc);

            doc.Add(new Paragraph(" "));
            doc.Add(new Paragraph(" "));

            WriteSales(doc);

            doc.Add(new Paragraph(" "));
            doc.Add(new Paragraph(" "));

            WriteCarParts(doc);

            doc.Close();

            writter.WriteLine("Writing to PDF Completed");
        }

        private void WriteCarParts(Document doc)
        {
            PdfPTable carPartsTable = new PdfPTable(4);

            PdfPCell cell = new PdfPCell(new Phrase("CAR PARTS"));

            cell.Colspan = 4;

            cell.Border = 0;

            cell.HorizontalAlignment = 1;

            carPartsTable.AddCell(cell);

            carPartsTable.AddCell("ID");
            carPartsTable.AddCell("NAME");
            carPartsTable.AddCell("WEIGHT");
            carPartsTable.AddCell("PRICE");

            foreach (var part in this.context.Parts)
            {
                carPartsTable.AddCell(part.Id.ToString());
                carPartsTable.AddCell(part.Name);
                carPartsTable.AddCell(part.Weight.ToString());
                carPartsTable.AddCell(part.Price.ToString());
            }

            doc.Add(carPartsTable);
        }

        private void WriteSales(Document doc)
        {
            PdfPTable salesTable = new PdfPTable(5);

            PdfPCell cell = new PdfPCell(new Phrase("SALES"));

            cell.Colspan = 5;

            cell.Border = 0;

            cell.HorizontalAlignment = 1;

            salesTable.AddCell(cell);

            salesTable.AddCell("ID");
            salesTable.AddCell("QUANTITY");
            salesTable.AddCell("PRICE");
            salesTable.AddCell("SUM");
            salesTable.AddCell("SALE REPORT NAME");

            foreach (var sale in this.context.Sales)
            {
                salesTable.AddCell(sale.Id.ToString());
                salesTable.AddCell(sale.Quantity.ToString());
                salesTable.AddCell(sale.Price.ToString());
                salesTable.AddCell(sale.Sum.ToString());
                salesTable.AddCell(sale.SaleReport.Name);
            }

            doc.Add(salesTable);
        }

        private void WriteSaleReports(Document doc)
        {
            PdfPTable saleReportsTable = new PdfPTable(3);

            PdfPCell cell = new PdfPCell(new Phrase("SALE REPORTS"));

            cell.Colspan = 3;

            cell.Border = 0;

            cell.HorizontalAlignment = 1;

            saleReportsTable.AddCell(cell);

            saleReportsTable.AddCell("ID");
            saleReportsTable.AddCell("NAME");
            saleReportsTable.AddCell("DATE");

            foreach (var saleReport in this.context.SaleReports)
            {
                saleReportsTable.AddCell(saleReport.Id.ToString());
                saleReportsTable.AddCell(saleReport.Name);
                saleReportsTable.AddCell(saleReport.Date.ToString());
            }

            doc.Add(saleReportsTable);
        }

        private void WriteCars(Document doc)
        {
            PdfPTable carsTable = new PdfPTable(5);

            PdfPCell cell = new PdfPCell(new Phrase("CARS"));

            cell.Colspan = 5;

            cell.Border = 0;

            cell.HorizontalAlignment = 1;

            carsTable.AddCell(cell);

            carsTable.AddCell("ID");
            carsTable.AddCell("MODEL");
            carsTable.AddCell("YEAR OF MANUFACTURE");
            carsTable.AddCell("DETAILS");
            carsTable.AddCell("PRICE");

            foreach (var car in this.context.Cars)
            {
                carsTable.AddCell(car.Id.ToString());
                carsTable.AddCell(car.Model);
                carsTable.AddCell(car.YearOfManufacture.ToString());
                carsTable.AddCell(car.Details);
                carsTable.AddCell(car.Price.ToString());
            }

            doc.Add(carsTable);
        }

        private void WriteShops(Document doc)
        {
            PdfPTable shopsTable = new PdfPTable(2);

            PdfPCell cell = new PdfPCell(new Phrase("SHOPS"));

            cell.Colspan = 2;

            cell.Border = 0;

            cell.HorizontalAlignment = 1;

            shopsTable.AddCell(cell);

            shopsTable.AddCell("ID");
            shopsTable.AddCell("NAME");

            foreach (var shop in this.context.Shops)
            {
                shopsTable.AddCell(shop.Id.ToString());
                shopsTable.AddCell(shop.Name);
            }

            doc.Add(shopsTable);
        }
    }
}
