using DaiPhatDat.Core.Kernel.Mapper;
using DaiPhatDat.Module.Task.Services;
using System;
using System.Collections.Generic;

namespace DaiPhatDat.Module.Task.Web
{
    public class ReportFilterModel : IMapping<ReportFilterDto>
    {
        public Guid? DepartmentId { get; set; }

        public Guid? UserOfDepartmentId { get; set; }

        public string UserDepartmentText { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public bool IsReport { get; set; }

        public bool IsAssignTo { get; set; }

        public bool IsAssignBy { get; set; }
        public bool IsWhoExcute { get; set; }
        public bool IsWhoAssignBy { get; set; }
        public bool IsFullControl { get; set; }

        public int? TrackingProgress { get; set; }

        public int? TrackingStatus { get; set; }

        public int? TrackingCrucial { get; set; }

        public string Action { get; set; }
        public string Title { get; set; }
        public int TotalWeek { get; set; }
        public int CurrentWeek { get; set; }
        public int? Level { get; set; }
        public Guid? ProjectId { get; set; }
        public string ReportType { get; set; }
        public List<FetchProjectsTasksResult> Projects { get; set; }
    }
}