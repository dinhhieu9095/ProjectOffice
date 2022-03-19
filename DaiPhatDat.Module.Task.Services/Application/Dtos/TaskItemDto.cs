using DaiPhatDat.Core.Kernel.Mapper;
using DaiPhatDat.Module.Task.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DaiPhatDat.Module.Task.Services
{
    public class TaskItemDto : IMapping<TaskItem>
    {
        public TaskItemDto()
        {
            Children = new HashSet<TaskItemDto>();
            TaskItemAssigns = new List<TaskItemAssignDto>();
            TaskItemProcessHistories = new HashSet<TaskItemProcessHistoryDto>();
            TaskItemCategories = new List<string>();
            Attachments = new List<AttachmentDto>();
        }

        public Guid Id { get; set; }

        public Guid? ProjectId { get; set; }

        public string TaskName { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public DateTime? FinishedDate { get; set; }

        public TaskItemStatusId? TaskItemStatusId { get; set; }
        public string StatusName { get; set; }
        public string AssignByFullName { get; set; }
        public string DepartmentName { get; set; }
        public string JobTitleName { get; set; }

        public DateTime? CreatedDate { get; set; }

        public Guid? CreatedBy { get; set; }

        public Guid? AssignBy { get; set; }

        public Guid? ParentId { get; set; }

        public double? PercentFinish { get; set; }
        public double? AppraisePercentFinish { get; set; }
        public double? ProjectPercentFinish { get; set; }

        public TaskType? TaskType { get; set; }

        public bool? IsReport { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public Guid? ModifiedBy { get; set; }

        public string Conclusion { get; set; }

        public TaskItemPriorityId? TaskItemPriorityId { get; set; }

        public Guid? DepartmentId { get; set; }

        public string TaskItemCategory { get; set; }

        public bool? IsSecurity { get; set; }

        public bool? IsWeirdo { get; set; }

        public bool? HasRecentActivity { get; set; }

        public double? Weight { get; set; }

        public bool? IsGroupLabel { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsLate { get; set; }
        public int? NatureTaskId { get; set; }
        public int? Order { get; set; }
        public int TaskGroupType { get; set; }
        public bool IsAuto { get; set; }
        public bool IsAdminCategory { get; set; }
        public Guid? AdminCategoryId { get; set; }
        public ProjectDto Project { get; set; }

        public TaskItemDto Parent { get; set; }

        public TaskItemPriorityDto TaskItemPriority { get; set; }

        public TaskItemStatusDto TaskItemStatus { get; set; }

        public ICollection<TaskItemDto> Children { get; set; }

        public List<TaskItemAssignDto> TaskItemAssigns { get; set; }

        public ICollection<TaskItemProcessHistoryDto> TaskItemProcessHistories { get; set; }
        #region Extentions
        public ICollection<AttachmentDto> Attachments { get; set; }
        public bool IsParentAuto { get; set; }
        public string MinFromDateText { get; set; }

        public string MaxToDateText { get; set; }
        public string ParentFromDateText { get; set; }

        public string ParentToDateText { get; set; }
        public List<string> TaskItemCategories { get; set; }
        public List<Guid> AttachDelIds { get; set; }
        public List<Guid> AssignDelIds { get; set; }

        public bool IsFullControl { get; set; }
        public bool IsLinked { get; set; }
        public string ActionText { get; set; }
        public string Description { get; set; }
        public string FromDateText { get; set; }

        public string ToDateText { get; set; }
        public string Process { get; set; }
        #endregion
    }
    public class TaskItemForMSProjectDto
    {
        public TaskItemForMSProjectDto()
        {
            Childrens = new List<TaskItemForMSProjectDto>();
        }

        public Guid Id { get; set; }
        public string No { get; set; }
        public int? Order { get; set; }
        public int RowIndex { get; set; }
        public int CountChild { get; set; }
        public string Name { get; set; }
        public bool IsOwner { get; set; }
        public string Content { get; set; }
        public Guid? AssignBy { get; set; }
        public string AssignByName { get; set; }
        public string AssignByUsername { get; set; }
        //public Guid? AssignTo { get; set; }
        public Guid? AuthorId { get; set; }
        public Guid? DocId { get; set; }
        public Guid? ParentId { get; set; }
        public bool? IsGroupLabel { get; set; }
        public double? PercentFinish { get; set; }
        public List<UserExcelDto> MainWorkers { get; set; }
        public List<UserExcelDto> Supporters { get; set; }
        public List<UserExcelDto> WhoOnlyKnow { get; set; }
        public DateTime? FinishiDate { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? TaskItemPriorityId { get; set; }
        public string TaskItemCategory { get; set; }
        public int? NatureTask { get; set; }

        public double? Weight { get; set; }
        public bool? HasReport { get; set; }
        public bool? HasSecurity { get; set; }
        public bool? HasWeirdo { get; set; }
        public bool? HasMail { get; set; }
        public bool? HasAssignFollow { get; set; }
        public int? Status { get; set; }
        public string StatusName { get; set; }
        public bool? IsAnyNewTaskItemAssign { get; set; }
        public bool? IsOverDue { get; set; }
        public bool Visible { get; set; }
        public bool IsHaveChild { get { return Childrens.Any(); } }
        public List<TaskItemForMSProjectDto> Childrens { get; set; }
    }
    public class UserExcelDto
    {
        public string Color { get; set; }
        public Guid? DeptID { get; set; }
        public Guid? ID { get; set; }
        public string Text { get; set; }
        public string Username { get; set; }
        public double? PercentFinish { get; set; }
        public int? Type { get; set; }
        public bool? CanProcess { get; set; }
        public DateTime? FinishedDate { get; set; }
    }
}