using DaiPhatDat.Core.Kernel.Mapper;
using DaiPhatDat.Module.Task.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaiPhatDat.Module.Task.Services
{
    public class ProjectHistoryDto : IMapping<ProjectHistory>
    {
        public ProjectHistoryDto()
        {
        }

        public Guid Id { get; set; }

        public Guid? ProjectId { get; set; }

        public string Action { get; set; }

        public string Summary { get; set; }
        public List<AttachmentDto> Attachments { get; set; }
        public string CreatedByFullName { get; set; }
        public string CreatedByJobTitleName { get; set; }

        public DateTime? Created { get; set; }
        public string DateFormat { get; set; }

        public Guid? CreatedBy { get; set; }

        public ActionId? ActionId { get; set; }

        public ActionDto ActionEntity { get; set; }

        public Guid? DepartmentId { get; set; }

        public double? PercentFinish { get; set; }
    }
}