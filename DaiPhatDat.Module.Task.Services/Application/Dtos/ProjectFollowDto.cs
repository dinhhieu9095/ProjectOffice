using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaiPhatDat.Module.Task.Services
{
    public class ProjectFollowDto
    {
        [Key] [Column(Order = 0)] public Guid UserId { get; set; }

        [Key] [Column(Order = 1)] public Guid ProjectId { get; set; }

        public virtual ProjectDto Project { get; set; }
    }
}