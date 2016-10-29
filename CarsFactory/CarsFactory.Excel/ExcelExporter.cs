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
        public static void Generate(PartsReportsEntities partsReportsEntities, MySqlContext mySqlContext)
        {
            Spreadsheet document = new Spreadsheet();
            Worksheet sheet = document.Workbook.Worksheets.Add("ShopReport");
            sheet.Cell(0, 0).Value = "ShopName";
            sheet.Cell(0, 1).Value = "Profit";
            sheet.Cell(0, 2).Value = "DATE";
            sheet.Cell(0, 3).Value = "SOLD LAPTOPS COUNT";

            Color headerColor = Color.FromArgb(75, 172, 198);
            sheet.Cell(0, 0).FillPattern = PatternStyle.Solid;
            sheet.Cell(0, 1).FillPattern = PatternStyle.Solid;
            sheet.Cell(0, 2).FillPattern = PatternStyle.Solid;
            sheet.Cell(0, 3).FillPattern = PatternStyle.Solid;
            sheet.Cell(0, 0).FillPatternForeColor = headerColor;
            sheet.Cell(0, 1).FillPatternForeColor = headerColor;
            sheet.Cell(0, 2).FillPatternForeColor = headerColor;
            sheet.Cell(0, 3).FillPatternForeColor = headerColor;

            sheet.Columns[0].Width = 300;
            sheet.Columns[1].Width = 300;
            sheet.Columns[2].Width = 300;
            sheet.Columns[3].Width = 300;

            var a = mySqlContext.GetAll<ShopReport>().ToList();
            var row = 1;
            var bs = partsReportsEntities.PartsReports.ToList();

            foreach (var report in a)
            {
                sheet.Cell(row, 0).Value = report.ShopName;
                sheet.Cell(row, 1).Value = report.ShopName;
                ++row;
            }


            document.SaveAs("export.xls");

            document.Close();
        }
    }
}
