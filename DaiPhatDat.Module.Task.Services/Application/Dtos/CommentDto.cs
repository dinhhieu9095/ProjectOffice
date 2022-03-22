using DaiPhatDat.Core.Kernel.Mapper;
using DaiPhatDat.Module.Task.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaiPhatDat.Module.Task.Services
{
    public class CommentDto : IMapping<Comment>
    {
        public CommentDto()
        {
            this.FileComments = new List<FileCommentDto>();
        }
        public System.Guid ID { get; set; }
        public Nullable<System.Guid> UserID { get; set; }
        public string FullName { get; set; }
        public Nullable<System.Guid> ObjectID { get; set; }
        public string ModuleCode { get; set; }
        public string Content { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsChange { get; set; }
        public string HistoryContent { get; set; }

        public string Class { get; set; }
        public string Class1 { get; set; }
        public int Total { get; set; }
        public virtual List<FileCommentDto> FileComments { get; set; }
    }
}