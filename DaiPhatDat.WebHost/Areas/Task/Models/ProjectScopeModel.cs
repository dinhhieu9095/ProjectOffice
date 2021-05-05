using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace DaiPhatDat.Module.Task.Web
{
    public class ProjectScopeModel
    {
        public ProjectScopeModel()
        {
            Projects = new HashSet<ProjectModel>();
        }

        public int Id { get; set; }

        [StringLength(128)] public string Name { get; set; }

        public bool? IsActive { get; set; }

        public virtual ICollection<ProjectModel> Projects { get; set; }
    }
}