namespace SurePortal.Module.Task.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    
    [Table("Task.CheckList")]
    public class CheckList
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid? ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
        public ICollection<CheckListItem> CheckListItems { get; set; }
        [NotMapped] public string CreatedByFullName { get; set; }
    }
}