using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DaiPhatDat.Module.Task.Entities
{
    [Table("Task.ProjectUser")]
    public class ProjectUser
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public Guid UserId { get; set; }
        public ProjectUserActionId Action { get; set; }
        public virtual Project Project { get; set; }

        public void Create(Guid projectId,Guid userId)
        {
            Id = Guid.NewGuid();
            ProjectId = projectId;
            UserId = userId;
            Action = ProjectUserActionId.View;
        }
    }
    public enum ProjectUserActionId
    {
        View,
    }
}