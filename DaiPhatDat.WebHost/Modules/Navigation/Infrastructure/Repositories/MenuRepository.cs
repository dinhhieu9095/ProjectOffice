using DaiPhatDat.Core.Kernel.AmbientScope;
using DaiPhatDat.WebHost.Modules.Navigation.Domain.POCO;
using DaiPhatDat.WebHost.Modules.Navigation.Domain.Repositories;

namespace DaiPhatDat.WebHost.Modules.Navigation.Infrastructure.Repositories
{
    public class MenuRepository : Repository<NavigationContext, Menu>, IMenuRepository
    {
        public MenuRepository(IAmbientDbContextLocator ambientDbContextLocator)
            : base(ambientDbContextLocator)
        {
        }
    }
}