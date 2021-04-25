using SurePortal.Core.Kernel.AmbientScope;
using SurePortal.Core.Kernel.Notifications.Domain.Entities;
using SurePortal.Core.Kernel.Notifications.Domain.ValueObjects;
using SurePortal.Core.Kernel.Notifications.Infrastruture;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace SurePortal.Core.Kernel.Notifications.Domain.Repositories
{
    public class NotificationRepository : Repository<NotificationDbContext, Notification>, INotificationRepository
    {
        public NotificationRepository(IAmbientDbContextLocator ambientDbContextLocator)
            : base(ambientDbContextLocator)
        {

        }

        public async Task<List<Notification>> GetByUser(Guid userId, NotificationActionTypes actionType)
        {
            var getDate = DateTime.Now.AddDays(-14);
            return await DbSet.AsNoTracking().Include(i => i.NotificationType)
                .OrderBy(o => o.ModuleCode).ThenByDescending(t => t.CreatedDate)
                .Where(w => !w.IsDeleted && w.CreatedDate >= getDate && w.RecipientId == userId
                && w.NotificationType.ActionType == actionType)
                .ToListAsync();
        }

        public async Task<List<Notification>> GetListAsync(Guid userId,
            string moduleCode,
            NotificationActionTypes actionType)
        {
            var getDate = DateTime.Now.AddDays(-14);
            return await DbSet.AsNoTracking()
                .Include(i => i.NotificationType)
                .OrderBy(t => t.CreatedDate)
                .Where(w =>
                !w.IsDeleted &&
                w.CreatedDate >= getDate &&
                w.RecipientId == userId &&
                w.ModuleCode.Equals(moduleCode, StringComparison.OrdinalIgnoreCase) &&
                w.NotificationType.ActionType == actionType)
                .ToListAsync();
        }
    }
}