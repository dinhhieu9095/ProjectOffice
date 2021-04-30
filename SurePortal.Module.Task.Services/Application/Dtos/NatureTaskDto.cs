using SurePortal.Core.Kernel.Mapper;
using SurePortal.Module.Task.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SurePortal.Module.Task.Services
{
    public class TaskItemCategoryDto : TaskItemCategory, IMapping<TaskItemCategory>
    {
        public TaskItemCategoryDto()
        {
        }
         
    }
}