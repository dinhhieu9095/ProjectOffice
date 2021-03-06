using System.ComponentModel.DataAnnotations.Schema;

namespace DaiPhatDat.Module.Task.Entities
{
    [Table("Task.ProjectPriority")]
    public class ProjectPriority
    {
        public ProjectPriorityId Id { get; set; }

        public string Name { get; set; }

        public bool? IsActive { get; set; }
    }
}