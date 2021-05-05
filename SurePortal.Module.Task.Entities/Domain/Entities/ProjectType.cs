using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace DaiPhatDat.Module.Task.Entities
{
    [Table("Task.ProjectType")]
    public class ProjectType
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProjectType()
        {
            Projects = new HashSet<Project>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(128)] public string Name { get; set; }

        [StringLength(64)] public string Code { get; set; }

        public int? OrderNumber { get; set; }

        public bool? IsActive { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Project> Projects { get; set; }
    }
}