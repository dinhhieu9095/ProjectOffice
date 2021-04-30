using SurePortal.Core.Kernel.Firebase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurePortal.Module.Task.Services
{
    public interface ITaskItemAssignService
    {
        Task<SendMessageResponse> UpdateProcessTaskAssign(TaskItemAssignDto dto);
        Task<TaskItemAssignDto> GetById(Guid id);
        Task<TaskItemAssignDto> GetByAssignBy(Guid taskId, Guid userId);
        Task<TaskItemAssignDto> GetByAssignTo(Guid taskId, Guid userId);
    }
}
