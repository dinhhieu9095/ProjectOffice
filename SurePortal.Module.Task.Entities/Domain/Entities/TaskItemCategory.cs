using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace SurePortal.Module.Task.Entities
{
    [Table("Task.TaskItemCategory")]
    public class TaskItemCategory
    {
        public TaskItemCategory()
        {
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [StringLength(128)] public string Name { get; set; }

        public Guid? ParentId { get; set; }

        public bool? IsActive { get; set; }

        public Guid? UserId { get; set; }

        public DateTime? DateUseLast { get; set; }
    }
}