using DaiPhatDat.Core.Kernel.AmbientScope;
using DaiPhatDat.Core.Kernel.Notifications.Domain.Entities;
using DaiPhatDat.Core.Kernel.Notifications.Domain.ValueObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DaiPhatDat.Core.Kernel.Notifications.Domain.Repositories
{
    public interface INotificationTypeRepository : IRepository<NotificationType>
    {
        Task<List<NotificationType>> GetByActionType(NotificationActionTypes? actionType);
    }
}