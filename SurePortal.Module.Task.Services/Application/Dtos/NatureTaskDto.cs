using DaiPhatDat.Core.Kernel.Mapper;
using DaiPhatDat.Module.Task.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DaiPhatDat.Module.Task.Services
{
    public class TaskItemCategoryDto : TaskItemCategory, IMapping<TaskItemCategory>
    {
        public TaskItemCategoryDto()
        {
        }
         
    }
}