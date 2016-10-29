using Bytescout.Spreadsheet;
using Bytescout.Spreadsheet.Constants;
using CarsFactory.MySql;
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
        //public static void Generate(CombinationsContext combinationsContext, MySqlContext mySqlContext)
        //{
        //    Spreadsheet document = new Spreadsheet();
        //    Worksheet sheet = document.Workbook.Worksheets.Add("SalesReport");
        //    sheet.Cell(0, 0).Value = "TOWN";
        //    sheet.Cell(0, 1).Value = "STORE";
        //    sheet.Cell(0, 2).Value = "DATE";
        //    sheet.Cell(0, 3).Value = "SOLD LAPTOPS COUNT";

        //    Color headerColor = Color.FromArgb(75, 172, 198);
        //    sheet.Cell(0, 0).FillPattern = PatternStyle.Solid;
        //    sheet.Cell(0, 1).FillPattern = PatternStyle.Solid;
        //    sheet.Cell(0, 2).FillPattern = PatternStyle.Solid;
        //    sheet.Cell(0, 3).FillPattern = PatternStyle.Solid;
        //    sheet.Cell(0, 0).FillPatternForeColor = headerColor;
        //    sheet.Cell(0, 1).FillPatternForeColor = headerColor;
        //    sheet.Cell(0, 2).FillPatternForeColor = headerColor;
        //    sheet.Cell(0, 3).FillPatternForeColor = headerColor;

        //    sheet.Columns[0].Width = 300;
        //    sheet.Columns[1].Width = 300;
        //    sheet.Columns[2].Width = 300;
        //    sheet.Columns[3].Width = 300;

        //    var a = mySqlContext.GetAll<SalesReport>().ToList();
        //    var row = 1;
        //    var bs = combinationsContext.Stores.ToList();

        //    foreach (var report in a)
        //    {
        //        sheet.Cell(row, 0).Value = report.Town;
        //        sheet.Cell(row, 1).Value = bs.First(x => x.Town == report.Town).Store;
        //        sheet.Cell(row, 2).Value = report.Quantity;
        //        sheet.Cell(row, 3).Value = report.Date;
        //        ++row;
        //    }


        //    document.SaveAs("export.xls");

        //    document.Close();
        //}
    }
}
