using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaiPhatDat.Module.Task.Services
{
    public class TaskItemKpiDto
    {
        [Key] [Column(Order = 0)] public Guid TaskItemId { get; set; }

        [Key] [Column(Order = 1)] public Guid TaskItemAssignId { get; set; }

        public int? AssignToAccomplishment { get; set; }

        public int? AssignToAchievement { get; set; }

        public DateTime? AssignToModified { get; set; }

        public int? AssignByAccomplishment { get; set; }

        public int? AssignByAchievement { get; set; }

        public DateTime? AssignByModified { get; set; }

        public float? AdjustmentFactor { get; set; }

        public float? AdjustmentDensity { get; set; }

        [Column(TypeName = "numeric")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal? AssignToAverage { get; set; }

        [Column(TypeName = "numeric")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal? AssignByAverage { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public float? AdjustmentAverage { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public float? DensityAverage { get; set; }

        public virtual TaskItemAssignDto TaskItemAssign { get; set; }
    }
}