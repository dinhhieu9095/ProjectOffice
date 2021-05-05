namespace DaiPhatDat.Module.Task.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Task.ProjectFolder")]
    public partial class ProjectFolder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProjectFolder()
        {
            ProjectFolderDetail = new HashSet<ProjectFolderDetail>();
        }

        public Guid Id { get; set; }

        [StringLength(350)]
        public string Name { get; set; }

        public Guid? ParentId { get; set; }

        public Guid? UserId { get; set; }

        public bool? IsPersonal { get; set; }

        public DateTime? Created { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTime? Modified { get; set; }

        public Guid? ModifiedBy { get; set; }

        public bool? IsActive { get; set; }

        public string Permission { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProjectFolderDetail> ProjectFolderDetail { get; set; }
    }
}
