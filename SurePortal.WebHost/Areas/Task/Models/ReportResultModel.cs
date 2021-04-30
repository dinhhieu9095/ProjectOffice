using SurePortal.Core.Kernel.Mapper;
using SurePortal.Module.Task.Entities;
using SurePortal.Module.Task.Services;
using System;
using System.Collections.Generic;

namespace SurePortal.Module.Task.Web
{
    public class ReportResultModel : IMapping<ReportResultDto>
    {
        public ReportResultModel()
        {
            //SumaryProjects = new List<SumaryModel>();
            //SumaryProjects.Add(new SumaryModel() { Count = 0, Name = "Tổng số danh mục" });

            SumaryTasks = new List<SumaryModel>();
            SumaryTasks.Add(new SumaryModel() { Count = 0, Name = "Tổng số C.Việc" });
        }
        public List<ReportItemModel> Result { get; set; }
        //public List<SumaryModel> SumaryProjects { get; set; }
        public List<SumaryModel> SumaryTasks { get; set; }

    }

    public class SumaryModel : IMapping<SumaryDto>
    {
        public string Name { get; set; }
        public int Total { get; set; }
        public int Index { get; set; }
        public int Count { get; set; }
        public int StatusId { get; set; }
        public double? Percent { get; set; }
        public double? PercentInDue { get; set; }
        public double? PercentOutDue { get; set; }
    } 

    public class ReportItemModel : IMapping<ReportItemDto>
    {
        public ReportItemModel()
        {
            children = new List<ReportItemModel>();
        }

        public ShowType ShowType { get; set; }

        public string Id { get; set; }
        public bool IsDeleted { get; set; }

        public string ProjectId { get; set; }

        public string ParentId { get; set; }

        public string NumberOf { get; set; }

        public string Name { get; set; }
        public bool IsLate { get; set; }
        public string AssignName { get; set; }
        public string TaskItemPriorityName { get; set; }
        public string TaskItemCategory { get; set; }

        public string StatusName { get; set; }
        public ProjectStatusId StatusId { get; set; }
        public string NatureTask { get; set; }

        public string BlueprintTime { get; set; }

        public string ActualTime { get; set; }

        public string Progress { get; set; }

        public string AssignBy { get; set; }

        public string Assign { get; set; }

        public string LastResult { get; set; }
        public string ContentAppraise { get; set; }
        public string DepartmentId { get; set; }
        //Ghi chú
        public string Note { get; set; }
        public string NoteExcel { get; set; }


        public string ExtendDescription { get; set; }

        public string Problem { get; set; }

        public string Solution { get; set; }

        public int ChildrenCount { get; set; }

        public double? TaskPercentFinish { get; set; }
        public double? PercentFinish { get; set; }
        public string PercentFinishText
        {
            get
            {
                return PercentFinish.HasValue ? PercentFinish.ToString() : string.Empty;
            }
        }
        public double? ProjectPercentFinish { get; set; }
        public int? ProjectStatusId { get; set; }
        public int? TaskStatusId { get; set; }
        public double? AppraisePercentFinish { get; set; }
        public string AppraisePercentFinishText
        {
            get
            {
                return AppraisePercentFinish.HasValue ? AppraisePercentFinish.ToString() : string.Empty;
            }
        }
        //Điểm tiến độ CV
        public string AppraiseProcess { get; set; }
        public DateTime? FromDate { get; set; }
        public string FromDateText
        {
            get
            {
                return FromDate != null ? FromDate.Value.ToString("dd/MM/yyyy") : string.Empty;
            }
        }
        public DateTime? ToDate { get; set; }
        public int Level { get; set; }
        public string ToDateText
        {
            get
            {
                return ToDate != null ? ToDate.Value.ToString("dd/MM/yyyy") : string.Empty;
            }
        }
        public DateTime? FinishedDate { get; set; }
        public string FinishedDateText
        {
            get
            {
                return FinishedDate != null ? FinishedDate.Value.ToString("dd/MM/yyyy") : string.Empty;
            }
        }
        public string FinishedText
        {
            get
            {
                return (FinishedDate != null && FinishedDate.Value < DateTime.Now) ? "Trong hạn" : (FinishedDate != null && FinishedDate.Value > DateTime.Now ? "Quá hạn" : "");
            }
        }

        public IList<ReportItemModel> children { get; set; }
    }
}