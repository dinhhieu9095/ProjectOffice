using SurePortal.Core.Kernel.AmbientScope;
using SurePortal.Core.Kernel.Notifications.Domain.Entities;
using SurePortal.Core.Kernel.Notifications.Domain.ValueObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurePortal.Core.Kernel.Notifications.Domain.Repositories
{
    public interface INotificationTypeRepository : IRepository<NotificationType>
    {
        Task<List<NotificationType>> GetByActionType(NotificationActionTypes? actionType);
    }
}