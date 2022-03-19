using DaiPhatDat.Core.Kernel.Mapper;
using DaiPhatDat.Module.Task.Entities;
using DaiPhatDat.Module.Task.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;

namespace DaiPhatDat.Module.Task.Web
{
    public class AdminCategoryModel : IMapping<AdminCategoryDto>
    {
        public AdminCategoryModel()
        {
            TaskItems = new HashSet<TaskItemModel>();
        }

        public Guid Id { get; set; }

        public string Summary { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CreatedDate { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public Guid? ModifiedBy { get; set; }

        public ICollection<TaskItemModel> TaskItems { get; set; }
    }
}