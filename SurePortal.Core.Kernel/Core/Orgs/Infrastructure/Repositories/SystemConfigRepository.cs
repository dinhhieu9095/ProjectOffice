using DaiPhatDat.Core.Kernel.AmbientScope;
using DaiPhatDat.Core.Kernel.Orgs.Domain;
using DaiPhatDat.Core.Kernel.Orgs.Domain.Entities;
using DaiPhatDat.Core.Kernel.Orgs.Domain.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace DaiPhatDat.Core.Kernel.Orgs.Infrastructure.Repositories
{
    public class SystemConfigRepository : Repository<OrgDbContext, SystemConfig>,
        ISystemConfigRepository
    {
        public SystemConfigRepository(IAmbientDbContextLocator ambientDbContextLocator)
            : base(ambientDbContextLocator)
        {
        }

        public async Task<IEnumerable<SystemConfig>> GetAllSystemConfigs()
        {
            return await DbSet.AsNoTracking().OrderBy(o => o.Name).ToListAsync();
        }

        public async Task<SystemConfig> GetSystemConfig(string code)
        {
            return await DbSet.AsNoTracking().FirstOrDefaultAsync(f => f.Code == code);
        }
    }
}
