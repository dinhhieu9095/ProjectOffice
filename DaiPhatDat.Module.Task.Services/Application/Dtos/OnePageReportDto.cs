using DaiPhatDat.Core.Kernel.Mapper;
using DaiPhatDat.Module.Task.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaiPhatDat.Module.Task.Services
{
    public class OnePageReportDto
    {


        public List<StackedHeaderColumn> StackedHeaders { get; set; }
        public List<ReportColumn> Columns { get; set; }
        public List<string> Reports { get; set; }
        public List<string> Headers { get; set; }
    }
    public class StackedHeaderColumn
    {
        public string column { get; set; }
        public string headerText { get; set; }
        public string cssClass { get; set; }
        public string headerTextAlign { get; set; }
        public string textAlign { get; set; }
    }
    public class ReportColumn
    {
        public string field { get; set; }
        public string headerText { get; set; }
        public string headerTextAlign { get; set; }
        public string textAlign { get; set; }
        public string headerTemplateID { get; set; }
        public string width { get; set; }
    }
    public class ReportOnepageBase
    {
        public ShowType ShowType { get; set; }

        public string Id { get; set; }

        public string ProjectId { get; set; }

        public string ParentId { get; set; }

        public string NumberOf { get; set; }

        public string Name { get; set; }
        public string StatusName { get; set; }
        public string DayBefore { get; set; }
        public string DayAfter { get; set; }
        public DateTime? FromDate { get; set; }
        public string FinishDateText { get; set; }
        public string FromDateText
        {
            get
            {
                string fromDate = FromDate.HasValue ? FromDate.Value.ToString("dd/MM/yyyy") : string.Empty;
                string toDate = ToDate.HasValue ? ToDate.Value.ToString("dd/MM/yyyy") : string.Empty;
                if (!string.IsNullOrEmpty(toDate))
                {
                    return fromDate + " - " + toDate;
                }
                return fromDate;
            }
        }
        public DateTime? ToDate { get; set; }
        public string ToDateText
        {
            get; set;
        }
        public string AssignName { get; set; }
        public string PercentFinish { get; set; }
        public bool IsLate { get; set; }
        public ProjectStatusId StatusId { get; set; }
    }
    public class ReportOnepageWeekly : ReportOnepageBase
    {
        public string T2 { get; set; }
        public string T3 { get; set; }
        public string T4 { get; set; }
        public string T5 { get; set; }
        public string T6 { get; set; }
        public string T7 { get; set; }
        public string CN { get; set; }
    }
    public class ReportOnepageMonthly : ReportOnepageBase
    {
        public string D1 { get; set; }
        public string D2 { get; set; }
        public string D3 { get; set; }
        public string D4 { get; set; }
        public string D5 { get; set; }
        public string D6 { get; set; }
        public string D7 { get; set; }
        public string D8 { get; set; }
        public string D9 { get; set; }
        public string D10 { get; set; }
        public string D11 { get; set; }
        public string D12 { get; set; }
        public string D13 { get; set; }
        public string D14 { get; set; }
        public string D15 { get; set; }
        public string D16 { get; set; }
        public string D17 { get; set; }
        public string D18 { get; set; }
        public string D19 { get; set; }
        public string D20 { get; set; }
        public string D21 { get; set; }
        public string D22 { get; set; }
        public string D23 { get; set; }
        public string D24 { get; set; }
        public string D25 { get; set; }
        public string D26 { get; set; }
        public string D27 { get; set; }
        public string D28 { get; set; }
        public string D29 { get; set; }
        public string D30 { get; set; }
        public string D31 { get; set; }
    }
    public class ReportOnepageQuaterly : ReportOnepageBase
    {
        public string T1 { get; set; }
        public string T2 { get; set; }
        public string T3 { get; set; }
    }
    public class ReportOnepageAnnual : ReportOnepageBase
    {
        public string Th1 { get; set; }
        public string Th2 { get; set; }
        public string Th3 { get; set; }
        public string Th4 { get; set; }
        public string Th5 { get; set; }
        public string Th6 { get; set; }
        public string Th7 { get; set; }
        public string Th8 { get; set; }
        public string Th9 { get; set; }
        public string Th10 { get; set; }
        public string Th11 { get; set; }
        public string Th12 { get; set; }
    }
}