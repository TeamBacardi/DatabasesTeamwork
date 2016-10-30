using Bytescout.Spreadsheet;
using Bytescout.Spreadsheet.Constants;
using CarsFactory.MySql;
using CarsFactory.MySql.Models;
using CarsFactory.Sqlite;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsFactory.Excel
{
    public static class ExcelExporter
    {
        private const int CellWidth = 300;

        public static void Generate(ExpensesEntities expensesEntities, MySqlContext mySqlContext)
        {
            Spreadsheet document = new Spreadsheet();
            Worksheet sheet = document.Workbook.Worksheets.Add("ShopReport");
            sheet.Cell(0, 0).Value = "ShopName";
            sheet.Cell(0, 1).Value = "TurnOver";
            sheet.Cell(0, 2).Value = "RentExpenses";
            sheet.Cell(0, 3).Value = "SalaryExpenses";
            sheet.Cell(0, 4).Value = "Profit";

            Color headerColor = Color.FromArgb(75, 172, 198);
            sheet.Cell(0, 0).FillPattern = PatternStyle.Solid;
            sheet.Cell(0, 1).FillPattern = PatternStyle.Solid;
            sheet.Cell(0, 2).FillPattern = PatternStyle.Solid;
            sheet.Cell(0, 3).FillPattern = PatternStyle.Solid;
            sheet.Cell(0, 4).FillPattern = PatternStyle.Solid;
            sheet.Cell(0, 0).FillPatternForeColor = headerColor;
            sheet.Cell(0, 1).FillPatternForeColor = headerColor;
            sheet.Cell(0, 2).FillPatternForeColor = headerColor;
            sheet.Cell(0, 3).FillPatternForeColor = headerColor;
            sheet.Cell(0, 4).FillPatternForeColor = headerColor;

            sheet.Columns[0].Width = CellWidth;
            sheet.Columns[1].Width = CellWidth;
            sheet.Columns[2].Width = CellWidth;
            sheet.Columns[3].Width = CellWidth;

            var shopReports = mySqlContext.GetAll<ShopReport>().ToList();
            var row = 1;

            var expensesReports = expensesEntities.Expenses.ToList();

            Console.WriteLine(shopReports.Count);

            for (int i = 0; i < Math.Min(expensesReports.Count, shopReports.Count); i++)
            {
                var report = shopReports[i];
                var turnOver = report.TurnOver;
                var rentExpenses = expensesReports[i].RentExpenses;
                var salaryExpenses = expensesReports[i].SalaryExpenses;

                sheet.Cell(row, 0).Value = report.ShopName;
                sheet.Cell(row, 1).Value = turnOver;
                sheet.Cell(row, 2).Value = expensesReports[i].RentExpenses;
                sheet.Cell(row, 3).Value = expensesReports[i].SalaryExpenses;

                var profit = (decimal)turnOver - (decimal)(rentExpenses + salaryExpenses);
                sheet.Cell(row, 4).Value = profit;
                ++row;
            }




            document.SaveAs("export.xlsx");

            document.Close();
        }
    }
}
