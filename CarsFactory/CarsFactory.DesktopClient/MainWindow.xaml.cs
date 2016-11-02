using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using CarsFactory.Data;
using CarsFactory.Excel;
using CarsFactory.Models.Contracts;
using CarsFactory.Models.XmlModels;
using CarsFactory.MongoDB;
using CarsFactory.PDF;
using CarsFactory.SQLDataPopulator;
using CarsFactory.XML;
using Microsoft.Win32;
using Utils;

namespace CarsFactory.DesktopClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CarsFactoryDbContext db = new CarsFactoryDbContext();

        private IWritter writter;
        //  private IReader reader;

        public MainWindow()
        {
            InitializeComponent();

            writter = new TextBoxOutputter(TestBox);
            // reader = new MySqlToolsReader();

            Console.SetOut((TextWriter)writter);
            Console.WriteLine("Started");
            if (db.Database.Exists() == false)
            {
                writter.WriteLine("Creating SQL Database");
                db.Database.CreateIfNotExists();

                SQLPopulatorEngine populator = new SQLPopulatorEngine(db, writter);

                populator.Start();
                //Main();
                //return;
            }
        }



        private void ExcelImportButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                FileName = "Archive",
                DefaultExt = ".zip",
                Filter = "ZIP Archives (.zip)|*.zip"
            };

            var result = fileDialog.ShowDialog();

            if (result == true)
            {
                string filename = fileDialog.FileName;

                ExcelImproter.ImportToMssql(filename, db);

                MessageBox.Show("magic has happened");
            }
        }

        private void MongoDbSeed_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MongoDbSeeder.ConnectAndSeed();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private void MongoDbImport_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                var importer = new MongoDbImporter(db);

                importer.Transfer();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void ImportFromXmlToDb_Click(object sender, RoutedEventArgs e)
        {
            var xmlReader = new XmlDataReader(db);

            OpenFileDialog fileDialog = new OpenFileDialog
            {
                FileName = "XML",
                DefaultExt = ".XML",
                Filter = "XML Files (.xml)|*.xml"
            };

            var result = fileDialog.ShowDialog();

            if (result == true)
            {
                try
                {
                    string filename = fileDialog.FileName;

                    IEnumerable<CarXmlModel> carsList = xmlReader.DeserializeXmlFileToObjects(filename);
                    xmlReader.SaveXmlToDb(carsList);

                    MessageBox.Show("Conversion Complete, and pushed to database.");
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.ToString());
                }
            }
        }

        private void MySQLTools_Click(object sender, RoutedEventArgs e)
        {
            var mySqlToolsReader = new MySqlToolsReader();
            mySqlToolsReader.Show();


        }

        private void GeneratePdf_Click(object sender, RoutedEventArgs e)
        {
            PDFPopulatorEngine pdfPopulator = new PDFPopulatorEngine(db, writter);
            pdfPopulator.Start();
        }

        private void GenerateXml_Click(object sender, RoutedEventArgs e)
        {
            XMLPopulatorEngine xmlPopulator = new XMLPopulatorEngine(db, writter);
            xmlPopulator.Start();
        }
    }
}
