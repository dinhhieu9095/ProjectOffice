using SurePortal.Core.Kernel.Firebase.Models;
using SurePortal.Core.Kernel.Models;
using SurePortal.Core.Kernel.Orgs.Application.Dto;
using SurePortal.Module.Task.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
//using LacViet.SurePortal.Core.Paging;
//using LacViet.SurePortal.DataAccess.Models;
//using LacViet.SurePortal.Services.DTOs;
//using LacViet.SurePortal.Task.Business.TaskItems;
//using LacViet.SurePortal.Task.Models;
//using LacViet.SurePortal.Task.Persistence;
using SystemTask = System.Threading.Tasks.Task;

namespace SurePortal.Module.Task.Services
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