using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurePortal.Module.Task.Entities
{
    [Table("Task.ProjectHistory")]
    public class ProjectHistory
    {
        public ProjectHistory()
        {
        }

        public Guid Id { get; set; }

        public Guid? ProjectId { get; set; }

        public string Action { get; set; }

        public string Summary { get; set; }

        public DateTime? Created { get; set; }

        public Guid? CreatedBy { get; set; }

        public ActionId? ActionId { get; set; }

        public Guid? DepartmentId { get; set; }

        public double? PercentFinish { get; set; }
    }
}