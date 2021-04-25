//using OfficeOpenXml;
//using OfficeOpenXml.Style;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Linq;

//namespace SurePortal.Core.Kernel.Application.Helpers
//{
//    public class ExcelExportHelper
//    {
//        public static string ExcelContentType
//        {
//            get
//            {
//                return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
//            }
//        }

//        public static DataTable ListToDataTable<T>(List<T> data)
//        {
//            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
//            DataTable dataTable = new DataTable();

//            for (var i = 0; i < properties.Count; i++)
//            {
//                PropertyDescriptor property = properties[i];
//                dataTable.Columns.Add(property.Name, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType);
//            }

//            object[] values = new object[properties.Count];
//            foreach (var item in data)
//            {
//                for (var i = 0; i < values.Length; i++)
//                {
//                    values[i] = properties[i].GetValue(item);
//                }

//                dataTable.Rows.Add(values);
//            }

//            return dataTable;
//        }

//        public static byte[] ExportExcel(DataTable dataTable, string heading = "", bool generateSerialNumber = false, params string[] columnsToTake)
//        {
//            byte[] result = null;
//            ExcelPackage package = new ExcelPackage();

//            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(String.Format("{0} Data", heading));
//            var startRowFrom = String.IsNullOrEmpty(heading) ? 1 : 3;

//            // generate serial number
//            if (generateSerialNumber)
//            {
//                DataColumn dataColumn = dataTable.Columns.Add("#", typeof(int));
//                dataColumn.SetOrdinal(0);
//                var index = 1;
//                foreach (DataRow item in dataTable.Rows)
//                {
//                    item[0] = index;
//                    index++;
//                }
//            }

//            // add content into excel file
//            worksheet.Cells["A" + startRowFrom].LoadFromDataTable(dataTable, true);

//            //// autofit width of cells
//            //int columnIndex = 1;
//            //foreach (DataColumn column in dataTable.Columns)
//            //{
//            //    ExcelRange columnCells = worksheet.Cells[worksheet.Dimension.Start.Row, columnIndex, worksheet.Dimension.End.Row, columnIndex];
//            //    int maxLength = columnCells.Max(cell => cell.Value.ToString().Count());
//            //    if (maxLength < 150)
//            //    {
//            //        worksheet.Column(columnIndex).AutoFit();
//            //    }

//            //    columnIndex++;
//            //}

//            // format header - bold, yellow on black
//            using (ExcelRange r = worksheet.Cells[startRowFrom, 1, startRowFrom, dataTable.Columns.Count])
//            {
//                r.Style.Font.Color.SetColor(System.Drawing.Color.White);
//                r.Style.Font.Bold = true;
//                r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
//                r.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#1fb5ad"));
//            }

//            // format cells - add borders
//            using (ExcelRange r = worksheet.Cells[startRowFrom + 1, 1, startRowFrom + dataTable.Rows.Count, dataTable.Columns.Count])
//            {
//                r.Style.Border.Top.Style = ExcelBorderStyle.Thin;
//                r.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
//                r.Style.Border.Left.Style = ExcelBorderStyle.Thin;
//                r.Style.Border.Right.Style = ExcelBorderStyle.Thin;

//                r.Style.Border.Top.Color.SetColor(System.Drawing.Color.Black);
//                r.Style.Border.Bottom.Color.SetColor(System.Drawing.Color.Black);
//                r.Style.Border.Left.Color.SetColor(System.Drawing.Color.Black);
//                r.Style.Border.Right.Color.SetColor(System.Drawing.Color.Black);
//            }

//            // removed ignored columns
//            for (int i = dataTable.Columns.Count - 1; i >= 0; i--)
//            {
//                if (i == 0 && generateSerialNumber)
//                {
//                    continue;
//                }
//                if (!columnsToTake.Contains(dataTable.Columns[i].ColumnName))
//                {
//                    worksheet.DeleteColumn(i + 1);
//                }
//            }

//            if (!String.IsNullOrEmpty(heading))
//            {
//                worksheet.Cells["A1"].Value = heading;
//                worksheet.Cells["A1"].Style.Font.Size = 20;

//                worksheet.InsertColumn(1, 1);
//                worksheet.InsertRow(1, 1);
//                worksheet.Column(1).Width = 5;
//            }

//            result = package.GetAsByteArray();

//            return result;
//        }

//        public static byte[] ExportExcel<T>(List<T> data, string heading = "", bool generateSerialNumber = false, params string[] columnsToTake)
//        {
//            return ExportExcel(ListToDataTable<T>(data), heading, generateSerialNumber, columnsToTake);
//        }
//    }
//}