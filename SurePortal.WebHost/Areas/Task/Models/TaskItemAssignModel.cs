using SurePortal.Core.Kernel.Mapper;
using SurePortal.Module.Task.Entities;
using SurePortal.Module.Task.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SurePortal.Module.Task.Web
{
    public class TaskItemAssignModel : IMapping<TaskItemAssignDto>
    {
        public TaskItemAssignModel()
        {
            TaskItemAppraiseHistories = new HashSet<TaskItemAppraiseHistoryModel>();
            TaskItemKpis = new HashSet<TaskItemKpiModel>();
            TaskItemProcessHistories = new HashSet<TaskItemProcessHistoryModel>();
        }

        public Guid Id { get; set; }

        public Guid? TaskItemId { get; set; }

        public Guid? ProjectId { get; set; }

        public Guid? AssignTo { get; set; }
        public string AssignToFullName { get; set; }
        public string AssignToJobTitleName { get; set; }
        public string Department { get; set; }

        public string LastResult { get; set; }

        public TaskItemStatusId? TaskItemStatusId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string ModifiedDateFormat { get; set; }

        public double? PercentFinish { get; set; }

        public double? AppraisePercentFinish { get; set; }
        
        public string AppraiseProcess { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public DateTime? FinishedDate { get; set; }

        public TaskType? TaskType { get; set; }

        public string AppraiseResult { get; set; }

        public int? AppraiseStatus { get; set; }

        public DateTime? ExtendDate { get; set; }

        public string PropertyExt { get; set; }

        public Guid? DepartmentId { get; set; }

        public Guid? AssignFollow { get; set; }

        public bool? HasRecentActivity { get; set; }
        //Nguyên nhân quá hạn
        public string ExtendDescription { get; set; }
        //Đề xuất
        public string Solution { get; set; }
        //Khó khăn, vướng mắc
        public string Problem { get; set; }

        public Guid? UserHandoverId { get; set; }

        public TaskItemModel TaskItemModel { get; set; }

        public TaskItemStatusModel TaskItemStatus { get; set; }

        public ICollection<TaskItemAppraiseHistoryModel> TaskItemAppraiseHistories { get; set; }

        public ICollection<TaskItemKpiModel> TaskItemKpis { get; set; }

        public ICollection<TaskItemProcessHistoryModel> TaskItemProcessHistories { get; set; }
        #region Extentions
        public bool IsFullControl { get; set; }
        public bool IsAssignBy { get; set; }
        public string Description { get; set; }
        public ActionId? ActionId { get; set; }
        public string ActionText { get; set; }
        public Guid? ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsExtend { get; set; }
        public int ExtendDateTotal { get; set; }
        public string ExtendDateText { get; set; }
        #endregion
    }
}