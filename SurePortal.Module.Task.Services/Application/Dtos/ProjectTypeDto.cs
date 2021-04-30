using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace SurePortal.Module.Task.Services
{
    public class ProjectTypeDto
    {
        public ProjectTypeDto()
        {
            Projects = new HashSet<ProjectDto>();
        }

        public int Id { get; set; }

        [StringLength(128)] public string Name { get; set; }

        [StringLength(64)] public string Code { get; set; }

        public int? OrderNumber { get; set; }

        public bool? IsActive { get; set; }

        public virtual ICollection<ProjectDto> Projects { get; set; }
    }
}