using DaiPhatDat.Core.Kernel.Mapper;
using DaiPhatDat.Module.Task.Entities;
using DaiPhatDat.Module.Task.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DaiPhatDat.Module.Task.Web
{
    public class TaskItemModel : IMapping<TaskItemDto>
    {
        public TaskItemModel()
        {
            Children = new HashSet<TaskItemModel>();
            TaskItemAssigns = new HashSet<TaskItemAssignModel>();
            TaskItemProcessHistories = new HashSet<TaskItemProcessHistoryModel>();
            TaskItemCategories = new List<string>();
            Attachments = new List<AttachmentModel>();
            TaskItemAssignIds = new List<Guid?>();
        }

        public Guid Id { get; set; }

        public Guid? ProjectId { get; set; }

        public string TaskName { get; set; }
        public string StatusName { get; set; }
        public string AssignByFullName { get; set; }
        public string JobTitleName { get; set; }

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
        public int? WorkingHours { get; set; }


        public ProjectModel Project { get; set; }

        public TaskItemModel Parent { get; set; }

        public TaskItemPriorityModel TaskItemPriority { get; set; }

        public TaskItemStatusModel TaskItemStatus { get; set; }

        public ICollection<TaskItemModel> Children { get; set; }

        public ICollection<TaskItemAssignModel> TaskItemAssigns { get; set; }

        public ICollection<TaskItemProcessHistoryModel> TaskItemProcessHistories { get; set; }
        #region Extentions
        public ICollection<AttachmentModel> Attachments { get; set; }
        public List<Guid?> TaskItemAssignIds { get; set; }
        public bool IsParentAuto { get; set; }
        public string MinFromDateText { get; set; }

        public string MaxToDateText { get; set; }
        public string ParentFromDateText { get; set; }
        public string ActionText { get; set; }
        public string ParentToDateText { get; set; }
        public List<string> TaskItemCategories { get; set; }

        public bool IsFullControl
        { get; set; }
        public bool IsLate { get; set; }
        public string FromDateText { get; set; }

        public string ToDateText { get; set; }
        public Guid? AdminCategoryId { get; set; }
        public string Process
        {
            get
            {
                string result = string.Empty;
                if (TaskItemStatusId == Entities.TaskItemStatusId.Finished && FinishedDate.HasValue && ToDate.HasValue && FinishedDate <= ToDate)
                {
                    result = "in-due-date";
                }
                else if (TaskItemStatusId == Entities.TaskItemStatusId.Finished && FinishedDate.HasValue && ToDate.HasValue && FinishedDate > ToDate)
                {
                    result = "out-of-date";
                }
                else if (TaskItemStatusId != Entities.TaskItemStatusId.Finished && ToDate.HasValue && ToDate.Value >= DateTime.Now && ToDate.Value <= DateTime.Now.AddDays(2))
                {
                    result = "near-of-date";
                }
                else if (TaskItemStatusId != Entities.TaskItemStatusId.Finished && ToDate.HasValue && ToDate >= DateTime.Now)
                {
                    result = "in-due-date";
                }
                else
                {
                    result = "out-of-date";
                }
                return result;
            }
        }

        #endregion
    }
}