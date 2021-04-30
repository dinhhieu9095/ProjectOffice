using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SurePortal.Module.Task.Web
{
    public class ProjectUserModel
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public Guid UserId { get; set; }
        public ProjectUserActionId Action { get; set; }
        public virtual ProjectModel Project { get; set; }
    }
    public enum ProjectUserActionId
    {
        View,
    }
}