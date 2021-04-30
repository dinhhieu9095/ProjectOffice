using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace SurePortal.Module.Task.Web
{
    public class ProjectSecretModel
    {
        public ProjectSecretModel()
        {
            Projects = new HashSet<ProjectModel>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(128)] public string Name { get; set; }

        public bool? IsActive { get; set; }

        public virtual ICollection<ProjectModel> Projects { get; set; }
    }
}