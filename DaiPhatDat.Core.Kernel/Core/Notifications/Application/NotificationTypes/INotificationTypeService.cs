using DaiPhatDat.Core.Kernel.JavaScript;
using DaiPhatDat.Core.Kernel.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DaiPhatDat.Core.Kernel.Notifications.Application.NotificationTypes
{
    public interface INotificationTypeService
    {
        Task<NotificationTypeDto> AddAsync(NotificationTypeDto input);
        Task<NotificationTypeDto> CopyAsync(Guid id);
        Task UpdateAsync(NotificationTypeDto input);
        Task DeleteAsync(Guid id);
        Task<Pagination<NotificationTypeDto>> GetPaginationAsync(DataManager dataManager);
        Task<NotificationTypeDto> GetAsync(Guid id);
        Task<List<NotificationTypeDto>> GetListAsync(string name);
        Task<IReadOnlyList<NotificationTypeDto>> GetListAsync(bool force = false);
    }
}