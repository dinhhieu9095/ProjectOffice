using SurePortal.Core.Kernel.AmbientScope;
using SurePortal.Core.Kernel.Notifications.Domain.Entities;
using SurePortal.Core.Kernel.Notifications.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurePortal.Core.Kernel.Notifications.Domain.Repositories
{
    public interface INotificationRepository : IRepository<Notification>
    {
        Task<List<Notification>> GetByUser(Guid userId, NotificationActionTypes actionType);
        Task<List<Notification>> GetListAsync(Guid userId, string moduleCode, NotificationActionTypes actionType);
    }
}