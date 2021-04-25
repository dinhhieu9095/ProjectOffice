using SurePortal.Core.Kernel.AmbientScope;
using SurePortal.WebHost.Modules.Navigation.Domain.POCO;

namespace SurePortal.WebHost.Modules.Navigation.Domain.Repositories
{
    public class NavNodeRepository : Repository<NavigationContext, NavNode>, INavNodeRepository
    {
        public NavNodeRepository(IAmbientDbContextLocator ambientDbContextLocator)
            : base(ambientDbContextLocator)
        {
        }
    }
}