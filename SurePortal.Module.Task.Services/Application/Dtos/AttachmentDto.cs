using SurePortal.Core.Kernel.Mapper;
using SurePortal.Module.Task.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurePortal.Module.Task.Services
{
    public class AttachmentDto : IMapping<Attachment>
    {
        public Guid Id { get; set; }

        public Guid? ProjectId { get; set; }

        public Guid? ItemId { get; set; }

        public Source? Source { get; set; }

        [StringLength(300)] public string FileName { get; set; }

        [StringLength(10)] public string FileExt { get; set; }

        public DateTime? CreatedDate { get; set; }

        public Guid? CreatedBy { get; set; }
        public int FileSize { get; set; }

        public string CreateByFullName { get; set; }

        public string DateFormat { get; set; }
        public byte[] FileContent { get; set; }
        public string Icon()
        {
            var rs = "fa fa-file-pdf-o";
            switch (FileExt.Trim())
            {
                case ".pdf":
                    {
                        rs = "fa fa-file-pdf-o font-red";
                        break;
                    }
                case ".xls":
                case ".xlsx":
                    {
                        rs = "fa fa-file-excel-o font-green-meadow";
                        break;
                    }
                case ".doc":
                case ".docx":
                    {
                        rs = "fa fa-file-word-o font-blue-madison";
                        break;
                    }
                case ".ppt":
                case ".pptx":
                    {
                        rs = "fa fa-file-powerpoint-o font-red";
                        break;
                    }
                case ".txt":
                    {
                        rs = "fa fa-file-text-o font-red";
                        break;
                    }
                case ".png":
                case ".jpg":
                case ".jpeg":
                case ".gif":
                    {
                        rs = "fa fa-file-image-o font-purple";
                        break;
                    }
            }
            return rs;
        }
    }
}
