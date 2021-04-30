using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurePortal.Module.Task.Entities
{
    [Table("Task.TaskItemAppraiseHistory")]
    public class TaskItemAppraiseHistory
    {
        public Guid Id { get; set; }

        public Guid? TaskItemAssignId { get; set; }

        public Guid? TaskItemId { get; set; }

        public Guid? ProjectId { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string AppraiseResult { get; set; }

        public TaskItemStatusId? TaskItemStatusId { get; set; }

        public DateTime? ExtendDate { get; set; }

        public string PropertyExt { get; set; }

        public ActionId? ActionId { get; set; }

        public virtual TaskItemAssign TaskItemAssign { get; set; }
    }
}