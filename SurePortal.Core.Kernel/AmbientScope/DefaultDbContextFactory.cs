using System;
using System.Data.Entity;

namespace SurePortal.Core.Kernel.AmbientScope
{
    public class DefaultDbContextFactory : IDbContextFactory
    {
        public TDbContext CreateDbContext<TDbContext>() where TDbContext : DbContext
        {
            return Activator.CreateInstance<TDbContext>();
        }
    }
}