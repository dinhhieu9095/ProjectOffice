using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurePortal.Module.Task.Entities
{
    public class FileComment
    {
        public System.Guid ID { get; set; }
        public System.Guid CommentID { get; set; }
        public string Name { get; set; }
        public string Ext { get; set; }
        public byte[] Content { get; set; }

        public virtual Comment Comment { get; set; }
    }
}
