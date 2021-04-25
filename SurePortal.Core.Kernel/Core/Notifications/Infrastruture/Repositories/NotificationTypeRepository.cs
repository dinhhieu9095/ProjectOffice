using SurePortal.Core.Kernel.AmbientScope;
using SurePortal.Core.Kernel.Notifications.Domain.Entities;
using SurePortal.Core.Kernel.Notifications.Domain.ValueObjects;
using SurePortal.Core.Kernel.Notifications.Infrastruture;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace SurePortal.Core.Kernel.Notifications.Domain.Repositories
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