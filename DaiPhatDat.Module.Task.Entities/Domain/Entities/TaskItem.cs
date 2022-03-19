using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DaiPhatDat.Module.Task.Entities
{
    [Table("Task.TaskItem")]
    public partial class TaskItem
    {
        public TaskItem()
        {
            Children = new HashSet<TaskItem>();
            TaskItemAssigns = new HashSet<TaskItemAssign>();
            TaskItemProcessHistories = new HashSet<TaskItemProcessHistory>();
        }

        public Guid Id { get; set; }

        public Guid? ProjectId { get; set; }

        public string TaskName { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public DateTime? FinishedDate { get; set; }

        public TaskItemStatusId? TaskItemStatusId { get; set; }

        public DateTime? CreatedDate { get; set; }

        public Guid? CreatedBy { get; set; }

        public Guid? AssignBy { get; set; }

        public Guid? ParentId { get; set; }

        public double? PercentFinish { get; set; }

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
        public int? NatureTaskId { get; set; }
        public int? Order { get; set; }
        public bool IsAuto { get; set; }
        public bool IsAdminCategory { get; set; }
        public int TaskGroupType { get; set; }
        public Guid? AdminCategoryId { get; set; }

        public Project Project { get; set; }

        public TaskItem Parent { get; set; }

        public TaskItemPriority TaskItemPriority { get; set; }

        public TaskItemStatus TaskItemStatus { get; set; }

        public ICollection<TaskItem> Children { get; set; }

        public ICollection<TaskItemAssign> TaskItemAssigns { get; set; }

        public ICollection<TaskItemProcessHistory> TaskItemProcessHistories { get; set; }

        [NotMapped] public bool HasTaskItemAssign => TaskItemAssigns.Any();

        [NotMapped] public bool HasChildrenAssignRecursively { get; set; }

        [NotMapped] public string DepartmentName { get; set; }

        [NotMapped] public string ShowElement { get; set; }

        [NotMapped] public string AssignByFullName { get; set; }

        [NotMapped] public string JobTitleName { get; set; }

    }
}