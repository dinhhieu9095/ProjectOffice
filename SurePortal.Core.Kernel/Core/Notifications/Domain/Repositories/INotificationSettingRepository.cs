using DaiPhatDat.Core.Kernel.AmbientScope;
using DaiPhatDat.Core.Kernel.Notifications.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DaiPhatDat.Core.Kernel.Notifications.Domain.Repositories
{
    public interface INotificationSettingRepository : IRepository<NotificationSetting>
    {
        Task<List<NotificationSetting>> GetByUser(Guid userId);
    }
}