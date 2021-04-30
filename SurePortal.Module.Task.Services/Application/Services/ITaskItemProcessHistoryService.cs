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
    public interface ITaskItemProcessHistoryService
    {
        Task<List<TaskItemProcessHistory>> GetProcessHistoryByTaskItem(Guid taskItemId);
    }
}