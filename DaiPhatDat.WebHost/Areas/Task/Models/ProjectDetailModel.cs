using DaiPhatDat.Core.Kernel.Mapper;
using DaiPhatDat.Module.Task.Entities;
using DaiPhatDat.Module.Task.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;

namespace DaiPhatDat.Module.Task.Web
{
    public class ProjectDetailModel : IMapping<ProjectDetailDto>
    {
        public ProjectDetailModel()
        {

        }

        public Guid Id { get; set; }

        public string Summary { get; set; }

        public List<string> ProjectCategories => string.IsNullOrEmpty(ProjectCategory)
            ? new List<string>()
                : ProjectCategory.Split(';').ToList();

        public string ProjectCategory { get; set; }

        public string ProjectStatusName { get; set; }

        public DateTime? FromDate { get; set; }
        public DateTime? FinishedDate { get; set; }

        public DateTime? ToDate { get; set; }

        public DateTime? Finished { get; set; }

        public ProjectStatusId? StatusId { get; set; }
        public string Process
        {
            get
            {
                string result = string.Empty;
                if (StatusId == ProjectStatusId.Finished && FinishedDate.HasValue && ToDate.HasValue && FinishedDate <= ToDate)
                {
                    result = "in-due-date";
                }
                else if (StatusId == ProjectStatusId.Finished && FinishedDate.HasValue && ToDate.HasValue && FinishedDate > ToDate)
                {
                    result = "out-of-date";
                }
                else if(StatusId != ProjectStatusId.Finished && ToDate.HasValue && ToDate.Value >= DateTime.Now && ToDate.Value <= DateTime.Now.AddDays(2))
                {
                    result = "near-of-date";
                }    
                else if (StatusId != ProjectStatusId.Finished && ToDate.HasValue && ToDate >= DateTime.Now)
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

        public string DateTimeString { get; set; }

        public Guid? ApprovedBy { get; set; }

        public Guid? CreatedBy { get; set; }
        public string FullName { get; set; }

        public string JobTitle { get; set; }

        public Guid? DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        public string ProjectContent { get; set; }
        public string ProjectPriorityName { get; set; }
        public string ProjectKindName { get; set; }

        public ProjectPriorityId? ProjectPriorityId { get; set; }
        public List<UserModel> Users { get; set; }
        public string UserViews { get; set; }
        public double? PercentFinish { get; set; }

        public string StatusColor => TaskInDueDate.Main(ToDate, Finished, StatusId) ? "blue" : "red";

        public ICollection<ProjectHistoryModel> ProjectHistories { get; set; }
        public int CountHistory { get; set; }
        public UserAssign ProjectUserAssign { get; set; }

        public IReadOnlyList<TaskItemRoot> TaskItemRoots { get; set; }
        public IReadOnlyList<TaskDetailModel> TaskItems { get; set; }
        public int CountTask { get; set; }

        public int? ProjectKindId { get; set; }

        public class TaskItemRoot
        {
            public Guid Id { get; set; }

            public bool? IsGroupLabel { get; set; }

            public string TaskName { get; set; }
            public string Content { get; set; }

            public DateTime? FromDate { get; set; }


            public DateTime? ToDate { get; set; }
            public DateTime? FinishedDate { get; set; }
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

            public string DateFormat { get; set; }

            public Guid? AssignBy { get; set; }

            public Guid? UserId { get; set; }

            public Guid? DepartmentId { get; set; }

            public TaskItemStatusId? TaskItemStatusId { get; set; }

            public string UserFullName { get; set; }

            public string JobTitleName { get; set; }

            public string StatusName { get; set; }

            public double? PercentFinish { get; set; }

            public string Color => TaskItemStatusId != Entities.TaskItemStatusId.Finished
                && ToDate != null
                && ToDate.GetValueOrDefault() < DateTime.Now
                ? "red" : "blue";

            public int CountComment { get; set; }
            public int CountChildren { get; set; }
            public int CountAttachment { get; set; }
            public List<EditTaskItemAssignModel> TaskItemAssigns { get; set; }

            public ICollection<AttachmentModel> Attachments { get; set; }

        }

        public class UserAssign
        {
            public Guid? Id { get; set; }

            public string FullName { get; set; }

            public string JobTitleName { get; set; }

        }

        public List<UserReportInProjectModel> ProjectMembers { get; set; }
        public List<AttachmentModel> Attachments { get; set; }
        public List<AttachmentModel> AttachmentChildren { get; set; }
        public ReportProjectModel ReportProject { get; set; }
    }
     
    public class EditTaskItemAssignModel
    {
        public Guid ProjectId { get; set; }
        public Guid TaskItemId { get; set; }
        public Guid TaskItemAssignId { get; set; }
        public string Comment { get; set; }
        public IEnumerable<string> Attachment { get; set; }
        // id và loại user trước khih thay đổi
        public string CurrentFullNameAssign { get; set; }
        public Guid CurrentAssignTo { get; set; }
        public TaskType CurrentTaskType { get; set; }
        // id,deptId và loại user sau khi thay đổi
        public string NewFullNameAssign { get; set; }
        public Guid NewAssignTo { get; set; }
        public Guid NewAssignDepartmentId { get; set; }
        public TaskType NewTaskType { get; set; }
    }

    public class UserReportInProjectModel : IMapping<UserReportInProjectDto>
    {
        public UserReportInProjectModel()
        {
            New = 0;
            Process = 0;
            Read = 0;
            Report = 0;
            Finsh = 0;
            InDueDate = 0;
            OutOfDate = 0;
        }

        public Guid? UserId { get; set; }

        public Guid? DepartmentId { get; set; }

        public PositionInProject Type { get; set; }

        public string UserName { get; set; }

        public string JobTitleName { get; set; }

        public int? TotalTask => InDueDate + OutOfDate;

        public int? Read { get; set; }

        public int? New { get; set; }

        public int? Process { get; set; }

        public int? Report { get; set; }

        public int? Finsh { get; set; }

        public int? InDueDate { get; set; }

        public int? OutOfDate { get; set; }

        public UserReportInProjectModel AddUser(Guid? userId, Guid? departmentId, string userName, string joinTitle, PositionInProject type)
        {
            return new UserReportInProjectModel()
            {
                UserId = userId,
                DepartmentId = departmentId,
                JobTitleName = joinTitle,
                UserName = userName,
                Type = type
            };
        }
    }

    public enum PositionInProject
    {
        Manager,
        Followers,
        AssignTo,
    }
}