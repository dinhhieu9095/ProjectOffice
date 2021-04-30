using SurePortal.Core.Kernel.Mapper;
using SurePortal.Module.Task.Entities;
using SurePortal.Module.Task.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SurePortal.Module.Task.Web
{
    public class ProjectCategoryModel : ProjectCategoryDto, IMapping<TaskItemCategoryDto>
    {
        public ProjectCategoryModel()
        {
        }

    }
}