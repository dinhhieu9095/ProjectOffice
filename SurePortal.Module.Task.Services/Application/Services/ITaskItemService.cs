using DaiPhatDat.Core.Kernel.Firebase.Models;
using DaiPhatDat.Core.Kernel.Orgs.Application.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DaiPhatDat.Module.Task.Services
{
    public interface ITaskItemService
    {
        Task<TaskItemDto> GetById(Guid id);
        Task<TaskItemDto> GetNewTask(TaskItemDto dto);
        IReadOnlyList<Guid> GetTaskOfUserAssign(Guid id, Guid userId);
        Task<SendMessageResponse> SaveAsync(TaskItemDto dto);
        Task<SendMessageResponse> DeleteTaskItem(TaskItemDto dto);
        Task<TaskItemDetailDto> RenderProjectTask(Guid Id, UserDto currentUser = default);

        Task<List<TaskItemProcessHistoryDto>> GetTaskHistories(QueryCommonDto query);
        Task<List<TaskItemAssignDto>> GetTaskItemAssignChildrens(Guid taskItemId);
        Task<List<ItemActionDto>> GetListItemActionTask(Guid projectId, Guid taskItemId, Guid userId, bool isFullControl);
        Task<List<TaskItemDto>> AllTaskItemChildren(Guid Id);
        Task<List<UserReportInProjectDto>> UserReportInTask(Guid id);
        Task<SendMessageResponse> UpdateStatusTaskItem(TaskItemDto dto);

        Task<bool> ReturnTaskItemAssign(Guid taskItemAssignId, Guid userId);
        Task<TaskItemDto> GetDateRangeForTask(Guid projectId, Guid? taskId, IReadOnlyList<TaskItemDto> taskItems = null);
        Task<bool> ImportExcelV2Action(List<TaskItemForMSProjectDto> tasks, Guid? projectId, Guid userId, Guid? taskId);
        Task<bool> ImportMSProject(List<TaskItemForMSProjectDto> tasks, Guid? projectId, bool isAll, Guid userId);
    }
}