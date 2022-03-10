using DaiPhatDat.Core.Kernel.Firebase.Models;
using DaiPhatDat.Core.Kernel.Models;
using DaiPhatDat.Core.Kernel.Orgs.Application.Contract;
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
    public interface IProjectService
    {
        Task<ProjectDto> GetById(Guid id);
        Task<SendMessageResponse> SaveAsync(ProjectDto dto);
        Task<SendMessageResponse> UpdateStatusProjectAsync(ProjectDto dto);
        Task<SendMessageResponse> DeleteProject(ProjectDto dto);

        List<ProjectFilterCounter> GetCountByFilters(List<string> lstParams, Guid userId, DateTime currentDate);


        Task<IReadOnlyList<TaskItem>> GetTaskAsync(Guid id, Guid userId, Guid? taskItemId = null,
            int viewType = 0,
            int viewStatus = -1);

        Task<IReadOnlyList<TaskItemDto>> GetFlatTaskAsync(Guid id, Guid userId,
          int page = 1, int pageSize = 5,
          Guid? taskItemId = null, int viewType = 0, int viewStatus = -1);

        Pagination<FetchProjectsTasksResult> GetTaskWithFilterPaging(string keyWord, List<string> paramValues, int page = 1,
                int pageSize = 15, string orderBy = "CreatedDate DESC",UserDto currentUser=default, bool isCount = false);
        Task<MoveItemDto> GetMoveDataByTask(Dictionary<string, string> paramValues);
        Pagination<FetchProjectsTasksResult> GetProjectsByFolderPaging(string keyWord, List<string> paramValues, int page = 1,
          int pageSize = 15, string orderBy = "CreatedDate DESC", UserDto currentUser = default, bool isCount = false);

        Task<UserDepartmentDto> GetUserDeptDTO(Guid? userId, Guid? deptId, IReadOnlyList<UserDepartmentDto> userDeptDtos = null);

        Task<ProjectDetailDto> RenderProject(Guid projectId, UserDto currentUser = default, bool isMobile = false);
        Task<List<ProjectHistoryDto>> GetHistories(QueryCommonDto query);
        Task<IReadOnlyList<TaskItemDto>> AllTaskItemInProject(Guid? Id);
        Task<IReadOnlyList<TaskItemDto>> AllTaskItemInProjectReport(ReportFilterDto reportFilter);

        Task<List<UserReportInProjectDto>> UserReportInProject(Guid id);

        SystemTask MarkTaskItemAssignAsReadAsync(Guid id, Guid userId, Guid taskItemId);
        Task<IReadOnlyList<TaskItemForMSProjectDto>> GetTaskForTableExcelV2(Guid id, Guid userId, Guid? taskItemId, string numberIndex, int viewType = 0, int viewStatus = -1, bool isAll = false, IReadOnlyList<Guid> listParent = null, IReadOnlyList<UserDto> users = null, bool isRoot = false);
        List<ItemActionDto> GetListItemAction(Guid projectId, Guid userId, bool isFullControl);
        Task<List<TrackingBreadCrumbViewParentDTO> > GetViewBreadCrumbWithParent(Guid parentId);
        /// <summary>
        /// check Import excel
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="projectId"></param>
        /// <param name="taskId"></param>
        /// <returns></returns>
        bool CheckImportExcelPermission(Guid userId, Guid projectId, Guid? taskId);
        bool UpdateMSProject(Guid Id);
        Task<IReadOnlyList<TaskItemForMSProjectDto>> GetTaskForTableMSPAsync(Guid id, Guid userId, Guid? taskItemId, string numberIndex, int viewType = 0, int viewStatus = -1, bool isAll = false, IReadOnlyList<Guid> listParent = null, IReadOnlyList<UserDto> users = null);
    }
}