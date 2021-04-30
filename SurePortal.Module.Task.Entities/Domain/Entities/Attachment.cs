using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurePortal.Module.Task.Entities
{
    [Table("Task.Attachment")]
    public class Attachment
    {
        public Guid Id { get; set; }

        public Guid? ProjectId { get; set; }

        public Guid? ItemId { get; set; }

        public Source? Source { get; set; }

        [StringLength(300)] public string FileName { get; set; }

        [StringLength(10)] public string FileExt { get; set; }

        public byte[] FileContent { get; set; }

        public DateTime? CreatedDate { get; set; }

        public Guid? CreatedBy { get; set; }

        public virtual Project Project { get; set; }

        [ForeignKey("ItemId")]
        public virtual TaskItemProcessHistory ProcessHistory { get; set; }
    }
}
