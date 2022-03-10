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
    public interface IAdminService
    {
        Task<SendMessageResponse> SaveCategoryProjectAsync(ProjectDto dto);
    }
}