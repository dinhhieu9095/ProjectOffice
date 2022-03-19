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
    public interface IAdminCategoryService
    {
        Task<SendMessageResponse> SaveAsync(AdminCategoryDto dto);
        Task<SendMessageResponse> Delete(AdminCategoryDto dto);
        AdminCategoryDto GetById(Guid id);
        Pagination<FetchProjectsTasksResult> GetTaskWithFilterPaging(string keyWord, List<string> paramValues, int page = 1, int pageSize = 15, string orderBy = "CreatedDate DESC", UserDto currentUser = default, bool isCount = false);
        Task<List<AdminCategoryDto>> GetAll();
    }
}