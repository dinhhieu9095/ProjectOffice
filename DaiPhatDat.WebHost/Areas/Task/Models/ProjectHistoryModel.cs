using DaiPhatDat.Module.Task.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaiPhatDat.Module.Task.Web
{
    public class ProjectHistoryModel
    {
        public ProjectHistoryModel()
        {
        }

        public Guid Id { get; set; }

        public Guid? ProjectId { get; set; }

        public string Action { get; set; }

        public string Summary { get; set; }
        public string CreatedByFullName { get; set; }
        public string CreatedByJobTitleName { get; set; }

        public DateTime? Created { get; set; }
        public string DateFormat { get; set; }

        public Guid? CreatedBy { get; set; }

        public ActionId? ActionId { get; set; }

        public ActionModel ActionEntity { get; set; }

        public Guid? DepartmentId { get; set; }

        public double? PercentFinish { get; set; }
        public List<AttachmentModel> Attachments { get; set; }
    }
}