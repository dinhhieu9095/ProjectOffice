using SurePortal.Core.Kernel.Resources.Application.Dto;
using SurePortal.Core.Kernel.Resources.Domain.Entities;
using System.Data.Entity;

namespace SurePortal.Core.Kernel.Resources.Infrastructure
{
    public class ResourceDbContext : Context, IContext
    {
        public ResourceDbContext()
             : base()
        {
            // when loading entity, you should you Include method for every navigation properties you need
            Configuration.LazyLoadingEnabled = false;
            // we do not use lazy load, so proxy creation not necessary anymore
            Configuration.ProxyCreationEnabled = false;
        }

        /// <summary>
        /// Map to db set of Users
        /// </summary>
        public DbSet<Domain.Entities.Resources> Resources { get; set; }
    }
}