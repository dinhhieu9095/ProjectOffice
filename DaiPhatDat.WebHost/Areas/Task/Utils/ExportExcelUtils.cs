//using LacViet.HPS.Common;
//using LacViet.HPS.Common.Interface;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaiPhatDat.Module.Task.Web.Utils
{

    public enum ErrorState
    {
        //Chưa đọc
        NotYet = 1,
        //có lỗi
        HasError = 2,
        // Không có lỗi
        Valid = 3
    };
    public class ExportExcelUtils
    {
        //public static byte[] CreateExportExcelFile(string templatePath, dynamic exportData, string sheetName)
        //{
        //    using (ExcelPackage package = new ExcelPackage())
        //    {
        //        MemoryStream importFileStream = new MemoryStream(System.IO.File.ReadAllBytes(templatePath));
        //        package.Load(importFileStream);
        //        ExcelWorksheet worksheet = package.Workbook.Worksheets[sheetName];
        //        if (worksheet == null)
        //            worksheet = package.Workbook.Worksheets.FirstOrDefault();
        //        IEplusExtension eplusExtension = new EplusExtension();
        //        eplusExtension.FillData(exportData, worksheet);

        //        return package.GetAsByteArray();
        //    }
        //}

        //public static byte[] CopyCell(string templatePath, string fromSheetName, string toSheetName, string fromAddress, string toAddress)
        //{
        //    using (ExcelPackage package = new ExcelPackage())
        //    {
        //        MemoryStream importFileStream = new MemoryStream(File.ReadAllBytes(templatePath));
        //        package.Load(importFileStream);
        //        ExcelWorksheet fromWorksheet = package.Workbook.Worksheets[fromSheetName];
        //        ExcelWorksheet toWorksheet = package.Workbook.Worksheets[toSheetName];
        //        fromWorksheet.Cells[fromAddress].Copy(toWorksheet.Cells[toAddress]);
        //        return package.GetAsByteArray();
        //    }
        //}
        //public static byte[] CopyCell(byte[] file, string fromSheetName, string toSheetName, string fromAddress, string toAddress)
        //{
        //    using (ExcelPackage package = new ExcelPackage())
        //    {
        //        MemoryStream importFileStream = new MemoryStream(file);
        //        package.Load(importFileStream);
        //        package.Load(importFileStream);
        //        ExcelWorksheet fromWorksheet = package.Workbook.Worksheets[fromSheetName];
        //        ExcelWorksheet toWorksheet = package.Workbook.Worksheets[toSheetName];
        //        fromWorksheet.Cells[fromAddress].Copy(toWorksheet.Cells[toAddress]);
        //        return package.GetAsByteArray();
        //    }
        //}

        //public static byte[] CreateExportExcelFile(string templatePath, dynamic exportData, string sheetName, IDictionary<string, object> parameterData)
        //{
        //    using (ExcelPackage package = new ExcelPackage())
        //    {
        //        MemoryStream importFileStream = new MemoryStream(System.IO.File.ReadAllBytes(templatePath));
        //        package.Load(importFileStream);
        //        ExcelWorksheet worksheet = package.Workbook.Worksheets[sheetName];
        //        if (worksheet == null)
        //            worksheet = package.Workbook.Worksheets.FirstOrDefault();
        //        ExcelRange range;
        //        //range = worksheet.Cells["A1:C1"].Merge = true;
        //        IEplusExtension eplusExtension = new EplusExtension();
        //        if (exportData != null)
        //            eplusExtension.FillData(exportData, worksheet);
        //        if (parameterData != null)
        //            eplusExtension.FillParameter(worksheet, parameterData);
        //        return package.GetAsByteArray();
        //    }
        //}

        //public static byte[] CreateExportExcelFile(byte[] file, dynamic exportData, string sheetName, IDictionary<string, object> parameterData)
        //{
        //    using (ExcelPackage package = new ExcelPackage())
        //    {
        //        MemoryStream importFileStream = new MemoryStream(file);
        //        package.Load(importFileStream);
        //        ExcelWorksheet worksheet = package.Workbook.Worksheets[sheetName];
        //        if (worksheet == null)
        //            worksheet = package.Workbook.Worksheets.FirstOrDefault();
        //        IEplusExtension eplusExtension = new EplusExtension();
        //        if (exportData != null)
        //            eplusExtension.FillData(exportData, worksheet);
        //        if (parameterData != null)
        //            eplusExtension.FillParameter(worksheet, parameterData);
        //        return package.GetAsByteArray();
        //    }
        //}


        //public static byte[] AddDataValidation(byte[] file, IList<string> values, int columIndex, string sheetName)
        //{
        //    using (ExcelPackage package = new ExcelPackage())
        //    {
        //        using (MemoryStream memoryStream = new MemoryStream(file))
        //        {
        //            package.Load(memoryStream);
        //            ExcelWorksheet worksheet = package.Workbook.Worksheets[sheetName];
        //            int startRow = 2;
        //            int countRow = worksheet.Dimension.End.Row;

        //            for (int index = startRow; index <= countRow; index++)
        //            {
        //                string address = worksheet.Cells[index, columIndex].Address;
        //                var validationColumn = worksheet.DataValidations.AddListValidation(address);
        //                validationColumn.Formula.Values.AddRange(values);
        //            }
        //            return package.GetAsByteArray();
        //        }
        //    }
        //}

        //public static byte[] ClearEmptyRows(byte[] file, int columIndexValidate, string sheetName)
        //{
        //    using (ExcelPackage package = new ExcelPackage())
        //    {
        //        using (MemoryStream memoryStream = new MemoryStream(file))
        //        {
        //            package.Load(memoryStream);
        //            ExcelWorksheet worksheet = package.Workbook.Worksheets[sheetName];
        //            int startRow = 2;
        //            int countRow = worksheet.Dimension.End.Row;
        //            int startDelRow = 0;

        //            for (int index = startRow; index <= countRow; index++)
        //            {
        //                string address = worksheet.Cells[index, columIndexValidate].Address;
        //                string value = string.Format("{0}", worksheet.Cells[index, columIndexValidate].Value);
        //                if (string.IsNullOrEmpty(value))
        //                {
        //                    startDelRow = index;
        //                    break;
        //                }
        //            }
        //            if (startDelRow > 0)
        //            {
        //                worksheet.DeleteRow(startDelRow, countRow, true);
        //            }
        //            return package.GetAsByteArray();
        //        }
        //    }
        //}
        //public static string[] GetHeaderColumns(byte[] file, string sheetName)
        //{
        //    using (ExcelPackage package = new ExcelPackage())
        //    {
        //        using (MemoryStream memoryStream = new MemoryStream(file))
        //        {
        //            package.Load(memoryStream);
        //            ExcelWorksheet sheet = package.Workbook.Worksheets[sheetName];
        //            return sheet.Cells[sheet.Dimension.Start.Row, sheet.Dimension.Start.Column, 1, sheet.Dimension.End.Column].Select(firstRowCell => firstRowCell.Text).ToArray();
        //        }
        //    }
        //}
        ////Chien Add start
        //public static int ColumnLetterToColumnIndex(string columnLetter)
        //{
        //    columnLetter = columnLetter.ToUpper();
        //    int sum = 0;

        //    for (int i = 0; i < columnLetter.Length; i++)
        //    {
        //        sum *= 26;
        //        sum += (columnLetter[i] - 'A' + 1);
        //    }
        //    return sum;
        //}
        //public static string ColumnIndexToColumnLetter(int colIndex)
        //{
        //    int div = colIndex;
        //    string colLetter = String.Empty;
        //    int mod = 0;

        //    while (div > 0)
        //    {
        //        mod = (div - 1) % 26;
        //        colLetter = (char)(65 + mod) + colLetter;
        //        div = (int)((div - mod) / 26);
        //    }
        //    return colLetter;
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="file"></param>
        ///// <param name="sheetName"></param>
        ///// <param name="columnLetterHide">split ,</param>
        //public static byte[] HideColumn(byte[] file, string sheetName, string columnLetterHide)
        //{
        //    using (ExcelPackage package = new ExcelPackage())
        //    {
        //        MemoryStream importFileStream = new MemoryStream(file);
        //            package.Load(importFileStream);
        //            ExcelWorksheet worksheet = package.Workbook.Worksheets[sheetName];
        //            if (worksheet == null)
        //                worksheet = package.Workbook.Worksheets.FirstOrDefault();
        //            string[] lst = columnLetterHide.Split(',');
        //            for (int i = 0; i < lst.Length; i++)
        //            {
        //                int index = ColumnLetterToColumnIndex(lst[i]);
        //                worksheet.Column(index).Hidden = true;
        //            }
        //            return package.GetAsByteArray();
        //    }    
        //}
        //Chien Add end
    }
}
