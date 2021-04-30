using SurePortal.Module.Task.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurePortal.Module.Task.Services
{
    public class ProjectPriorityDto
    {
        public ProjectPriorityId Id { get; set; }

        public string Name { get; set; }

        public bool? IsActive { get; set; }
    }
}