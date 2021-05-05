using System.ComponentModel.DataAnnotations.Schema;

namespace DaiPhatDat.Module.Task.Entities
{
    [Table("Task.NatureTask")]
    public class NatureTask
    {
        public EnumNatureTask Id { get; set; }

        public string Name { get; set; }
          
        public bool? IsActive { get; set; }
    }
}