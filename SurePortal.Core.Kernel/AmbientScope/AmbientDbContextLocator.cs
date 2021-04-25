using System.Data.Entity;

namespace SurePortal.Core.Kernel.AmbientScope
{
    public class AmbientDbContextLocator : IAmbientDbContextLocator
    {
        public TDbContext Get<TDbContext>() where TDbContext : DbContext
        {
            return DbContextScope.GetAmbientScope()?.DbContexts.Get<TDbContext>();
        }
    }
}