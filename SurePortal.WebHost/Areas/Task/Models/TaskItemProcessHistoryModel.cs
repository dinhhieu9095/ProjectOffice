using SurePortal.Module.Task.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using SurePortal.Core.Kernel.Mapper;
using SurePortal.Module.Task.Services;

namespace SurePortal.Module.Task.Web
{
    public class TaskItemProcessHistoryModel : IMapping<TaskItemProcessHistoryDto>
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

        //public TaskItemModel TaskItem { get; set; }

        //public TaskItemAssignModel TaskItemAssign { get; set; }

        public ActionModel Action { get; set; }
        public bool? IsPrivacyReport { get; set; }
      
        public virtual List<AttachmentModel> Attachments { get; set; }

    }
}