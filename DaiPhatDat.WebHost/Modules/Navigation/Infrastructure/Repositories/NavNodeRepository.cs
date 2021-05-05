using DaiPhatDat.Core.Kernel.AmbientScope;
using DaiPhatDat.WebHost.Modules.Navigation.Domain.POCO;

namespace DaiPhatDat.WebHost.Modules.Navigation.Domain.Repositories
{
    public class NavNodeRepository : Repository<NavigationContext, NavNode>, INavNodeRepository
    {
        public NavNodeRepository(IAmbientDbContextLocator ambientDbContextLocator)
            : base(ambientDbContextLocator)
        {
        }
    }
}