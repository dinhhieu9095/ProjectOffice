using DaiPhatDat.Core.Kernel.Mapper;
using DaiPhatDat.Module.Task.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DaiPhatDat.Module.Task.Services
{
    public class TaskItemDetailDto
    {
        public TaskItemDetailDto()
        {
            TaskItemAssigns = new List<TaskItemAssignDto>();
            TaskItemProcessHistories = new List<TaskItemProcessHistoryDto>();
        }
        public string ProjectId { get; set; }

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

        public bool IsWriteComment => TaskItemStatus != TaskItemStatusId.Finished;

        public DateTime? FinishedDate { get; set; }

        public TaskItemStatusId? TaskItemStatus { get; set; }
        public bool? IsGroupLabel { get; set; }
        public bool HasFinishTaskAssignAction { get; set; }

        public TaskItemPriorityId? TaskItemPriorityId { get; set; }

        public double? PercentFinish { get; set; }

        public string StatusColor => TaskInDueDate.Main(TaskItemToDate, FinishedDate, TaskItemStatus) ? "blue" : "red";
        public string Process => TaskInDueDate.Main(TaskItemToDate, FinishedDate, TaskItemStatus) ? "in-due-date" : "out-of-date";

        public List<FetchProjectsTasksResult> Children { get; set; }

        #region Mobile

        public int CountChildren { get; set; }
        public int CountAssigns { get; set; }
        public int CountComment { get; set; }
        public int CountHistory { get; set; }

        public string NatureTaskName { get; set; }
        public int? NatureTaskId { get; set; }
        public string TaskItemPriorityName { get; set; }

        #endregion

        public List<TaskItemAssignDto> TaskItemAssigns { get; set; }

        public List<AttachmentDto> Attachments { get; set; }
        public List<AttachmentDto> AttachmentChildren { get; set; }
        public List<TaskItemProcessHistoryDto> TaskItemProcessHistories { get; set; }

    }
}