using DaiPhatDat.Core.Kernel.AmbientScope;
using DaiPhatDat.Core.Kernel.Notifications.Domain.Entities;
using DaiPhatDat.Core.Kernel.Notifications.Domain.ValueObjects;
using DaiPhatDat.Core.Kernel.Notifications.Infrastruture;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace DaiPhatDat.Core.Kernel.Notifications.Domain.Repositories
{
    public class NotificationTypeRepository : Repository<NotificationDbContext, NotificationType>, INotificationTypeRepository
    {
        public NotificationTypeRepository(IAmbientDbContextLocator ambientDbContextLocator)
            : base(ambientDbContextLocator)
        {

        }

        public async Task<List<NotificationType>> GetByActionType(NotificationActionTypes? actionType)
        {
            if (actionType != null)
            {
                return await DbSet.AsNoTracking()
                    .OrderBy(o => o.CreatedDate).ThenBy(t => t.ActionTypeName)
                    .Where(w => !w.IsDeleted && w.ActionType == actionType.Value)
                    .ToListAsync();
            }
            else
            {
                return await DbSet.AsNoTracking()
                   .OrderBy(o => o.Name).ThenBy(t => t.ActionTypeName)
                   .Where(w => !w.IsDeleted && w.ActionType == actionType.Value)
                   .ToListAsync();
            }
        }
    }
}