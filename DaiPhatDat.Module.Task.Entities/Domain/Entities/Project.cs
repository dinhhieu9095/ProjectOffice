using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;

namespace DaiPhatDat.Module.Task.Entities
{
    [Table("Task.Project")]
    public class Project
    {
        public Project()
        {
            Attachments = new HashSet<Attachment>();
            ProjectFollows = new HashSet<ProjectFollow>();
            ProjectHistories = new HashSet<ProjectHistory>();
            TaskItems = new HashSet<TaskItem>();
            ProjectUsers = new HashSet<ProjectUser>();
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
        public Guid? AdminCategoryId { get; set; }
        public ICollection<Attachment> Attachments { get; set; }

        public ProjectPriority ProjectPriority { get; set; }

        public ProjectScope ProjectScope { get; set; }

        public ProjectSecret ProjectSecret { get; set; }

        public ProjectStatus ProjectStatus { get; set; }

        public ProjectType ProjectType { get; set; }

        public ICollection<ProjectFollow> ProjectFollows { get; set; }

        public ICollection<ProjectHistory> ProjectHistories { get; set; }

        public ICollection<TaskItem> TaskItems { get; set; }

        public ICollection<ProjectUser> ProjectUsers { get; set; }
    }

    
}