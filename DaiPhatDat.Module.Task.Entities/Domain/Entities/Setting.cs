using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaiPhatDat.Module.Task.Entities
{
    [Table("dbo.Settings")]
    public class Setting
    {
        public string Name { get; set; }
        [Key]
        public string Code { get; set; }
        public string Value { get; set; }
    }
}