using DaiPhatDat.Core.Kernel.AmbientScope;
using DaiPhatDat.Core.Kernel.Notifications.Domain.Entities;
using DaiPhatDat.Core.Kernel.Notifications.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DaiPhatDat.Core.Kernel.Notifications.Domain.Repositories
{
    public interface INotificationRepository : IRepository<Notification>
    {
        Task<List<Notification>> GetByUser(Guid userId, NotificationActionTypes actionType);
        Task<List<Notification>> GetListAsync(Guid userId, string moduleCode, NotificationActionTypes actionType);
    }
}