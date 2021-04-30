using SurePortal.Core.Kernel.Mapper;
using SurePortal.Module.Task.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurePortal.Module.Task.Services
{
    public class FileCommentDto : IMapping<FileComment>
    {
        public System.Guid ID { get; set; }
        public System.Guid CommentID { get; set; }
        public string Name { get; set; }
        public string Ext { get; set; }
        public byte[] Content { get; set; }

        public virtual CommentDto Comment { get; set; }
    }
}