using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaiPhatDat.Module.Task.Web
{
    public class ProjectFollowModel
    {
        [Key] [Column(Order = 0)] public Guid UserId { get; set; }

        [Key] [Column(Order = 1)] public Guid ProjectId { get; set; }

        public virtual ProjectModel Project { get; set; }
    }
}