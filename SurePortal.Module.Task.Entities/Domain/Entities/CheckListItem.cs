namespace SurePortal.Module.Task.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Task.CheckListItem")]
    public class CheckListItem
    {
        public Guid Id { get; set; }
        public Guid CheckListId { get; set; }
        public string Name { get; set; }
        public int? CheckListItemTypeId { get; set; }
        public string DefaultValue { get; set; }
        public CheckListItemType CheckListItemType { get; set; }
    }
}