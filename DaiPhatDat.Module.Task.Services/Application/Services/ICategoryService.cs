using DaiPhatDat.Core.Kernel.Firebase.Models;
using DaiPhatDat.Core.Kernel.Models;
using DaiPhatDat.Core.Kernel.Orgs.Application.Dto;
using DaiPhatDat.Module.Task.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SystemTask = System.Threading.Tasks.Task;

namespace DaiPhatDat.Module.Task.Services
{
    public interface ICategoryService
    {
        #region ProjectCategory
        Task<List<ProjectCategoryDto>> GetProjectCategories(Guid userId);
        Task<List<string>> GetProjectCategoriesByProjectId(Guid projectId, Guid? taskId = null);

        Task<List<ProjectCategoryDto>> GetAllOfProjectCategories();
        #endregion
        #region ProjectPriorities
        Task<List<ProjectPriorityDto>> GetAllOfProjectPriorities();
        #endregion
        #region ProjectStatus
        Task<List<ProjectStatusDto>> GetAllProjectStatuses();
        #endregion
        #region ProjectType
        Task<List<ProjectTypeDto>> GetAllOfProjectTypes();
        #endregion

        #region TaskItemCategory
        Task<List<TaskItemCategoryDto>> GetTaskItemCategories(Guid userId);

        Task<List<TaskItemCategoryDto>> GetAllTaskItemCategories();
        #endregion

        #region TaskItemStatus
        Task<List<TaskItemStatusDto>> GetAllTaskItemStatuses();
        #endregion

        #region TaskItemPriority
        Task<List<TaskItemPriorityDto>> GetAllTaskItemPriorities();
        #endregion

        #region NatureTask
        Task<List<NatureTaskDto>> GetAllNatureTasks();
        #endregion
        #region Db
        Task<bool> PostTrackingUpdateDB();
        #endregion
    }
}