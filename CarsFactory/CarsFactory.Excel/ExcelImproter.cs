using System;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.OleDb;
using System.IO;
using System.IO.Compression;
using System.Linq;

using CarsFactory.Data;
using CarsFactory.Models;

namespace CarsFactory.Excel
{
    public class ExcelImproter
    {
        private const string PathToExctract = @"../Extracted";
        private static readonly DirectoryInfo DirectoryInfo = new DirectoryInfo(PathToExctract);

        private const string SelectQuery = "Select * from [Sales$]";
        private const string OleDbConnectionString =
            "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES;IMEX=1;'";

        private readonly CarsFactoryDbContext CarsFactoryDbContext;

        public ExcelImproter(CarsFactoryDbContext dbContext)
        {
            CarsFactoryDbContext = dbContext;
        }

        public void ImportToMssql(string archivePath)
        {
            UnzipArchive(archivePath);
            IterateDirectory(DirectoryInfo, CarsFactoryDbContext);
            ClearExtracredDirecotry();
        }

        private static void UnzipArchive(string path)
        {
            ZipFile.ExtractToDirectory(path, PathToExctract);
        }

        private static void ClearExtracredDirecotry()
        {
            foreach (DirectoryInfo dir in DirectoryInfo.GetDirectories())
            {
                dir.Delete(true);
            }
        }

        private static void IterateDirectory(DirectoryInfo directory, CarsFactoryDbContext db)
        {
            DirectoryInfo[] childDirectories = directory.GetDirectories();

            foreach (DirectoryInfo dir in childDirectories)
            {
                IterateDirectory(dir, db);
            }

            string currectDocumentDate = directory.Name;

            FileInfo[] files = directory.GetFiles();
            foreach (var file in files)
            {
                DataTable data = ReadFile(file.FullName);

                SaleReport saleReport = new SaleReport();

                try
                {
                    foreach (DataRow row in data.Rows)
                    {
                        Sale sale = CreateSale(row, db);
                        db.Sales.Add(sale);
                        saleReport.Sales.Add(sale);
                        saleReport.Date = DateTime.Parse(currectDocumentDate);
                        saleReport.Name = $"Report for shop: {sale.Shop.Name}";
                    }

                    db.SaleReports.Add(saleReport);
                    db.SaveChanges();
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

        private static DataTable ReadFile(string path)
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

        private static Sale CreateSale(DataRow row, CarsFactoryDbContext dbContext)
        {
            var shopId = int.Parse(row["ShopId"].ToString());
            var shop = dbContext.Shops.FirstOrDefault(s => s.Id == shopId);

            var carId = int.Parse(row["CarId"].ToString());
            var car = dbContext.Cars.FirstOrDefault(c => c.Id == carId);

            var partId = int.Parse(row["PartId"].ToString());
            var part = dbContext.Parts.FirstOrDefault(p => p.Id == partId);

            var quantity = int.Parse(row["Quantity"].ToString());
            var price = decimal.Parse(row["Price"].ToString());
            var sum = decimal.Parse(row["Sum"].ToString());

            var sale = new Sale
            {
                ShopId = shopId,
                Shop = shop,
                Car = car,
                //Part = part,
                CarId = carId,
                //PartId = partId,
                Price = price,
                Quantity = quantity,
                Sum = sum
            };

            return sale;
        }
    }
}
