using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Drawing.Chart;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;

namespace DaiPhatDat.Core.Kernel.Helper
{
    public class ExcelUtility
    {
        #region Constructors

        public ExcelUtility()
        {
            this.ParameterData = new Dictionary<string, object>();
        }

        #endregion

        #region Properties

        public byte[] TemplateFileData { get; set; }
        public ConfigInfo ConfigInfo { get; set; }

        public IDictionary<string, object> ParameterData { get; set; }
        public byte[] OutputData { get; set; }

        public Action<ExcelWorksheet, ExcelCellBase, FieldInfo> AfterFillParameter { get; set; }
        public Action<ExcelWorksheet> PrepareTemplate { get; set; }
        public Action<ExcelWorksheet> AfterFillAllData { get; set; }

        #endregion

        #region Methods

        public byte[] Export<TEntity>(IList<TEntity> entities) where TEntity : class
        {
            return this.Export(entities, null);
        }
        public byte[] Export<TEntity>(IList<TEntity> entities, SheetInfo sheetInfo) where TEntity : class
        {
            //1. Read Config
            this.ReadConfig(sheetInfo);

            //2. Fill Paremter
            this.FillParameter(sheetInfo);

            //3. Export
            this.FillData(entities, sheetInfo);

            return this.OutputData;
        }

        private void ReadConfig()
        {
            this.ReadConfig(null);
        }
        private void ReadConfig(SheetInfo sheetInfo)
        {
            this.ConfigInfo = new ConfigInfo();

            //1. Read data file
            using (ExcelPackage m_ExcelPackage = new ExcelPackage())
            {
                //Open Excel + Get WorkSheet
                using (var m_MemoryStream = new MemoryStream(this.TemplateFileData))
                {
                    m_ExcelPackage.Load(m_MemoryStream);
                }

                //Get Worksheet
                ExcelWorksheet m_ExcelWorksheet = this.GetWorkSheet(m_ExcelPackage, sheetInfo);
                if (m_ExcelWorksheet == null)
                    m_ExcelWorksheet = m_ExcelPackage.Workbook.Worksheets.First();

                //Prepare Template
                if (PrepareTemplate != null)
                    PrepareTemplate(m_ExcelWorksheet);

                //Get Config
                var m_Dimension = m_ExcelWorksheet.Dimension;
                var m_Cells = m_ExcelWorksheet.Cells;
                for (int m_RowIndex = 1; m_RowIndex <= m_Dimension.Rows; m_RowIndex++)
                {
                    for (int m_ColumnIndex = 1; m_ColumnIndex <= m_Dimension.Columns; m_ColumnIndex++)
                    {
                        var m_Cell = m_Cells[m_RowIndex, m_ColumnIndex];
                        string m_Text = m_Cell.Text;

                        var m_FieldInfo = this.ParseConfig(m_Text);
                        if (m_FieldInfo != null)
                        {
                            m_FieldInfo.ExcelAddress = m_Cell.Address;
                            m_FieldInfo.ExcelRow = m_RowIndex;
                            m_FieldInfo.ExcelColumn = m_ColumnIndex;
                            this.ConfigInfo.Fields.Add(m_FieldInfo);
                        }
                    }
                }

                this.TemplateFileData = m_ExcelPackage.GetAsByteArray();
            }
        }

        protected FieldInfo ParseConfig(string text)
        {
            FieldInfo m_FieldInfo = null;

            if (text.Contains(Key_Start) && text.Contains(Key_End))
            {
                string m_TextNoKey = text.Replace(Key_Start, string.Empty).Replace(Key_End, string.Empty);
                string[] m_TextNoKeyParts = m_TextNoKey.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                if (m_TextNoKeyParts.Length == 2)
                {
                    m_FieldInfo = new FieldInfo()
                    {
                        Type = m_TextNoKeyParts[0],
                        Name = m_TextNoKeyParts[1]
                    };
                }
            }
            else
                m_FieldInfo = null;

            return m_FieldInfo;
        }
        private void FillParameter()
        {
            this.FillParameter(null);
        }
        private void FillParameter(SheetInfo sheetInfo)
        {
            using (ExcelPackage m_ExcelPackage = new ExcelPackage())
            {
                using (var m_MemoryStream = new MemoryStream(this.TemplateFileData))
                {
                    m_ExcelPackage.Load(m_MemoryStream);
                }

                //Get Worksheet
                ExcelWorksheet m_ExcelWorksheet = this.GetWorkSheet(m_ExcelPackage, sheetInfo);
                if (m_ExcelWorksheet == null)
                    m_ExcelWorksheet = m_ExcelPackage.Workbook.Worksheets.First();

                using (var m_Cells = m_ExcelWorksheet.Cells)
                {
                    FieldInfo[] m_FieldInfos = this.ConfigInfo.Fields.Where(f => f.Type == KeyType_Parameter).ToArray();
                    foreach (var m_FieldInfo in m_FieldInfos)
                    {
                        object m_Value = string.Empty;
                        if (this.ParameterData.TryGetValue(m_FieldInfo.Name, out m_Value))
                        {
                            using (var m_Cell = m_Cells[m_FieldInfo.ExcelAddress])
                            {
                                m_Cell.Value = m_Value;

                                if (AfterFillParameter != null)
                                    AfterFillParameter(m_ExcelWorksheet, m_Cell, m_FieldInfo);
                            }
                        }
                    }
                }

                this.OutputData = m_ExcelPackage.GetAsByteArray();
            };
        }

        private void FillData<TEntity>(IList<TEntity> entities) where TEntity : class
        {
            this.FillData(entities, null);
        }
        private void FillData<TEntity>(IList<TEntity> entities, SheetInfo sheetInfo) where TEntity : class
        {
            using (ExcelPackage m_ExcelPackage = new ExcelPackage())
            {
                using (var m_MemoryStream = new MemoryStream(this.OutputData))
                {
                    m_ExcelPackage.Load(m_MemoryStream);
                }

                //Get Worksheet
                ExcelWorksheet m_ExcelWorksheet = this.GetWorkSheet(m_ExcelPackage, sheetInfo);
                if (m_ExcelWorksheet == null)
                    m_ExcelWorksheet = m_ExcelPackage.Workbook.Worksheets.First();

                using (var m_Cells = m_ExcelWorksheet.Cells)
                {
                    FieldInfo[] m_FieldInfos = this.ConfigInfo.Fields.Where(f => f.Type == KeyType_Field).ToArray();
                    if (m_FieldInfos.Length > 0)
                    {
                        int m_RowBeginIndex = m_FieldInfos.FirstOrDefault().ExcelRow;
                        //Insert Zone
                        m_ExcelWorksheet.InsertRow(m_RowBeginIndex + 1, entities.Count - 1, m_RowBeginIndex);

                        //Fill
                        int m_RowIndex = m_RowBeginIndex;
                        Type m_EntityType = typeof(TEntity);
                        foreach (var m_Entity in entities)
                        {
                            foreach (var m_FieldInfo in m_FieldInfos)
                            {
                                var m_Value = ReflectorUtility.FollowPropertyPath(m_Entity, m_FieldInfo.Name);
                                m_Cells[m_RowIndex, m_FieldInfo.ExcelColumn].Value = m_Value;
                                //PropertyInfo m_PropertyInfo = m_EntityType.GetProperty(m_FieldInfo.Name);
                                //if (m_PropertyInfo != null)
                                //{
                                //    object m_Value = m_PropertyInfo.GetValue(m_Entity);
                                //    m_Cells[m_RowIndex, m_FieldInfo.ExcelColumn].Value = m_Value;
                                //}
                            }
                            m_RowIndex++;
                        }

                        if (AfterFillAllData != null)
                            this.AfterFillAllData(m_ExcelWorksheet);
                    }
                }

                this.OutputData = m_ExcelPackage.GetAsByteArray();
            };
        }

        public object[,] ReadData(byte[] data)
        {
            object[,] m_DataOutput = null;

            //2. Import Excel
            using (var stream = new MemoryStream(data))
            {
                m_DataOutput = this.ReadData(stream, null);
            }

            return m_DataOutput;
        }
        public object[,] ReadData(byte[] data, SheetInfo sheetInfo)
        {
            object[,] m_DataOutput = null;

            //2. Import Excel
            using (var stream = new MemoryStream(data))
            {
                m_DataOutput = this.ReadData(stream, sheetInfo);
            }

            return m_DataOutput;
        }
        public object[,] ReadData(Stream stream)
        {
            return this.ReadData(stream, null);
        }
        public object[,] ReadData(Stream stream, SheetInfo sheetInfo)
        {
            object[,] m_DataOutput = null;

            //2. Import Excel
            using (ExcelPackage m_ExcelPackage = new ExcelPackage())
            {
                // Open the Excel file and load it to the ExcelPackage
                m_ExcelPackage.Load(stream);

                ExcelWorksheet m_ExcelWorksheet = null;
                if (sheetInfo != null)
                    m_ExcelWorksheet = sheetInfo.SheetIndex > 0 ? m_ExcelPackage.Workbook.Worksheets[sheetInfo.SheetIndex] : m_ExcelPackage.Workbook.Worksheets[sheetInfo.SheetName];
                else
                    m_ExcelWorksheet = m_ExcelPackage.Workbook.Worksheets.FirstOrDefault();

                if (m_ExcelWorksheet != null)
                {
                    var m_Dimension = m_ExcelWorksheet.Dimension;
                    var m_Cells = m_ExcelWorksheet.Cells;
                    m_DataOutput = new object[m_Dimension.Rows, m_Dimension.Columns];
                    for (int m_RowIndex = 0; m_RowIndex < m_Dimension.Rows; m_RowIndex++)
                    {
                        for (int m_ColumnIndex = 0; m_ColumnIndex < m_Dimension.Columns; m_ColumnIndex++)
                        {
                            m_DataOutput[m_RowIndex, m_ColumnIndex] = m_Cells[m_RowIndex + 1, m_ColumnIndex + 1].Value;
                        }
                    }
                }
                else
                {
                    throw new ArgumentException("Không tìm thấy Sheet tương ứng");
                }
            }

            return m_DataOutput;
        }
        public Dictionary<string, object> ReadCellData(byte[] data)
        {
            return this.ReadCellData(data, null);
        }
        public Dictionary<string, object> ReadCellData(byte[] data, SheetInfo sheetInfo)
        {
            Dictionary<string, object> m_DataOutput = new Dictionary<string, object>();
            using (var stream = new MemoryStream(data))
            {
                m_DataOutput = this.ReadCellData(data, sheetInfo);
            }
            return m_DataOutput;
        }
        public Dictionary<string, object> ReadCellData(Stream stream)
        {
            return this.ReadCellData(stream, null);
        }
        public Dictionary<string, object> ReadCellData(Stream stream, SheetInfo sheetInfo)
        {
            Dictionary<string, object> m_DataOutput = new Dictionary<string, object>();
            using (ExcelPackage m_ExcelPackage = new ExcelPackage())
            {
                // Open the Excel file and load it to the ExcelPackage
                m_ExcelPackage.Load(stream);

                ExcelWorksheet m_ExcelWorksheet = null;
                if (sheetInfo != null)
                    m_ExcelWorksheet = sheetInfo.SheetIndex > 0 ? m_ExcelPackage.Workbook.Worksheets[sheetInfo.SheetIndex] : m_ExcelPackage.Workbook.Worksheets[sheetInfo.SheetName];
                else
                    m_ExcelWorksheet = m_ExcelPackage.Workbook.Worksheets.FirstOrDefault();

                if (m_ExcelWorksheet != null)
                {
                    //Get Define Name
                    foreach (var DefineName in m_ExcelPackage.Workbook.Names)
                    {
                        m_DataOutput.Add(DefineName.Name, m_ExcelWorksheet.Cells[DefineName.Start.Row, DefineName.Start.Column].Text);
                        DefineName.Dispose();
                    }

                    //Get Cells
                    foreach (var cell in m_ExcelWorksheet.Cells)
                        m_DataOutput.Add(cell.Address, cell.Value);
                }
                else
                {
                    throw new ArgumentException("Không tìm thấy Sheet tương ứng");
                }
            }
            return m_DataOutput;
        }

        public byte[] Export(Dictionary<string, string> dicEntities)
        {
            return Export(dicEntities, null);
        }
        public byte[] Export(Dictionary<string, string> dicEntities, SheetInfo sheetInfo)
        {
            byte[] m_OutputData = null;
            using (ExcelPackage m_ExcelPackage = new ExcelPackage())
            {
                // Open the Excel file and load it to the ExcelPackage
                using (var stream = new MemoryStream(this.TemplateFileData))
                {
                    m_ExcelPackage.Load(stream);
                }

                ExcelWorksheet m_ExcelWorksheet = null;
                if (sheetInfo != null)
                    m_ExcelWorksheet = sheetInfo.SheetIndex > 0 ? m_ExcelPackage.Workbook.Worksheets[sheetInfo.SheetIndex] : m_ExcelPackage.Workbook.Worksheets[sheetInfo.SheetName];
                else
                    m_ExcelWorksheet = m_ExcelPackage.Workbook.Worksheets.FirstOrDefault();

                if (m_ExcelWorksheet != null)
                {
                    using (var m_Cells = m_ExcelWorksheet.Cells)
                    {
                        foreach (var m_dicEntitie in dicEntities)
                        {
                            try
                            {
                                using (var DefineName = m_ExcelPackage.Workbook.Names[m_dicEntitie.Key])
                                {
                                    if (DefineName != null)
                                    {
                                        m_Cells[DefineName.Start.Row, DefineName.Start.Column].Value = m_dicEntitie.Value;
                                    }
                                }
                            }
                            catch (Exception) { }
                        }
                    }
                }
                else
                {
                    throw new ArgumentException("Không tìm thấy Sheet tương ứng");
                }

                m_OutputData = m_ExcelPackage.GetAsByteArray();
            }
            return m_OutputData;
        }
        public byte[] MergeRow(byte[] excelData, int[] mergeColum, int startRow, SheetInfo sheetInfo)
        {
            byte[] m_OutputData = null;
            using (ExcelPackage m_ExcelPackage = new ExcelPackage())
            {
                // Open the Excel file and load it to the ExcelPackage
                Stream stream = new MemoryStream(excelData);
                m_ExcelPackage.Load(stream);

                ExcelWorksheet m_ExcelWorksheet = null;
                if (sheetInfo != null)
                    m_ExcelWorksheet = sheetInfo.SheetIndex > 0 ? m_ExcelPackage.Workbook.Worksheets[sheetInfo.SheetIndex] : m_ExcelPackage.Workbook.Worksheets[sheetInfo.SheetName];
                else
                    m_ExcelWorksheet = m_ExcelPackage.Workbook.Worksheets.FirstOrDefault();

                if (m_ExcelWorksheet != null)
                {
                    for (int m = 0; m < mergeColum.Length; m++)
                    {
                        int startMarkRow = 1;
                        int endMarkRow = 1;
                        string currentValue = m_ExcelWorksheet.Cells[1, mergeColum[m], 1, mergeColum[m]].Value != null ? m_ExcelWorksheet.Cells[1, mergeColum[m], 1, mergeColum[m]].Value.ToString() : string.Empty;
                        startRow = startRow > m_ExcelWorksheet.Dimension.End.Row ? m_ExcelWorksheet.Dimension.End.Row : startRow;
                        for (int i = startMarkRow; i < m_ExcelWorksheet.Dimension.End.Row; i++)
                        {
                            try
                            {
                                if (m_ExcelWorksheet.Cells[i, mergeColum[m], i, mergeColum[m]].Value != null && currentValue != m_ExcelWorksheet.Cells[i, mergeColum[m], i, mergeColum[m]].Value.ToString())
                                {
                                    if (endMarkRow > startMarkRow)
                                        m_ExcelWorksheet.Cells[startMarkRow, mergeColum[m], endMarkRow, mergeColum[m]].Merge = true;
                                    startMarkRow = i;
                                    currentValue = m_ExcelWorksheet.Cells[i, mergeColum[m], i, mergeColum[m]].Value.ToString();
                                }
                                else
                                    endMarkRow = i;
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }

                }
                else
                {
                    throw new ArgumentException("Không tìm thấy Sheet tương ứng");
                }
                //m_ExcelPackage.Save();
                m_OutputData = m_ExcelPackage.GetAsByteArray();
            }
            return m_OutputData;
        }
        public byte[] MergeRow(byte[] excelData, int[] mergeColumn, int startRow, SheetInfo sheetInfo, int endRows = -1)
        {
            byte[] m_OutputData = null;
            using (ExcelPackage m_ExcelPackage = new ExcelPackage())
            {
                // Open the Excel file and load it to the ExcelPackage
                Stream stream = new MemoryStream(excelData);
                m_ExcelPackage.Load(stream);

                ExcelWorksheet m_ExcelWorksheet = null;
                if (sheetInfo != null)
                    m_ExcelWorksheet = sheetInfo.SheetIndex > 0 ? m_ExcelPackage.Workbook.Worksheets[sheetInfo.SheetIndex] : m_ExcelPackage.Workbook.Worksheets[sheetInfo.SheetName];
                else
                    m_ExcelWorksheet = m_ExcelPackage.Workbook.Worksheets.FirstOrDefault();

                if (m_ExcelWorksheet != null)
                {
                    if (endRows <= 0)
                        endRows = m_ExcelWorksheet.Dimension.End.Row;
                    for (int m = 0; m < mergeColumn.Length; m++)
                    {
                        int countMergeRow = 0;
                        for (int i = startRow + 1; i <= endRows; i++)
                        {
                            string currentValue = m_ExcelWorksheet.Cells[i, mergeColumn[m]].Value != null ? m_ExcelWorksheet.Cells[i, mergeColumn[m]].Value.ToString() : string.Empty;
                            string oldValue = m_ExcelWorksheet.Cells[i - 1, mergeColumn[m]].Value != null ? m_ExcelWorksheet.Cells[i - 1, mergeColumn[m]].Value.ToString() : string.Empty;
                            if (oldValue == currentValue && !string.IsNullOrEmpty(currentValue))
                                countMergeRow++;
                            else
                            {
                                if (countMergeRow > 0)
                                {
                                    m_ExcelWorksheet.Cells[i - 1 - countMergeRow, mergeColumn[m], i - 1, mergeColumn[m]].Merge = true;
                                    countMergeRow = 0;
                                }
                            }
                        }
                        if (countMergeRow > 0)
                            m_ExcelWorksheet.Cells[endRows - countMergeRow, mergeColumn[m], endRows, mergeColumn[m]].Merge = true;
                    }
                }
                else
                {
                    throw new ArgumentException("Không tìm thấy Sheet tương ứng");
                }
                //m_ExcelPackage.Save();
                m_OutputData = m_ExcelPackage.GetAsByteArray();
            }
            return m_OutputData;
        }

        public byte[] MergeColumn(byte[] excelData, int[] mergeRows, int startColumn, SheetInfo sheetInfo, int endRow = -1)
        {
            byte[] m_OutputData = null;
            using (ExcelPackage m_ExcelPackage = new ExcelPackage())
            {
                // Open the Excel file and load it to the ExcelPackage
                Stream stream = new MemoryStream(excelData);
                m_ExcelPackage.Load(stream);

                ExcelWorksheet m_ExcelWorksheet = null;
                if (sheetInfo != null)
                    m_ExcelWorksheet = sheetInfo.SheetIndex > 0 ? m_ExcelPackage.Workbook.Worksheets[sheetInfo.SheetIndex] : m_ExcelPackage.Workbook.Worksheets[sheetInfo.SheetName];
                else
                    m_ExcelWorksheet = m_ExcelPackage.Workbook.Worksheets.FirstOrDefault();

                if (m_ExcelWorksheet != null)
                {
                    if (endRow <= 0)
                        endRow = m_ExcelWorksheet.Dimension.End.Column;
                    for (int m = 0; m < mergeRows.Length; m++)
                    {
                        startColumn = startColumn > m_ExcelWorksheet.Dimension.End.Column ? m_ExcelWorksheet.Dimension.End.Column : startColumn;
                        for (int i = startColumn; i < endRow; i++)
                        {
                            try
                            {
                                string currentValue = m_ExcelWorksheet.Cells[mergeRows[m], i, mergeRows[m], i].Value != null ? m_ExcelWorksheet.Cells[mergeRows[m], i, mergeRows[m], i].Value.ToString() : string.Empty;
                                string nextValue = string.Empty;
                                if (i + 1 > m_ExcelWorksheet.Dimension.End.Column)
                                    break;

                                nextValue = m_ExcelWorksheet.Cells[mergeRows[m], i + 1, mergeRows[m], i + 1].Value != null ? m_ExcelWorksheet.Cells[mergeRows[m], i + 1, mergeRows[m], i + 1].Value.ToString() : string.Empty;

                                if (nextValue == currentValue && !string.IsNullOrEmpty(currentValue) && !string.IsNullOrEmpty(nextValue))
                                {
                                    m_ExcelWorksheet.Cells[mergeRows[m], i, mergeRows[m], i + 1].Merge = true;
                                    currentValue = nextValue;
                                }
                            }
                            catch (Exception)
                            {
                                throw new ArgumentException("Lỗi");
                            }
                        }
                    }

                }
                else
                {
                    throw new ArgumentException("Không tìm thấy Sheet tương ứng");
                }
                //m_ExcelPackage.Save();
                m_OutputData = m_ExcelPackage.GetAsByteArray();
            }
            return m_OutputData;
        }

        public byte[] InsertColum(int fromColumn, int totalColumnInsert, string[] headerText, string[] Fields, SheetInfo sheetInfo)
        {
            byte[] m_OutputData = null;
            using (ExcelPackage m_ExcelPackage = new ExcelPackage())
            {
                // Open the Excel file and load it to the ExcelPackage
                using (var stream = new MemoryStream(this.TemplateFileData))
                {
                    m_ExcelPackage.Load(stream);
                }
                ExcelWorksheet m_ExcelWorksheet = null;
                if (sheetInfo != null)
                    m_ExcelWorksheet = sheetInfo.SheetIndex > 0 ? m_ExcelPackage.Workbook.Worksheets[sheetInfo.SheetIndex] : m_ExcelPackage.Workbook.Worksheets[sheetInfo.SheetName];
                else
                    m_ExcelWorksheet = m_ExcelPackage.Workbook.Worksheets.FirstOrDefault();

                if (m_ExcelWorksheet != null)
                {
                    m_ExcelWorksheet.InsertColumn(fromColumn, totalColumnInsert);
                    try
                    {
                        if (headerText.Length > 0)
                        {
                            // header text
                            for (int i = fromColumn; i < fromColumn + totalColumnInsert; i++)
                            {
                                m_ExcelWorksheet.Cells[1, i].Value = string.Format("{0}", headerText[i - fromColumn]);
                            }
                        }
                        // Filter data 
                        if (Fields.Length > 0)
                        {
                            for (int i = fromColumn; i < fromColumn + totalColumnInsert; i++)
                            {
                                m_ExcelWorksheet.Cells[2, i].Value = Fields[i - fromColumn];
                            }
                        }

                    }
                    catch (Exception)
                    {
                    }
                }
                else
                {
                    throw new ArgumentException("Không tìm thấy Sheet tương ứng");
                }
                m_OutputData = m_ExcelPackage.GetAsByteArray();
            }
            return m_OutputData;
        }

        public byte[] InsertColum(int fromColumn, int totalColumnInsert, string[] headerText, string[] Fields, SheetInfo sheetInfo, int FromRow = 1, int IndexRangeColumn = 1)
        {
            byte[] m_OutputData = null;
            using (ExcelPackage m_ExcelPackage = new ExcelPackage())
            {
                // Open the Excel file and load it to the ExcelPackage
                using (var stream = new MemoryStream(this.TemplateFileData))
                {
                    m_ExcelPackage.Load(stream);
                }
                ExcelWorksheet m_ExcelWorksheet = null;
                if (sheetInfo != null)
                    m_ExcelWorksheet = sheetInfo.SheetIndex > 0 ? m_ExcelPackage.Workbook.Worksheets[sheetInfo.SheetIndex] : m_ExcelPackage.Workbook.Worksheets[sheetInfo.SheetName];
                else
                    m_ExcelWorksheet = m_ExcelPackage.Workbook.Worksheets.FirstOrDefault();

                if (m_ExcelWorksheet != null)
                {
                    m_ExcelWorksheet.InsertColumn(fromColumn, totalColumnInsert);
                    try
                    {
                        for (int i = 0; i < Fields.Count(); i++)
                        {
                            m_ExcelWorksheet.Cells[FromRow + i, i].Value = string.Format("{0}", headerText[i - fromColumn]);
                            m_ExcelWorksheet.Cells[FromRow + i + IndexRangeColumn, i].Value = Fields[i - fromColumn];
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                else
                {
                    throw new ArgumentException("Không tìm thấy Sheet tương ứng");
                }
                m_OutputData = m_ExcelPackage.GetAsByteArray();
            }
            return m_OutputData;
        }

        public byte[] DeleteColumn(int columnFrom, int columns, SheetInfo sheetInfo)
        {
            byte[] m_OutputData = null;
            using (ExcelPackage m_ExcelPackage = new ExcelPackage())
            {
                // Open the Excel file and load it to the ExcelPackage
                using (var stream = new MemoryStream(this.TemplateFileData))
                {
                    m_ExcelPackage.Load(stream);
                }
                ExcelWorksheet m_ExcelWorksheet = null;
                if (sheetInfo != null)
                    m_ExcelWorksheet = sheetInfo.SheetIndex > 0 ? m_ExcelPackage.Workbook.Worksheets[sheetInfo.SheetIndex] : m_ExcelPackage.Workbook.Worksheets[sheetInfo.SheetName];
                else
                    m_ExcelWorksheet = m_ExcelPackage.Workbook.Worksheets.FirstOrDefault();

                if (m_ExcelWorksheet != null)
                {
                    m_ExcelWorksheet.DeleteColumn(columnFrom, columns);
                    for (int i = columnFrom; i <= columns; i++)
                        m_ExcelWorksheet.Column(i).Hidden = true;
                }
                else
                    throw new ArgumentException("Không tìm thấy Sheet tương ứng");
                m_OutputData = m_ExcelPackage.GetAsByteArray();
            }
            return m_OutputData;
        }

        public byte[] DeleteColumn(int columns, SheetInfo sheetInfo)
        {
            byte[] m_OutputData = null;
            using (ExcelPackage m_ExcelPackage = new ExcelPackage())
            {
                // Open the Excel file and load it to the ExcelPackage
                using (var stream = new MemoryStream(this.TemplateFileData))
                {
                    m_ExcelPackage.Load(stream);
                }
                ExcelWorksheet m_ExcelWorksheet = null;
                if (sheetInfo != null)
                    m_ExcelWorksheet = sheetInfo.SheetIndex > 0 ? m_ExcelPackage.Workbook.Worksheets[sheetInfo.SheetIndex] : m_ExcelPackage.Workbook.Worksheets[sheetInfo.SheetName];
                else
                    m_ExcelWorksheet = m_ExcelPackage.Workbook.Worksheets.FirstOrDefault();

                if (m_ExcelWorksheet != null)
                {
                    m_ExcelWorksheet.DeleteColumn(columns);
                    m_ExcelWorksheet.Column(columns).Hidden = true;
                }
                else
                    throw new ArgumentException("Không tìm thấy Sheet tương ứng");
                m_OutputData = m_ExcelPackage.GetAsByteArray();
            }
            return m_OutputData;
        }

        public byte[] HideColumn(int columnFrom, int columns, SheetInfo sheetInfo)
        {
            byte[] m_OutputData = null;
            using (ExcelPackage m_ExcelPackage = new ExcelPackage())
            {
                // Open the Excel file and load it to the ExcelPackage
                using (var stream = new MemoryStream(this.TemplateFileData))
                {
                    m_ExcelPackage.Load(stream);
                }
                ExcelWorksheet m_ExcelWorksheet = null;
                if (sheetInfo != null)
                    m_ExcelWorksheet = sheetInfo.SheetIndex > 0 ? m_ExcelPackage.Workbook.Worksheets[sheetInfo.SheetIndex] : m_ExcelPackage.Workbook.Worksheets[sheetInfo.SheetName];
                else
                    m_ExcelWorksheet = m_ExcelPackage.Workbook.Worksheets.FirstOrDefault();

                if (m_ExcelWorksheet != null)
                {
                    var m_ColumnTo = (columnFrom + columns);
                    for (int i = columnFrom; i < m_ColumnTo; i++)
                        m_ExcelWorksheet.Column(i).Hidden = true;
                }
                else
                    throw new ArgumentException("Không tìm thấy Sheet tương ứng");
                m_OutputData = m_ExcelPackage.GetAsByteArray();
            }
            return m_OutputData;
        }

        public byte[] FormatTemplate(FormatStyle formatStyle, SheetInfo sheetInfo)
        {
            byte[] m_OutputData = null;
            using (ExcelPackage m_ExcelPackage = new ExcelPackage())
            {
                // Open the Excel file and load it to the ExcelPackage
                using (var stream = new MemoryStream(this.TemplateFileData))
                {
                    m_ExcelPackage.Load(stream);
                }
                ExcelWorksheet m_ExcelWorksheet = null;
                if (sheetInfo != null)
                    m_ExcelWorksheet = sheetInfo.SheetIndex > 0 ? m_ExcelPackage.Workbook.Worksheets[sheetInfo.SheetIndex] : m_ExcelPackage.Workbook.Worksheets[sheetInfo.SheetName];
                else
                    m_ExcelWorksheet = m_ExcelPackage.Workbook.Worksheets.FirstOrDefault();

                if (m_ExcelWorksheet != null)
                {
                    //Get the final row for the column in the worksheet
                    int finalrows = m_ExcelWorksheet.Dimension.End.Row;
                    //Convert the range to the color Red
                    var allCells = m_ExcelWorksheet.Cells[1, 1, m_ExcelWorksheet.Dimension.End.Row, m_ExcelWorksheet.Dimension.End.Column];
                    allCells.Style.WrapText = formatStyle.WrapText;
                    if (formatStyle.AutoFitColumns)
                        allCells.AutoFitColumns();
                    var cellFont = allCells.Style.Font;
                    if (formatStyle.Font != null)
                        cellFont.SetFromFont(formatStyle.Font);
                }
                else
                    throw new ArgumentException("Không tìm thấy Sheet tương ứng");
                m_OutputData = m_ExcelPackage.GetAsByteArray();
            }
            return m_OutputData;
        }

        public byte[] WrapText(SheetInfo sheetInfo)
        {
            byte[] m_OutputData = null;
            using (ExcelPackage m_ExcelPackage = new ExcelPackage())
            {
                // Open the Excel file and load it to the ExcelPackage
                using (var stream = new MemoryStream(this.TemplateFileData))
                {
                    m_ExcelPackage.Load(stream);
                }
                ExcelWorksheet m_ExcelWorksheet = null;
                if (sheetInfo != null)
                    m_ExcelWorksheet = sheetInfo.SheetIndex > 0 ? m_ExcelPackage.Workbook.Worksheets[sheetInfo.SheetIndex] : m_ExcelPackage.Workbook.Worksheets[sheetInfo.SheetName];
                else
                    m_ExcelWorksheet = m_ExcelPackage.Workbook.Worksheets.FirstOrDefault();

                if (m_ExcelWorksheet != null)
                {
                    //Get the final row for the column in the worksheet
                    int finalrows = m_ExcelWorksheet.Dimension.End.Row;
                    //Convert into a string for the range.
                    //Convert the range to the color Red
                    var allCells = m_ExcelWorksheet.Cells[1, 1, m_ExcelWorksheet.Dimension.End.Row, m_ExcelWorksheet.Dimension.End.Column];
                    var cellFont = allCells.Style.WrapText = true;
                }
                else
                    throw new ArgumentException("Không tìm thấy Sheet tương ứng");
                m_OutputData = m_ExcelPackage.GetAsByteArray();
            }
            return m_OutputData;
        }

        public byte[] CopySheet(SheetInfo sheetTemplate, SheetInfo sheetNew)
        {
            //Prepare template excel
            using (ExcelPackage m_ExcelPackage = new ExcelPackage())
            {
                using (var m_MemoryStream = new MemoryStream(this.TemplateFileData))
                {
                    m_ExcelPackage.Load(m_MemoryStream);
                }
                //Copy Range
                if (m_ExcelPackage.Workbook.Worksheets[sheetNew.SheetName] == null)
                    m_ExcelPackage.Workbook.Worksheets.Copy(sheetTemplate.SheetName, sheetNew.SheetName);

                return m_ExcelPackage.GetAsByteArray();
            }
        }

        public byte[] DeleteSheet(SheetInfo sheetDetele)
        {
            //Prepare template excel
            using (ExcelPackage m_ExcelPackage = new ExcelPackage())
            {
                using (var m_MemoryStream = new MemoryStream(this.TemplateFileData))
                {
                    m_ExcelPackage.Load(m_MemoryStream);
                }
                //delete sheet
                if (!string.IsNullOrWhiteSpace(sheetDetele.SheetName))
                    m_ExcelPackage.Workbook.Worksheets.Delete(sheetDetele.SheetName);
                else if (sheetDetele.SheetIndex > 0)
                    m_ExcelPackage.Workbook.Worksheets.Delete(sheetDetele.SheetIndex);
                return m_ExcelPackage.GetAsByteArray();
            }
        }

        public byte[] AddImage(string imageName, string address, int height, int width, byte[] imageBytes)
        {
            return AddImage(imageName, null, address, height, width, imageBytes);
        }
        public byte[] AddImage(string imageName, SheetInfo sheetInfo, string address, int height, int width, byte[] imageBytes)
        {
            byte[] m_DataOutput = null;
            using (ExcelPackage m_ExcelPackage = new ExcelPackage())
            {
                // Open the Excel file and load it to the ExcelPackage
                using (var stream = new MemoryStream(this.TemplateFileData))
                {
                    m_ExcelPackage.Load(stream);
                }

                ExcelWorksheet m_ExcelWorksheet = null;
                if (sheetInfo != null)
                    m_ExcelWorksheet = sheetInfo.SheetIndex > 0 ? m_ExcelPackage.Workbook.Worksheets[sheetInfo.SheetIndex] : m_ExcelPackage.Workbook.Worksheets[sheetInfo.SheetName];
                else
                    m_ExcelWorksheet = m_ExcelPackage.Workbook.Worksheets.FirstOrDefault();

                if (m_ExcelWorksheet != null)
                {
                    Bitmap image;
                    using (var img = new MemoryStream(imageBytes))
                    {
                        image = new Bitmap(img);
                        ExcelPicture excelImage = null;
                        if (image != null)
                        {
                            using (var DefineName = m_ExcelPackage.Workbook.Names[address])
                            {
                                if (DefineName != null)
                                {
                                    excelImage = m_ExcelWorksheet.Drawings.AddPicture(imageName, image);
                                    excelImage.From.Column = DefineName.Start.Column;
                                    excelImage.From.Row = DefineName.Start.Row;
                                    excelImage.SetSize(width, height);
                                    // 2x2 px space for better alignment
                                    excelImage.From.ColumnOff = Pixel2MTU(2);
                                    excelImage.From.RowOff = Pixel2MTU(2);
                                }
                            }
                        }
                    }
                }
                else
                {
                    throw new ArgumentException("Không tìm thấy Sheet tương ứng");
                }

                m_DataOutput = m_ExcelPackage.GetAsByteArray();
            }
            return m_DataOutput;
        }

        private ExcelWorksheet GetWorkSheet(ExcelPackage excelPackage, SheetInfo sheetInfo)
        {
            ExcelWorksheet m_ExcelWorksheet = null;
            if (sheetInfo != null)
                m_ExcelWorksheet = sheetInfo.SheetIndex > 0 ? excelPackage.Workbook.Worksheets[sheetInfo.SheetIndex] : excelPackage.Workbook.Worksheets[sheetInfo.SheetName];
            else
                m_ExcelWorksheet = excelPackage.Workbook.Worksheets.FirstOrDefault();
            return m_ExcelWorksheet;
        }

        #endregion

        #region Utilities
        public int Pixel2MTU(int pixels)
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

    public class ExcelUtilityExtension : ExcelUtility
    {
        #region Constructors

        #endregion

        #region Properties

        // private byte[] OutputData { get; set; }

        #endregion

        #region Methods
        public byte[] Export<TEntity>(IList<TEntity> entities, SheetInfo sheetInfo, SheetInfo paramSheetInfo, RangeInfo rangeInfo, bool insertNewRows = true, bool fillStaticData = false) where TEntity : class
        {
            //1. Read Config
            this.ReadConfig(new SheetInfo[] { sheetInfo, paramSheetInfo });

            //2. Fill Paremter
            this.FillParameter(paramSheetInfo);

            //3. Export
            this.FillData(entities, sheetInfo, insertNewRows, fillStaticData);

            return this.OutputData;
        }

        private void ReadConfig(SheetInfo[] sheetInfo)
        {
            this.ConfigInfo = new ConfigInfo();

            //1. Read data file
            using (ExcelPackage m_ExcelPackage = new ExcelPackage())
            {
                //Open Excel + Get WorkSheet
                using (var m_MemoryStream = new MemoryStream(this.TemplateFileData))
                {
                    m_ExcelPackage.Load(m_MemoryStream);
                }
                for (int i = 0; i < sheetInfo.Length; i++)
                {
                    ExcelWorksheet m_ExcelWorksheet = null;
                    if (sheetInfo != null)
                        m_ExcelWorksheet = sheetInfo[i].SheetIndex > 0 ? m_ExcelPackage.Workbook.Worksheets[sheetInfo[i].SheetIndex] : m_ExcelPackage.Workbook.Worksheets[sheetInfo[i].SheetName];
                    else
                        m_ExcelWorksheet = m_ExcelPackage.Workbook.Worksheets.FirstOrDefault();
                    if (m_ExcelWorksheet != null)
                    {
                        //Prepare Template
                        if (PrepareTemplate != null)
                            PrepareTemplate(m_ExcelWorksheet);

                        //Get Config
                        var m_Dimension = m_ExcelWorksheet.Dimension;
                        var m_Cells = m_ExcelWorksheet.Cells;
                        RangeInfo rangeInfo = new RangeInfo()
                        {
                            FromColumn = 1,
                            ToColumn = m_Dimension.Columns,
                            FromRow = 1,
                            ToRow = m_Dimension.Rows
                        };

                        for (int m_RowIndex = rangeInfo.FromRow; m_RowIndex <= rangeInfo.ToRow; m_RowIndex++)
                        {
                            for (int m_ColumnIndex = rangeInfo.FromColumn; m_ColumnIndex <= rangeInfo.ToColumn; m_ColumnIndex++)
                            {
                                var m_Cell = m_Cells[m_RowIndex, m_ColumnIndex];
                                string m_Text = m_Cell.Text;

                                var m_FieldInfo = this.ParseConfig(m_Text);
                                if (m_FieldInfo != null)
                                {
                                    m_FieldInfo.ExcelAddress = m_Cell.Address;
                                    m_FieldInfo.ExcelRow = m_RowIndex;
                                    m_FieldInfo.ExcelColumn = m_ColumnIndex;
                                    this.ConfigInfo.Fields.Add(m_FieldInfo);
                                }
                            }
                        }
                    }
                }
                this.TemplateFileData = m_ExcelPackage.GetAsByteArray();
            }
        }

        private void FillParameter(SheetInfo sheetInfo)
        {
            using (ExcelPackage m_ExcelPackage = new ExcelPackage())
            {
                using (var m_MemoryStream = new MemoryStream(this.TemplateFileData))
                {
                    m_ExcelPackage.Load(m_MemoryStream);
                }
                ExcelWorksheet m_ExcelWorksheet = null;
                if (sheetInfo != null)
                    m_ExcelWorksheet = sheetInfo.SheetIndex > 0 ? m_ExcelPackage.Workbook.Worksheets[sheetInfo.SheetIndex] : m_ExcelPackage.Workbook.Worksheets[sheetInfo.SheetName];
                else
                    m_ExcelWorksheet = m_ExcelPackage.Workbook.Worksheets.FirstOrDefault();
                if (m_ExcelWorksheet != null)
                {
                    using (var m_Cells = m_ExcelWorksheet.Cells)
                    {
                        FieldInfo[] m_FieldInfos = this.ConfigInfo.Fields.Where(f => f.Type == KeyType_Parameter).ToArray();
                        foreach (var m_FieldInfo in m_FieldInfos)
                        {
                            object m_Value = string.Empty;
                            if (this.ParameterData.TryGetValue(m_FieldInfo.Name, out m_Value))
                            {
                                using (var m_Cell = m_Cells[m_FieldInfo.ExcelAddress])
                                {
                                    m_Cell.Value = m_Value;

                                    if (AfterFillParameter != null)
                                        AfterFillParameter(m_ExcelWorksheet, m_Cell, m_FieldInfo);
                                }
                            }
                        }
                    }

                    this.OutputData = m_ExcelPackage.GetAsByteArray();
                }
            };
        }

        private void FillData<TEntity>(IList<TEntity> entities, SheetInfo sheetInfo, bool insertNewRows = true, bool fillStaticData = false) where TEntity : class
        {
            using (ExcelPackage m_ExcelPackage = new ExcelPackage())
            {
                using (var m_MemoryStream = new MemoryStream(this.OutputData))
                {
                    m_ExcelPackage.Load(m_MemoryStream);
                }
                ExcelWorksheet m_ExcelWorksheet = null;
                if (sheetInfo != null)
                    m_ExcelWorksheet = sheetInfo.SheetIndex > 0 ? m_ExcelPackage.Workbook.Worksheets[sheetInfo.SheetIndex] : m_ExcelPackage.Workbook.Worksheets[sheetInfo.SheetName];
                else
                    m_ExcelWorksheet = m_ExcelPackage.Workbook.Worksheets.FirstOrDefault();

                if (m_ExcelWorksheet != null)
                {
                    using (var m_Cells = m_ExcelWorksheet.Cells)
                    {
                        FieldInfo[] m_FieldInfos = this.ConfigInfo.Fields.Where(f => f.Type == KeyType_Field).ToArray();
                        if (m_FieldInfos.Length > 0)
                        {
                            int m_RowBeginIndex = m_FieldInfos.FirstOrDefault().ExcelRow;
                            //Insert Zone
                            if (insertNewRows)
                                m_ExcelWorksheet.InsertRow(m_RowBeginIndex + 1, entities.Count - 1, m_RowBeginIndex);

                            //Fill
                            int m_RowIndex = m_RowBeginIndex;
                            Type m_EntityType = typeof(TEntity);
                            foreach (var m_Entity in entities)
                            {
                                foreach (var m_FieldInfo in m_FieldInfos)
                                {
                                    if (fillStaticData)
                                    {
                                        var m_Value = ReflectorUtility.FollowPropertyPath(m_Entity, m_FieldInfo.Name);
                                        m_Cells[m_FieldInfo.ExcelRow, m_FieldInfo.ExcelColumn].Value = m_Value;
                                    }
                                    else
                                    {
                                        var m_Value = ReflectorUtility.FollowPropertyPath(m_Entity, m_FieldInfo.Name);
                                        m_Cells[m_RowIndex, m_FieldInfo.ExcelColumn].Value = m_Value;
                                    }
                                }
                                m_RowIndex++;
                            }

                            if (AfterFillAllData != null)
                                this.AfterFillAllData(m_ExcelWorksheet);
                        }
                    }

                    this.OutputData = m_ExcelPackage.GetAsByteArray();
                }
            };
        }

        public byte[] SetChartData(SheetInfo sheetData, SheetInfo sheetChart, RangeInfo rangeSerie, RangeInfo rangeXSerie, string[] chartName)
        {
            using (ExcelPackage m_ExcelPackage = new ExcelPackage())
            {
                using (var m_MemoryStream = new MemoryStream(this.OutputData))
                {
                    m_ExcelPackage.Load(m_MemoryStream);
                }
                ExcelWorksheet m_ExcelWorksheetData = null;
                ExcelWorksheet m_ExcelWorksheetReport = null;
                if (sheetData != null)
                    m_ExcelWorksheetData = sheetData.SheetIndex > 0 ? m_ExcelPackage.Workbook.Worksheets[sheetData.SheetIndex] : m_ExcelPackage.Workbook.Worksheets[sheetData.SheetName];
                else
                    m_ExcelWorksheetData = m_ExcelPackage.Workbook.Worksheets.FirstOrDefault();

                if (sheetChart != null)
                    m_ExcelWorksheetReport = sheetChart.SheetIndex > 0 ? m_ExcelPackage.Workbook.Worksheets[sheetChart.SheetIndex] : m_ExcelPackage.Workbook.Worksheets[sheetChart.SheetName];
                else
                    m_ExcelWorksheetReport = m_ExcelPackage.Workbook.Worksheets.FirstOrDefault();

                if (m_ExcelWorksheetData != null && m_ExcelWorksheetReport != null)
                {
                    for (int i = 0; i < chartName.Length; i++)
                    {
                        try
                        {
                            var m_pieChart = m_ExcelWorksheetReport.Drawings[chartName[i]] as ExcelBarChart;
                            for (int s = 0; s < m_pieChart.Series.Count; s++)
                            {
                                m_pieChart.Series.Delete(s);
                            }
                            m_pieChart.Series.Add(m_ExcelWorksheetData.Cells[rangeSerie.FromRow, rangeSerie.FromColumn, rangeSerie.ToRow, rangeSerie.ToColumn].FullAddress, m_ExcelWorksheetData.Cells[rangeXSerie.FromRow, rangeXSerie.FromColumn, rangeXSerie.ToRow, rangeXSerie.ToColumn].FullAddress);
                        }
                        catch (Exception)
                        {

                        }

                    }
                }
                this.OutputData = m_ExcelPackage.GetAsByteArray();
            }
            return this.OutputData;
        }

        public byte[] SetChartData(SheetInfo sheetData, SheetInfo sheetChart, string serieAddress, string xSerieAddress, string[] chartName)
        {
            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                using (MemoryStream memoryStream = new MemoryStream(this.OutputData))
                {
                    excelPackage.Load(memoryStream);
                }
                ExcelWorksheet excelWorksheetData = null;
                ExcelWorksheet excelWorksheetReport = null;
                if (sheetData != null)
                    excelWorksheetData = sheetData.SheetIndex > 0 ? excelPackage.Workbook.Worksheets[sheetData.SheetIndex] : excelPackage.Workbook.Worksheets[sheetData.SheetName];
                else
                    excelWorksheetData = excelPackage.Workbook.Worksheets.FirstOrDefault();

                if (sheetChart != null)
                    excelWorksheetReport = sheetChart.SheetIndex > 0 ? excelPackage.Workbook.Worksheets[sheetChart.SheetIndex] : excelPackage.Workbook.Worksheets[sheetChart.SheetName];
                else
                    excelWorksheetReport = excelPackage.Workbook.Worksheets.FirstOrDefault();

                if (excelWorksheetData != null && excelWorksheetReport != null)
                {
                    for (int i = 0; i < chartName.Length; i++)
                    {
                        try
                        {
                            ExcelBarChart excelBarChart = excelWorksheetReport.Drawings[chartName[i]] as ExcelBarChart;
                            for (int s = 0; s < excelBarChart.Series.Count; s++)
                            {
                                excelBarChart.Series.Delete(s);
                            }
                            excelBarChart.Series.Add(serieAddress, xSerieAddress);
                        }
                        catch (Exception)
                        {

                        }

                    }
                }
                this.OutputData = excelPackage.GetAsByteArray();
            }
            return this.OutputData;
        }

        public byte[] DeleteRows(SheetInfo sheetData, RangeInfo rangeDelete)
        {
            using (ExcelPackage m_ExcelPackage = new ExcelPackage())
            {
                using (var m_MemoryStream = new MemoryStream(this.OutputData))
                {
                    m_ExcelPackage.Load(m_MemoryStream);
                }
                ExcelWorksheet m_ExcelWorksheetData = null;
                if (sheetData != null)
                    m_ExcelWorksheetData = sheetData.SheetIndex > 0 ? m_ExcelPackage.Workbook.Worksheets[sheetData.SheetIndex] : m_ExcelPackage.Workbook.Worksheets[sheetData.SheetName];
                else
                    m_ExcelWorksheetData = m_ExcelPackage.Workbook.Worksheets.FirstOrDefault();

                m_ExcelWorksheetData.DeleteRow(rangeDelete.FromRow, rangeDelete.ToRow - rangeDelete.FromRow, true);

                this.OutputData = m_ExcelPackage.GetAsByteArray();
            }
            return this.OutputData;
        }

        public byte[] DeleteRows(byte[] data, SheetInfo sheetData, RangeInfo rangeDelete)
        {
            using (ExcelPackage m_ExcelPackage = new ExcelPackage())
            {
                using (var m_MemoryStream = new MemoryStream(data))
                {
                    m_ExcelPackage.Load(m_MemoryStream);
                }
                ExcelWorksheet m_ExcelWorksheetData = null;
                if (sheetData != null)
                    m_ExcelWorksheetData = sheetData.SheetIndex > 0 ? m_ExcelPackage.Workbook.Worksheets[sheetData.SheetIndex] : m_ExcelPackage.Workbook.Worksheets[sheetData.SheetName];
                else
                    m_ExcelWorksheetData = m_ExcelPackage.Workbook.Worksheets.FirstOrDefault();

                m_ExcelWorksheetData.DeleteRow(rangeDelete.FromRow, rangeDelete.ToRow - rangeDelete.FromRow, true);

                this.OutputData = m_ExcelPackage.GetAsByteArray();
            }
            return this.OutputData;
        }

        public byte[] DeleteChart(SheetInfo sheetData, string[] chartName)
        {
            using (ExcelPackage m_ExcelPackage = new ExcelPackage())
            {
                using (var m_MemoryStream = new MemoryStream(this.OutputData))
                {
                    m_ExcelPackage.Load(m_MemoryStream);
                }
                ExcelWorksheet m_ExcelWorksheetData = null;
                if (sheetData != null)
                    m_ExcelWorksheetData = sheetData.SheetIndex > 0 ? m_ExcelPackage.Workbook.Worksheets[sheetData.SheetIndex] : m_ExcelPackage.Workbook.Worksheets[sheetData.SheetName];
                else
                    m_ExcelWorksheetData = m_ExcelPackage.Workbook.Worksheets.FirstOrDefault();

                for (int i = 0; i < chartName.Length; i++)
                {
                    try
                    {
                        m_ExcelWorksheetData.Drawings.Remove(chartName[i]);
                    }
                    catch (Exception)
                    {

                    }
                }

                this.OutputData = m_ExcelPackage.GetAsByteArray();
            }
            return this.OutputData;
        }

        public byte[] SetPositionChart(SheetInfo sheetData, string chartName, RangeInfo rangeInfo)
        {
            using (ExcelPackage m_ExcelPackage = new ExcelPackage())
            {
                using (var m_MemoryStream = new MemoryStream(this.OutputData))
                {
                    m_ExcelPackage.Load(m_MemoryStream);
                }
                ExcelWorksheet m_ExcelWorksheetData = null;
                if (sheetData != null)
                    m_ExcelWorksheetData = sheetData.SheetIndex > 0 ? m_ExcelPackage.Workbook.Worksheets[sheetData.SheetIndex] : m_ExcelPackage.Workbook.Worksheets[sheetData.SheetName];
                else
                    m_ExcelWorksheetData = m_ExcelPackage.Workbook.Worksheets.FirstOrDefault();

                try
                {
                    var m_Chart = m_ExcelWorksheetData.Drawings[chartName] as ExcelBarChart;
                    m_Chart.SetPosition(rangeInfo.FromRow, 0, rangeInfo.FromColumn, 0);
                }
                catch (Exception)
                {

                }

                this.OutputData = m_ExcelPackage.GetAsByteArray();
            }
            return this.OutputData;
        }

        /// <summary>
        /// push datatable to excel
        /// </summary>
        /// <param name="excelData"></param>
        /// <param name="mergeColum"></param>
        /// <param name="startRow"></param>
        /// <param name="sheetInfo"></param>
        /// <returns></returns>
        public byte[] SetDatatable(byte[] m_Data, SheetInfo sheetData, int startRow, int startColumn, DataTable dataTable, RangeInfo Range = null)
        {
            byte[] m_OutputData = null;
            using (ExcelPackage pck = new ExcelPackage())
            {
                using (var m_MemoryStream = new MemoryStream(m_Data))
                {
                    pck.Load(m_MemoryStream);
                }
                ExcelWorksheet ws = pck.Workbook.Worksheets[sheetData.SheetName];
                if (pck.Workbook.Worksheets[sheetData.SheetName] == null)
                {
                    ws = pck.Workbook.Worksheets.Add(sheetData.SheetName);
                }
                ws.Cells[startRow, startColumn].LoadFromDataTable(dataTable, true);

                if (Range != null && Range.FromRow > 0 && Range.ToRow > 0 && Range.ToColumn > 0)
                {
                    var modelTable = ws.Cells[Range.FromRow, 1, Range.ToRow, Range.ToColumn];
                    modelTable.Style.Font.Bold = true;
                    modelTable.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    modelTable.Style.Fill.BackgroundColor.SetColor(Range.BackGround);
                    modelTable.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    modelTable.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    modelTable.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    modelTable.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    modelTable.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    modelTable.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                }
                m_OutputData = pck.GetAsByteArray();
            }
            return m_OutputData;
        }

        public byte[] MergeCells(byte[] m_Data, SheetInfo sheetData, RangeInfo[] rangeInfo)
        {
            byte[] m_OutputData = null;
            using (ExcelPackage m_ExcelPackage = new ExcelPackage())
            {
                using (var m_MemoryStream = new MemoryStream(m_Data))
                {
                    m_ExcelPackage.Load(m_MemoryStream);
                }
                ExcelWorksheet m_ExcelWorksheetData = null;
                if (sheetData != null)
                    m_ExcelWorksheetData = sheetData.SheetIndex > 0 ? m_ExcelPackage.Workbook.Worksheets[sheetData.SheetIndex] : m_ExcelPackage.Workbook.Worksheets[sheetData.SheetName];
                else
                    m_ExcelWorksheetData = m_ExcelPackage.Workbook.Worksheets.FirstOrDefault();
                if (m_ExcelWorksheetData != null)
                {
                    foreach (var m_rangeInfo in rangeInfo)
                    {
                        m_ExcelWorksheetData.Cells[m_rangeInfo.FromRow, m_rangeInfo.FromColumn, m_rangeInfo.ToRow, m_rangeInfo.ToColumn].Merge = true;
                    }
                }
                m_OutputData = m_ExcelPackage.GetAsByteArray();
            }
            return m_OutputData;
        }

        #endregion

        #region Utilities
        //public int Pixel2MTU(int pixels)
        //{
        //    int mtus = pixels * 9525;
        //    return mtus;
        //}
        #endregion

        #region Constants

        //public const string Key_Start = "[[%";
        //public const string Key_End = "%]]";
        //public const string Key_Seperator = ":";

        //public const string KeyType_Parameter = "Parameter";
        //public const string KeyType_Field = "Field";

        #endregion
    }

    #region Extension class

    public class FormatStyle
    {
        public Font Font { set; get; }
        public bool WrapText { set; get; }
        public bool AutoFitColumns { set; get; }
    }

    public class ConfigInfo
    {
        public ConfigInfo()
        {
            this.Fields = new List<FieldInfo>();
        }
        public IList<FieldInfo> Fields { get; set; }
    }

    public class FieldInfo
    {
        public string Name { get; set; }
        public string ExcelAddress { get; set; }
        public int ExcelRow { get; set; }
        public int ExcelColumn { get; set; }
        public string Type { get; set; }
    }

    public class SheetInfo
    {
        public string SheetName { get; set; }
        public int SheetIndex { get; set; }
    }

    public class RangeInfo
    {
        public int FromRow { get; set; }
        public int ToRow { get; set; }
        public int FromColumn { get; set; }
        public int ToColumn { get; set; }
        public System.Drawing.Color BackGround { get; set; }
    }

    public class MergeAddress
    {
        public int Row { get; set; }
        public int Column { get; set; }
    }
    #endregion
}
