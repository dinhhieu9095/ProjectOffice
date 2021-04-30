using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurePortal.Module.Task.Entities
{
    public class Comment
    {
        public Comment()
        {
            this.FileComments = new HashSet<FileComment>();
        }

        public System.Guid ID { get; set; }
        public Nullable<System.Guid> UserID { get; set; }
        public Nullable<System.Guid> ObjectID { get; set; }
        public string ModuleCode { get; set; }
        public string Content { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsChange { get; set; }
        public string HistoryContent { get; set; }

        public virtual ICollection<FileComment> FileComments { get; set; }
    }
}
