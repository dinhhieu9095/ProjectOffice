using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Drawing.Chart;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace SurePortal.Core.Kernel.Helper
{
    public class EplusExtension
    {
        /// <summary>
        /// Create new Sheet
        /// </summary>
        /// <param name="excelPackage"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public ExcelWorksheet CreateSheet(ExcelPackage excelPackage, string sheetName)
        {
            excelPackage.Workbook.Worksheets.Add(sheetName);
            ExcelWorksheet ws = excelPackage.Workbook.Worksheets[excelPackage.Workbook.Worksheets.Count];
            ws.Name = sheetName;
            return ws;
        }

        /// <summary>
        /// Merge Cell
        /// </summary>
        /// <param name="excelWorksheet"></param>
        /// <param name="excelRange">Address need to merge</param>
        /// <returns></returns>
        public ExcelWorksheet MergeCell(ExcelWorksheet excelWorksheet, ExcelRange excelRange)
        {
            excelWorksheet.Cells[excelRange.Address].Merge = true;
            return excelWorksheet;
        }

        /// <summary>
        /// Split Cell
        /// </summary>
        /// <param name="excelWorksheet"></param>
        /// <param name="excelRange">Address need to merge</param>
        /// <returns></returns>
        public ExcelWorksheet SplitCell(ExcelWorksheet excelWorksheet, ExcelRange excelRange)
        {
            excelWorksheet.Cells[excelRange.FullAddress].Merge = false;
            return excelWorksheet;
        }

        public ExcelWorksheet AddComment(ExcelWorksheet excelWorksheet, ExcelRange excelRange, string comment, string author)
        {
            //Adding a comment to a Cell
            ExcelRange commentCell = excelWorksheet.Cells[excelRange.FullAddress];
            commentCell.AddComment(comment, author);
            return excelWorksheet;
        }

        /// <summary>
        /// Add picture to WorkSheet 
        /// </summary>
        /// <param name="excelWorksheet"></param>
        /// <param name="columnIndex"></param>
        /// <param name="rowIndex"></param>
        /// <param name="filePath">Picture file path</param>
        /// <param name="pictureSize">Size of picture</param>
        /// <returns></returns>
        public ExcelWorksheet AddImage(ExcelWorksheet excelWorksheet, int columnIndex, int rowIndex, string filePath, Size pictureSize)
        {
            //How to Add a Image using EP Plus
            Bitmap image = new Bitmap(filePath);
            ExcelPicture picture = null;
            if (image != null)
            {
                picture = excelWorksheet.Drawings.AddPicture("pic" + rowIndex.ToString() + columnIndex.ToString(), image);
                picture.From.Column = columnIndex;
                picture.From.Row = rowIndex;
                picture.From.ColumnOff = Pixel2MTU(2); //Two pixel space for better alignment
                picture.From.RowOff = Pixel2MTU(2);//Two pixel space for better alignment
                picture.SetSize(pictureSize.Width, pictureSize.Height);
            }
            return excelWorksheet;
        }

        /// <summary>
        /// Addre Custom Shape with text to WorkSheet
        /// </summary>
        /// <param name="excelWorksheet"></param>
        /// <param name="colIndex"></param>
        /// <param name="rowIndex"></param>
        /// <param name="shapeStyle">eShapeStyle</param>
        /// <param name="text">Shape Content inside</param>
        /// <param name="shapeSize">Size of Shape</param>
        /// <returns></returns>
        public ExcelWorksheet AddCustomShape(ExcelWorksheet excelWorksheet, int colIndex, int rowIndex, eShapeStyle shapeStyle, string text, Size shapeSize)
        {
            ExcelShape shape = excelWorksheet.Drawings.AddShape("cs" + rowIndex.ToString() + colIndex.ToString(), shapeStyle);
            shape.From.Column = colIndex;
            shape.From.Row = rowIndex;
            shape.From.ColumnOff = Pixel2MTU(5);
            shape.SetSize(shapeSize.Width, shapeSize.Height);
            shape.RichText.Add(text);
            return excelWorksheet;
        }

        /// <summary>
        /// Fill Parameter
        /// </summary>
        /// <param name="excelWorksheet"></param>
        /// <param name="sheetInfo"></param>
        /// <param name="parameterValues"></param>
        /// <param name="parameterData"></param>
        /// <returns></returns>
        public ExcelWorksheet FillParameter(ExcelWorksheet excelWorksheet, IDictionary<string, object> parameterData)
        {
            IList<FieldInfo> fieldInfos = GetParamenterTemplate(excelWorksheet).Fields.Where(type => type.Type == KeyType_Parameter).ToArray();

            using (ExcelRange cells = excelWorksheet.Cells)
            {
                foreach (FieldInfo fieldInfo in fieldInfos)
                {
                    object value = string.Empty;
                    if (parameterData.TryGetValue(fieldInfo.Name, out value))
                    {
                        using (ExcelRange cell = cells[fieldInfo.ExcelAddress])
                        {
                            cell.Value = value;
                        }
                    }
                }
            }
            return excelWorksheet;
        }

        /// <summary>
        /// Get All Parameter
        /// </summary>
        /// <param name="excelWorksheet"></param>
        /// <returns></returns>
        public ConfigInfo GetParamenterTemplate(ExcelWorksheet excelWorksheet)
        {
            ConfigInfo configInfo = new ConfigInfo();

            ExcelAddressBase dimension = excelWorksheet.Dimension;
            ExcelRange cells = excelWorksheet.Cells;
            for (int rowIndex = 1; rowIndex <= dimension.Rows; rowIndex++)
            {
                for (int columnIndex = 1; columnIndex <= dimension.Columns; columnIndex++)
                {
                    ExcelRange cell = cells[rowIndex, columnIndex];
                    string text = cell.Text;

                    FieldInfo fieldInfo = ParseConfig(text);
                    if (fieldInfo != null)
                    {
                        fieldInfo.ExcelAddress = cell.Address;
                        fieldInfo.ExcelRow = rowIndex;
                        fieldInfo.ExcelColumn = columnIndex;
                        configInfo.Fields.Add(fieldInfo);
                    }
                }
            }
            return configInfo;
        }

        /// <summary>
        /// Get WorkSheet By Sheet Info
        /// </summary>
        /// <param name="excelPackage"></param>
        /// <param name="sheetInfo"></param>
        /// <returns></returns>
        public ExcelWorksheet GetWorkSheet(ExcelPackage excelPackage, SheetInfo sheetInfo)
        {
            ExcelWorksheet excelWorksheet = null;
            if (sheetInfo != null)
                excelWorksheet = sheetInfo.SheetIndex > 0 ? excelPackage.Workbook.Worksheets[sheetInfo.SheetIndex] : excelPackage.Workbook.Worksheets[sheetInfo.SheetName];

            return excelWorksheet;
        }

        /// <summary>
        /// Fill list data to excel
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        /// <param name="excelWorksheet"></param>
        /// <returns></returns>
        public ExcelWorksheet FillData<T>(IList<T> entities, ExcelWorksheet excelWorksheet) where T : class
        {
            using (ExcelRange cells = excelWorksheet.Cells)
            {
                IList<FieldInfo> fieldInfos = GetParamenterTemplate(excelWorksheet).Fields.Where(f => f.Type == KeyType_Field).ToArray();
                if (fieldInfos.Count > 0)
                {
                    int startRowIndex = fieldInfos.Min(e => e.ExcelRow);
                    int endRowIndex = entities.Count - 1;

                    excelWorksheet.InsertRow(startRowIndex + 1, endRowIndex, startRowIndex);
                    int rowIndex = startRowIndex;
                    foreach (T data in entities)
                    {
                        foreach (FieldInfo fieldInfo in fieldInfos)
                        {
                            var value = ReflectorUtility.FollowPropertyPath(data, fieldInfo.Name);
                            cells[rowIndex, fieldInfo.ExcelColumn].Value = value;
                        }
                        rowIndex++;
                    }
                }
            }
            return excelWorksheet;
        }

        /// <summary>
        /// Add Excell Chart To Sheet
        /// </summary>
        /// <typeparam name="ChartType"></typeparam>
        /// <param name="excelWorksheet"></param>
        /// <param name="chartName"></param>
        /// <param name="eChartType"></param>
        /// <returns></returns>
        public ChartType AddChart<ChartType>(ExcelWorksheet excelWorksheet, string chartName, eChartType eChartType) where ChartType : class
        {
            ChartType chartType = excelWorksheet.Drawings.AddChart(chartName, eChartType) as ChartType;
            return chartType;
        }

        #region Private Method
        protected FieldInfo ParseConfig(string text)
        {
            FieldInfo fieldInfo = null;

            if (text.Contains(Key_Start) && text.Contains(Key_End))
            {
                string m_TextNoKey = text.Replace(Key_Start, string.Empty).Replace(Key_End, string.Empty);
                string[] m_TextNoKeyParts = m_TextNoKey.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                if (m_TextNoKeyParts.Length == 2)
                {
                    fieldInfo = new FieldInfo()
                    {
                        Type = m_TextNoKeyParts[0],
                        Name = m_TextNoKeyParts[1]
                    };
                }
            }

            return fieldInfo;
        }

        private int Pixel2MTU(int pixels)
        {
            int mtus = pixels * 9525;
            return mtus;
        }
        #endregion

        #region Constants
        public const string Key_Start = "[[%";
        public const string Key_End = "%]]";
        public const string Key_Seperator = ":";

        public const string KeyType_Parameter = "Parameter";
        public const string KeyType_Field = "Field";
        #endregion

    }
}
