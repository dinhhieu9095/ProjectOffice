using System.ComponentModel.DataAnnotations.Schema;

namespace SurePortal.Module.Task.Entities
{
    [Table("Task.TaskItemPriority")]
    public class TaskItemPriority
    {
        public TaskItemPriorityId Id { get; set; }

        public string Name { get; set; }

        public float? Density { get; set; }

        public bool? IsActive { get; set; }
    }
}