using DaiPhatDat.Core.Kernel.AmbientScope;
using DaiPhatDat.Core.Kernel.Orgs.Domain;
using DaiPhatDat.Core.Kernel.Orgs.Domain.Entities;
using DaiPhatDat.Core.Kernel.Orgs.Domain.Repositories;

namespace DaiPhatDat.Core.Kernel.Orgs.Infrastructure.Repositories
{
    public class UserDeviceRepository :
        Repository<OrgDbContext, UserDevice>, IUserDeviceRepository
    {
        public UserDeviceRepository(IAmbientDbContextLocator ambientDbContextLocator) : base(ambientDbContextLocator)
        {
        }


    }
}
