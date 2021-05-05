using DaiPhatDat.Core.Kernel.AmbientScope;
using DaiPhatDat.Core.Kernel.Orgs.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DaiPhatDat.Core.Kernel.Orgs.Domain.Repositories
{
    public interface ISystemConfigRepository : IRepository<SystemConfig>
    {
        Task<SystemConfig> GetSystemConfig(string code);

        Task<IEnumerable<SystemConfig>> GetAllSystemConfigs();
    }
}
