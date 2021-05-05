using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaiPhatDat.Module.Task.Entities
{
    [Table("Task.ProjectFilterParam")]
    public class ProjectFilterParam
    {
        public Guid Id { get; set; }

        [StringLength(250)] public string Name { get; set; }

        [StringLength(50)] public string Code { get; set; }

        [StringLength(2500)] public string ParamValue { get; set; }

        public bool? IsCount { get; set; }

        public bool? IsPrivate { get; set; }
        public bool? IsLable { get; set; }

        public bool? IsActive { get; set; }

        public int? NoOrder { get; set; }
        public string Roles { get; set; }
        public string Users { get; set; }

        public DateTime? CreatedDate { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public Guid? ModifiedBy { get; set; }

        public Guid? ParentID { get; set; }

        [StringLength(80)]
        public string TypeShow { get; set; }

        [StringLength(80)]
        public string Icon { get; set; }

    }
}