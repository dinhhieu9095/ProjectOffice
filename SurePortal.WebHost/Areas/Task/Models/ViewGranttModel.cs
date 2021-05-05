using DaiPhatDat.Module.Task.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DaiPhatDat.Module.Task.Web
{
    public class ViewGranttModel : ViewGranttQuaterlyModel
    {
        public string DayBeforeW { get; set; }
        public string DayAfterW { get; set; }
        public string DayBeforeM { get; set; }
        public string DayAfterM { get; set; }
        public string DayBeforeQ { get; set; }
        public string DayAfterQ { get; set; }
        public string DayBeforeY { get; set; }
        public string DayAfterY { get; set; }
    }
    public class ViewGranttQuaterlyModel : ViewGranttWeeklyModel
    {
        public string Q1 { get; set; }
        public string Q2 { get; set; }
        public string Q3 { get; set; }
    }
 
    public class ViewGranttWeeklyModel : ViewGranttMonthlyModel
    {
        public string T2 { get; set; }
        public string T3 { get; set; }
        public string T4 { get; set; }
        public string T5 { get; set; }
        public string T6 { get; set; }
        public string T7 { get; set; }
        public string CN { get; set; }
    }
    public class ViewGranttMonthlyModel : ViewGranttYearModel
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
    public class ViewGranttYearModel : FetchProjectsTasksResult
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