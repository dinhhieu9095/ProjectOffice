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
    public class ProjectModel : IMapping<ProjectDto>
    {
        public ProjectModel()
        {
            Attachments = new HashSet<AttachmentModel>();
            ProjectFollows = new HashSet<ProjectFollowModel>();
            ProjectHistories = new HashSet<ProjectHistoryModel>();
            TaskItems = new HashSet<TaskItemModel>();
            ProjectUsers = new HashSet<ProjectUserModel>();
            ProjectMembers = new HashSet<ProjectMemberModel>();
            ProjectCategories = new List<string>();
        }

        public Guid Id { get; set; }

        public string SerialNumber { get; set; }

        public string Summary { get; set; }

        public string ProjectContent { get; set; }

        public ProjectStatusId? ProjectStatusId { get; set; }

        public DateTime? ApprovedDate { get; set; }

        public Guid? ApprovedBy { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public DateTime? FinishedDate { get; set; }

        public int? ProjectTypeId { get; set; }

        public int? ProjectSecretId { get; set; }

        public ProjectPriorityId? ProjectPriorityId { get; set; }
        public int? ProjectKindId { get; set; }

        public DateTime? CreatedDate { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public Guid? ModifiedBy { get; set; }

        public int? ProjectScopeId { get; set; }

        public string ProjectCategory { get; set; }

        public Guid? DepartmentId { get; set; }

        public bool? HasRecentActivity { get; set; }

        public string UserViews { get; set; }

        public double? PercentFinish { get; set; }
        public string ManagerId { get; set; }
        public string AppraiseResult { get; set; }
        public bool IsLinked { get; set; }
        public bool IsAuto { get; set; }
        public ICollection<AttachmentModel> Attachments { get; set; }

        public ProjectPriorityModel ProjectPriority { get; set; }

        public ProjectScopeModel ProjectScope { get; set; }

        public ProjectSecretModel ProjectSecret { get; set; }

        public ProjectStatusModel ProjectStatus { get; set; }

        public ProjectTypeModel ProjectType { get; set; }

        public ICollection<ProjectFollowModel> ProjectFollows { get; set; }

        public ICollection<ProjectHistoryModel> ProjectHistories { get; set; }

        public ICollection<TaskItemModel> TaskItems { get; set; }

        public ICollection<ProjectUserModel> ProjectUsers { get; set; }
        #region Extentions
        public ICollection<ProjectMemberModel> ProjectMembers { get; set; }
        public List<string> ProjectCategories { get; set; }
        public string FromDateText { get; set; }
        public string ToDateText { get; set; }
        public string MinFromDateText { get; set; }
        public string MaxToDateText { get; set; }
        public List<Guid> AttachDelIds { get; set; }

        #endregion
    }
    public class ProjectMemberModel
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Department { get; set; }
        public string JobTitle { get; set; }
        public string Role { get; set; }
    }

}