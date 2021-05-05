using DaiPhatDat.Core.Kernel.Mapper;
using DaiPhatDat.Module.Task.Entities;
using DaiPhatDat.Module.Task.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DaiPhatDat.Module.Task.Web
{
    public class TaskItemCategoryModel : TaskItemCategoryDto, IMapping<TaskItemCategoryDto>
    {
        public TaskItemCategoryModel()
        {
        }
    }
}