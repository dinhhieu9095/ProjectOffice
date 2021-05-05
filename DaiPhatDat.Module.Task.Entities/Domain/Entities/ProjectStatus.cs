using System.ComponentModel.DataAnnotations.Schema;

namespace DaiPhatDat.Module.Task.Entities
{
    [Table("Task.ProjectStatus")]
    public class ProjectStatus
    {
        public ProjectStatusId Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public bool? IsActive { get; set; }

        [NotMapped] public string Color { get; set; }
    }
}