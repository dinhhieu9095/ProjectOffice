using SurePortal.Core.Kernel.AmbientScope;
using SurePortal.Core.Kernel.Orgs.Domain;
using SurePortal.Core.Kernel.Orgs.Domain.Entities;
using SurePortal.Core.Kernel.Orgs.Domain.Repositories;

namespace SurePortal.Core.Kernel.Orgs.Infrastructure.Repositories
{
    public class UserDeviceRepository :
        Repository<OrgDbContext, UserDevice>, IUserDeviceRepository
    {
        public UserDeviceRepository(IAmbientDbContextLocator ambientDbContextLocator) : base(ambientDbContextLocator)
        {
        }


    }
}
