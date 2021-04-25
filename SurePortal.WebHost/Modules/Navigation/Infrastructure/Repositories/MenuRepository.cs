using SurePortal.Core.Kernel.AmbientScope;
using SurePortal.WebHost.Modules.Navigation.Domain.POCO;
using SurePortal.WebHost.Modules.Navigation.Domain.Repositories;

namespace SurePortal.WebHost.Modules.Navigation.Infrastructure.Repositories
{
    public class MenuRepository : Repository<NavigationContext, Menu>, IMenuRepository
    {
        public MenuRepository(IAmbientDbContextLocator ambientDbContextLocator)
            : base(ambientDbContextLocator)
        {
        }
    }
}