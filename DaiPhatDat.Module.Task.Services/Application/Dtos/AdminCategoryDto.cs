using DaiPhatDat.Core.Kernel.Mapper;
using DaiPhatDat.Module.Task.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;

namespace DaiPhatDat.Module.Task.Services
{
    public class AdminCategoryDto : IMapping<AdminCategory>
    {
        public AdminCategoryDto()
        {
            TaskItems = new HashSet<TaskItemDto>();
        }

        public Guid Id { get; set; }

        public string Summary { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CreatedDate { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public Guid? ModifiedBy { get; set; }

        public ICollection<TaskItemDto> TaskItems { get; set; }
    }
}