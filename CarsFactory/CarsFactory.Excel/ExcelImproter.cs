using System;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.OleDb;
using System.IO;
using System.IO.Compression;
using System.Linq;

using CarsFactory.Data;
using CarsFactory.Models;
using Utils;

namespace CarsFactory.Excel
{
    public class ExcelImproter
    {
        private const string PathToExctract = @"../Extracted";
        private static readonly DirectoryInfo DirectoryInfo = new DirectoryInfo(PathToExctract);
        private IWritter writter;

        private const string SelectQuery = "Select * from [Sales$]";
        private const string OleDbConnectionString =
            "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES;IMEX=1;'";

        public ExcelImproter(IWritter writter)
        {
            this.writter = writter;
        }

        public void ImportToMssql(string archivePath, CarsFactoryDbContext dbContext)
        {
            UnzipArchive(archivePath);
            IterateDirectory(DirectoryInfo, dbContext);
            ClearExtracredDirecotry();
        }

        private void UnzipArchive(string path)
        {
            try
            {
                ZipFile.ExtractToDirectory(path, PathToExctract);
            }
            catch (Exception)
            {
                Directory.Delete(PathToExctract, true);
                UnzipArchive(path);
            }          
        }

        private void ClearExtracredDirecotry()
        {
            foreach (DirectoryInfo dir in DirectoryInfo.GetDirectories())
            {
                dir.Delete(true);
            }
        }

        private void IterateDirectory(DirectoryInfo directory, CarsFactoryDbContext db)
        {
            DirectoryInfo[] childDirectories = directory.GetDirectories();


            foreach (DirectoryInfo dir in childDirectories)
            {
                IterateDirectory(dir, db);
            }

            this.writter.WriteLine($"Unzipping folder {directory.Name}");

            string currectDocumentDate = directory.Name;

            FileInfo[] files = directory.GetFiles();
            foreach (var file in files)
            {
                this.writter.WriteLine($"Importing from file {file.Name}");
                DataTable data = ReadFile(file.FullName);

                SaleReport saleReport = new SaleReport();

                try
                {
                    int biggestSaleId = 0;
                    foreach (var salesReport in db.Sales)
                    {
                        if (saleReport.Id > biggestSaleId)
                        {
                            biggestSaleId = saleReport.Id;
                        }
                    }

                    int index = 0;
                    foreach (DataRow row in data.Rows)
                    {
                        Sale sale = CreateSale(row, db);
                        sale.Id = biggestSaleId + index++;
                        db.Sales.Add(sale);
                        saleReport.Sales.Add(sale);
                        saleReport.Date = DateTime.Parse(currectDocumentDate);
                        saleReport.Name = $"Report for SaleReport: {sale.Id}";
                    }

                    int biggestId = 1;

                    foreach (var salesReport in db.SaleReports)
                    {
                        if (saleReport.Id > biggestId)
                        {
                            biggestId = saleReport.Id;
                        }
                    }

                    var shop = new Shop()
                    {
                        Name = "Excell Made Shop"
                    };

                    saleReport.Shop = shop;

                    shop.SaleReport = saleReport;

                    saleReport.Id = biggestId + 1;

                    db.Shops.Add(shop);

                    db.SaleReports.Add(saleReport);
                    db.SaveChanges();
                    this.writter.WriteLine($"Finished importing from file {file.Name}");
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }
            }
        }

        private DataTable ReadFile(string path)
        {
            using (var connection = new OleDbConnection())
            {
                var dataTable = new DataTable();
                string fileExtension = Path.GetExtension(path);
                connection.ConnectionString = string.Format(OleDbConnectionString, path);

                using (var command = new OleDbCommand())
                {
                    command.CommandText = string.Format(SelectQuery);
                    command.Connection = connection;

                    using (var dataAdapter = new OleDbDataAdapter())
                    {
                        dataAdapter.SelectCommand = command;
                        dataAdapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
        }

        private Sale CreateSale(DataRow row, CarsFactoryDbContext dbContext)
        {
            var carId = int.Parse(row["CarId"].ToString());
            var car = dbContext.Cars.FirstOrDefault(c => c.Id == carId);
            
            var quantity = int.Parse(row["Quantity"].ToString());
            var price = decimal.Parse(row["Price"].ToString());
            var sum = decimal.Parse(row["Sum"].ToString());

            var sale = new Sale
            {
                Car = car,
                Price = price,
                Quantity = quantity,
                Sum = sum
            };

            return sale;
        }
    }
}
