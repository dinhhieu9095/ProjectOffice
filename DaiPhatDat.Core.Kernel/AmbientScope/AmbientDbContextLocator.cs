using System.Data.Entity;

namespace DaiPhatDat.Core.Kernel.AmbientScope
{
    public class AmbientDbContextLocator : IAmbientDbContextLocator
    {
        public TDbContext Get<TDbContext>() where TDbContext : DbContext
        {
            return DbContextScope.GetAmbientScope()?.DbContexts.Get<TDbContext>();
        }
    }
}