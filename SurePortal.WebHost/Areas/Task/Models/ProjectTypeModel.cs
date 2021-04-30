using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace SurePortal.Module.Task.Web
{
    public class ProjectTypeModel
    {
        public ProjectTypeModel()
        {
            Projects = new HashSet<ProjectModel>();
        }

        public int Id { get; set; }

        [StringLength(128)] public string Name { get; set; }

        [StringLength(64)] public string Code { get; set; }

        public int? OrderNumber { get; set; }

        public bool? IsActive { get; set; }

        public virtual ICollection<ProjectModel> Projects { get; set; }
    }
}