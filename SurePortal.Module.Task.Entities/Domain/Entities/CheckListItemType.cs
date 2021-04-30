namespace SurePortal.Module.Task.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("Task.CheckListItemType")]
    public class CheckListItemType
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Code { get; set; }
        public bool? IsActive { get; set; }
    }
}