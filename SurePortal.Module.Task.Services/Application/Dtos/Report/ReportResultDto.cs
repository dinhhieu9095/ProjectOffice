using SurePortal.Module.Task.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurePortal.Module.Task.Services
{
    public class ReportResultDto
    {
        public ReportResultDto()
        {
            SumaryProjects = new List<SumaryDto>();
            SumaryProjects.Add(new SumaryDto() { Count = 0, Name = "Tổng số danh mục" });

            SumaryTasks = new List<SumaryDto>();
            SumaryTasks.Add(new SumaryDto() { Count = 0, Name = "Tổng số C.Việc" });
        }
        public List<ReportItemDto> Result { get; set; }
        public List<SumaryDto> SumaryProjects { get; set; }
        public List<SumaryDto> SumaryTasks { get; set; }
        
    }

    public enum ShowType
    {
        Project,
        Group,
        TaskItem,
        TaskItemAssign
    }


    public class SumaryDto
    {
        public string Name { get; set; }
        public int Total { get; set; }
        public int Count { get; set; }
        public int StatusId { get; set; }
        public double? Percent { get; set; }
        public double? PercentInDue { get; set; }
        public double? PercentOutDue { get; set; }
    }
}
