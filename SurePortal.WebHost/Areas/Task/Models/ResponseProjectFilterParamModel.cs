using SurePortal.Core.Kernel.Mapper;
using SurePortal.Module.Task.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurePortal.Module.Task.Web
{
    public class ResponseProjectFilterParamModel
    {
        public ProjectFilterParamModel ProjectFilterParam { get; set; }
        public bool IsAdmin { get; set; }
        public List<TaskItemStatusModel> TaskItemStatuses { get; set; }
        public List<TaskItemPriorityModel> TaskItemPriorites { get; set; }
        public List<TaskItemCategoryModel> TaskItemCategories { get; set; }
        public List<ProjectCategoryModel> ProjectCategories { get; set; }
        public List<NatureTaskModel> NatureTasks { get; set; }
        public List<ProjectFilterParamModel> ProjectFilterParams { get; set; }
        public List<UserModel> UserInfos { get; set; }
    }
}