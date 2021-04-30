using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurePortal.Module.Task.Entities
{
    [Table("Task.Report")]
    public class Report
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string LinkDesktop { get; set; }
        public string Icon { get; set; }
        public string CssClass { get; set; }
        public string Link { get; set; }
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }
        public bool IsActive { get; set; }
        public string Type { get; set; }
        public string Permission { get; set; }
        public bool IsUser { get; set; }
    }
}