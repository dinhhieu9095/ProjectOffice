using SurePortal.Core.Kernel.Mapper;
using SurePortal.Module.Task.Entities;
using SurePortal.Module.Task.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SurePortal.Module.Task.Web
{
    public class TaskItemDetailModel : IMapping<TaskItemDetailDto>
    {
        public TaskItemDetailModel()
        {
            TaskItemAssigns = new List<TaskItemAssignModel>();
            TaskItemProcessHistories = new List<TaskItemProcessHistoryModel>();
        }
        public string ProjectId { get; set; }
        public Guid? CurrentUserID { get; set; }

        public string TaskItemId { get; set; }

        public string TaskItemName { get; set; }

        public DateTime? TaskItemFromDate { get; set; }

        public string TaskItemFromDateFormat { get; set; }

        public DateTime? TaskItemToDate { get; set; }

        public string TaskItemToDateFormat { get; set; }

        public Guid? AssignBy { get; set; }

        public Guid? DepartmentId { get; set; }

        public string AssignByFullName { get; set; }

        public string AssignByJobTitleName { get; set; }

        public string TaskItemConclusion { get; set; }

        public string ProjectSummary { get; set; }

        public string TaskItemParentId { get; set; }
        public string TaskItemCategory { get; set; }
        public List<string> TaskItemCategories => string.IsNullOrEmpty(TaskItemCategory)
            ? new List<string>()
                : TaskItemCategory.Split(';').ToList();

        public string TaskItemParentName { get; set; }

        public string TaskItemStatusName { get; set; }

        public bool? IsSecurity { get; set; }
        public bool? IsGroupLabel { get; set; }

        public bool IsWriteComment => TaskItemStatus != TaskItemStatusId.Finished;

        public DateTime? FinishedDate { get; set; }

        public TaskItemStatusId? TaskItemStatus { get; set; }
        public bool HasFinishTaskAssignAction { get; set; }

        public TaskItemPriorityId? TaskItemPriorityId { get; set; }

        public double? PercentFinish { get; set; }

        public string StatusColor => TaskInDueDate.Main(TaskItemToDate, FinishedDate, TaskItemStatus) ? "blue" : "red";
        //public string Process => TaskInDueDate.Main(TaskItemToDate, FinishedDate, TaskItemStatus) ? "in-due-date" : "out-of-date";
        public string Process
        {
            get
            {
                string result = string.Empty;
                if (TaskItemStatus == TaskItemStatusId.Finished && FinishedDate.HasValue && TaskItemToDate.HasValue && FinishedDate <= TaskItemToDate)
                {
                    result = "in-due-date";
                }
                else if (TaskItemStatus == TaskItemStatusId.Finished && FinishedDate.HasValue && TaskItemToDate.HasValue && FinishedDate > TaskItemToDate)
                {
                    result = "out-of-date";
                }
                else if (TaskItemStatus != TaskItemStatusId.Finished && TaskItemToDate.HasValue && TaskItemToDate.Value >= DateTime.Now && TaskItemToDate.Value <= DateTime.Now.AddDays(2))
                {
                    result = "near-of-date";
                }
                else if (TaskItemStatus != TaskItemStatusId.Finished && TaskItemToDate.HasValue && TaskItemToDate >= DateTime.Now)
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
        public IEnumerable<FetchProjectsTasksResult> Children { get; set; } 

        #region Mobile

        public int CountChildren { get; set; }
        public int CountAssigns { get; set; }
        public int CountComment { get; set; }
        public int CountHistory { get; set; }

        public string NatureTaskName { get; set; }
        public int? NatureTaskId { get; set; }
        public string TaskItemPriorityName { get; set; }

        #endregion

        public IEnumerable<TaskItemAssignModel> TaskItemAssigns { get; set; }

        public class TaskItemAssignResultModel
        {
            public string TaskItemAssignId { get; set; }

            public Guid? AssignTo { get; set; }

            public Guid? DepartmentId { get; set; }

            public string AssignToFullName { get; set; }

            public string AssignToJobTitleName { get; set; }

            public DateTime? ModifiedDate { get; set; }

            public string ModifiedDateFormat { get; set; }

            public string TaskItemAssignLastResult { get; set; }

            public string TaskItemAssignStatusName { get; set; }
            public string TaskItemAssignPercentFinish { get; set; }
            public double TaskItemAssignPercent { get; set; }
            public double? TaskItemAssignProcessPercent { get; set; }

            public string TaskItemAssignType { get; set; }

            public DateTime? TaskItemAssignToDate { get; set; }

            public DateTime? TaskItemAssignFinishDate { get; set; }

            public TaskItemStatusId? TaskItemAssignStatusId { get; set; }

            public string TaskItemAssignStatusColor =>
                TaskInDueDate.Main(TaskItemAssignToDate, TaskItemAssignFinishDate, TaskItemAssignStatusId) ? "blue" : "red";
        }

        public List<AttachmentModel> Attachments { get; set; }
        public List<TaskItemProcessHistoryModel> TaskItemProcessHistories { get; set; }
    }
}