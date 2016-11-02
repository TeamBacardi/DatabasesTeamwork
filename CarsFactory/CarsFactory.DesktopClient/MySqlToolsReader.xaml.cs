﻿using System;
using System.Windows;
using CarsFactory.Excel;
using CarsFactory.MySql;
using CarsFactory.Sqlite;
using Utils;

namespace CarsFactory.DesktopClient
{
    /// <summary>
    /// Interaction logic for MySqlToolsReader.xaml
    /// </summary>
    public partial class MySqlToolsReader : Window, IReader
    {
        private string password;

        public MySqlToolsReader()
        {
            InitializeComponent();
        }

        public string ReadLine()
        {
            this.password = PasswordTextBox.Password;
            return password;
        }

        private void SeedMySql_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ReadLine();
                if (string.IsNullOrEmpty(this.password))
                {
                    MessageBox.Show("Please enter your Password");
                    return;
                }

                var partsReporter = new MySqlData(this);

                MySqlSeed.Seed(partsReporter);
                MessageBox.Show("Success");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void GenerateExelReport_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                ReadLine();
                var sqlite = new ExpensesEntities();

                if (string.IsNullOrEmpty(this.password))
                {
                    MessageBox.Show("Please enter your Password");
                    return;
                }
                // change the password
                var mysqlContex = new MySqlContext($"server = localhost; database = carsfactory; uid = root; pwd ={this.password}; ");
                ExcelExporter.Generate(sqlite, mysqlContex);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }
    }
}
