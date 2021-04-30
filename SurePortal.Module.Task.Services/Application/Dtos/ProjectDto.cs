using SurePortal.Core.Kernel.Mapper;
using SurePortal.Module.Task.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;

namespace SurePortal.Module.Task.Services
{
    public class ProjectDto : IMapping<Project>
    {
        public ProjectDto()
        {
            Attachments = new HashSet<AttachmentDto>();
            ProjectFollows = new HashSet<ProjectFollowDto>();
            ProjectHistories = new HashSet<ProjectHistoryDto>();
            TaskItems = new HashSet<TaskItemDto>();
            ProjectUsers = new HashSet<ProjectUserDto>();
            ProjectMembers = new HashSet<ProjectMemberDto>();
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
        /// <summary>
        /// Tự động liên kết
        /// </summary>
        public bool IsLinked { get; set; }
        /// <summary>
        /// Tự động giãn thời gian
        /// </summary>
        public bool IsAuto { get; set; }

        public ICollection<AttachmentDto> Attachments { get; set; }

        public ProjectPriorityDto ProjectPriority { get; set; }

        public ProjectScopeDto ProjectScope { get; set; }

        public ProjectSecretDto ProjectSecret { get; set; }

        public ProjectStatusDto ProjectStatus { get; set; }

        public ProjectTypeDto ProjectType { get; set; }

        public ICollection<ProjectFollowDto> ProjectFollows { get; set; }

        public ICollection<ProjectHistoryDto> ProjectHistories { get; set; }

        public ICollection<TaskItemDto> TaskItems { get; set; }

        public ICollection<ProjectUserDto> ProjectUsers { get; set; }
        #region Extentions
        public bool IsFullControl { get; set; }
        #endregion
        #region Extentions
        public List<string> ProjectCategories { get; set; }
        public ICollection<ProjectMemberDto> ProjectMembers { get; set; }
        public string FromDateText { get; set; }
        public string ToDateText { get; set; }
        public string MinFromDateText { get; set; }
        public string MaxToDateText { get; set; }
        public List<Guid> AttachDelIds { get; set; }
        #endregion
    }

    public class ProjectMemberDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Department { get; set; }
        public string JobTitle { get; set; }
        public string Role { get; set; }
    }
}