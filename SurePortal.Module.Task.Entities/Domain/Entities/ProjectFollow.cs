using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurePortal.Module.Task.Entities
{
    [Table("Task.ProjectFollow")]
    public class ProjectFollow
    {
        [Key] [Column(Order = 0)] public Guid UserId { get; set; }

        [Key] [Column(Order = 1)] public Guid ProjectId { get; set; }

        public virtual Project Project { get; set; }
    }
}