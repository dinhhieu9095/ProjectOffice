using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace SurePortal.Module.Task.Services
{
    public class ProjectSecretDto
    {
        public ProjectSecretDto()
        {
            Projects = new HashSet<ProjectDto>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(128)] public string Name { get; set; }

        public bool? IsActive { get; set; }

        public virtual ICollection<ProjectDto> Projects { get; set; }
    }
}