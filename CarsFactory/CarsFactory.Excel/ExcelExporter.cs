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

            Color headerColor = Color.FromArgb(75, 172, 198);
            sheet.Cell(0, 0).FillPattern = PatternStyle.Solid;
            sheet.Cell(0, 1).FillPattern = PatternStyle.Solid;
            sheet.Cell(0, 2).FillPattern = PatternStyle.Solid;
            sheet.Cell(0, 3).FillPattern = PatternStyle.Solid;
            sheet.Cell(0, 0).FillPatternForeColor = headerColor;
            sheet.Cell(0, 1).FillPatternForeColor = headerColor;
            sheet.Cell(0, 2).FillPatternForeColor = headerColor;
            sheet.Cell(0, 3).FillPatternForeColor = headerColor;

            sheet.Columns[0].Width = CellWidth;
            sheet.Columns[1].Width = CellWidth;
            sheet.Columns[2].Width = CellWidth;
            sheet.Columns[3].Width = CellWidth;

            var shopReports = mySqlContext.GetAll<ShopReport>().ToList();
            var row = 1;

            var partReports = expensesEntities.Expenses.ToList();


            foreach (var report in shopReports)
            {
                sheet.Cell(row, 0).Value = report.ShopName;
                sheet.Cell(row, 1).Value = report.TurnOver;
                ++row;
            }


            document.SaveAs("export.xlsx");

            document.Close();
        }
    }
}
