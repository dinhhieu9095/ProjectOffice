using System.ComponentModel.DataAnnotations.Schema;

namespace SurePortal.Module.Task.Entities
{
    [Table("Task.TaskItemStatus")]
    public class TaskItemStatus
    {
        public TaskItemStatusId Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public bool? IsActive { get; set; }

        [NotMapped] public string Color { get; set; }
    }
}