using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace DaiPhatDat.Module.Task.Services
{
    public class ProjectScopeDto
    {
        public ProjectScopeDto()
        {
            Projects = new HashSet<ProjectDto>();
        }

        public int Id { get; set; }

        [StringLength(128)] public string Name { get; set; }

        public bool? IsActive { get; set; }

        public virtual ICollection<ProjectDto> Projects { get; set; }
    }
}