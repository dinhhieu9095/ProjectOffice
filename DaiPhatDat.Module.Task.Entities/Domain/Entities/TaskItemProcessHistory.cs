using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DaiPhatDat.Module.Task.Entities
{
    [Table("Task.TaskItemProcessHistory")]
    public class TaskItemProcessHistory
    {
        public Guid Id { get; set; }

        public Guid? TaskItemAssignId { get; set; }

        public Guid? TaskItemId { get; set; }

        public Guid? ProjectId { get; set; }

        public DateTime? CreatedDate { get; set; }

        public Guid? CreatedBy { get; set; }

        public string ProcessResult { get; set; }

        public double? PercentFinish { get; set; }

        public TaskItemStatusId? TaskItemStatusId { get; set; }

        public string PropertyExt { get; set; }

        public ActionId? ActionId { get; set; }

        public TaskItem TaskItem { get; set; }

        public TaskItemAssign TaskItemAssign { get; set; }

        //public Action Action { get; set; }
        public bool? IsPrivacyReport { get; set; }
      
        public virtual ICollection<Attachment> Attachments { get; set; }

    }
}