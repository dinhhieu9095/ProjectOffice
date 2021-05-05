using DaiPhatDat.Core.Kernel.Mapper;
using DaiPhatDat.Module.Task.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DaiPhatDat.Module.Task.Services
{
    public class TaskItemAssignDto : IMapping<TaskItemAssign>
    {
        public TaskItemAssignDto()
        {
            TaskItemAppraiseHistories = new HashSet<TaskItemAppraiseHistoryDto>();
            TaskItemKpis = new HashSet<TaskItemKpiDto>();
            TaskItemProcessHistories = new HashSet<TaskItemProcessHistoryDto>();
        }

        public Guid Id { get; set; }

        public Guid? TaskItemId { get; set; }

        public Guid? ProjectId { get; set; }

        public Guid? AssignTo { get; set; }
        public string AssignToFullName { get; set; }
        public string AssignToJobTitleName { get; set; }

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
        public string StatusName { get; set; }
        //Khó khăn, vướng mắc
        public string Problem { get; set; }

        public Guid? UserHandoverId { get; set; }

        public TaskItemDto TaskItem { get; set; }

        public TaskItemStatusDto TaskItemStatus { get; set; }

        public ICollection<TaskItemAppraiseHistoryDto> TaskItemAppraiseHistories { get; set; }

        public ICollection<TaskItemKpiDto> TaskItemKpis { get; set; }
        public List<Guid> AttachDelIds { get; set; }
        public ICollection<AttachmentDto> Attachments { get; set; }

        public ICollection<TaskItemProcessHistoryDto> TaskItemProcessHistories { get; set; }
        #region Extentions
        public bool IsDeleted { get; set; }

        public string Department { get; set; }
        public bool IsFullControl { get; set; }
        public bool IsAssignBy { get; set; }
        public bool IsExtend { get; set; }
        public string ExtendDateText { get; set; }
        public int ExtendDateTotal { get; set; }
        public string Description { get; set; }
        public string ActionText { get; set; }
        public ActionId? ActionId { get; set; }
        public Guid? ModifiedBy { get; set; }
        public int? JobTitleOrderNumber { get; set; }

        #endregion
    }
}