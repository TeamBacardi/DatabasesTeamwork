using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CarsFactory.Data;
using CarsFactory.Excel;
using Microsoft.Win32;

namespace CarsFactory.DesktopClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CarsFactoryDbContext dbContext = new CarsFactoryDbContext();
        public MainWindow()
        {
            InitializeComponent();

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

                ExcelImproter.ImportToMssql(filename, dbContext);

                MessageBox.Show("magic has happened");
            }
        }
    }
}
