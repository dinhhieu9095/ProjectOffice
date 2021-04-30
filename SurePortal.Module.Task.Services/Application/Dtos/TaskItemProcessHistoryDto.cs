using SurePortal.Core.Kernel.Mapper;
using SurePortal.Module.Task.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SurePortal.Module.Task.Services
{
    public class TaskItemProcessHistoryDto : IMapping<TaskItemProcessHistory>
    {
        public Guid Id { get; set; }

        public Guid? TaskItemAssignId { get; set; }

        public Guid? TaskItemId { get; set; }

        public Guid? ProjectId { get; set; }

        public DateTime? CreatedDate { get; set; }
        public string CreatedDateFormat { get; set; }

        public Guid? CreatedBy { get; set; }
        public string CreatedByFullName { get; set; }
        public string CreatedByJobTitle { get; set; }
        public string ProcessResult { get; set; }

        public double? PercentFinish { get; set; }

        public TaskItemStatusId? TaskItemStatusId { get; set; }

        public string PropertyExt { get; set; }

        public ActionId? ActionId { get; set; }

        public TaskItemDto TaskItem { get; set; }

        public TaskItemAssignDto TaskItemAssign { get; set; }

        public ActionDto Action { get; set; }
        public bool? IsPrivacyReport { get; set; }
      
        public virtual List<AttachmentDto> Attachments { get; set; }

    }
}