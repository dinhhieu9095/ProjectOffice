namespace SurePortal.Module.Task.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Task.ProjectFolderDetail")]
    public partial class ProjectFolderDetail
    {
        [Key]
        [Column(Order = 0)]
        public Guid FolderId { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid ProjectId { get; set; }

        public DateTime? Created { get; set; }

        public Guid? CreatedBy { get; set; }

        [ForeignKey("FolderId")]
        public virtual ProjectFolder ProjectFolder { get; set; }
    }
}
