using SurePortal.Core.Kernel.AmbientScope;
using SurePortal.Core.Kernel.Notifications.Domain.Entities;
using SurePortal.Core.Kernel.Notifications.Infrastruture;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace SurePortal.Core.Kernel.Notifications.Domain.Repositories
{
    public class NotificationSettingRepository : Repository<NotificationDbContext, NotificationSetting>,
        INotificationSettingRepository
    {
        public NotificationSettingRepository(IAmbientDbContextLocator ambientDbContextLocator)
            : base(ambientDbContextLocator)
        {

        }

        public async Task<List<NotificationSetting>> GetByUser(Guid userId)
        {
            return await DbSet.AsNoTracking()
                .OrderBy(o => o.IsUrgent).ThenBy(o => o.ModifiedDate)
                .Where(w => w.UserId == userId)
                .ToListAsync();
        }

    }
}