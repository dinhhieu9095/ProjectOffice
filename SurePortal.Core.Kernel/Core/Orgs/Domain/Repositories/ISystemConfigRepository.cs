using SurePortal.Core.Kernel.AmbientScope;
using SurePortal.Core.Kernel.Orgs.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurePortal.Core.Kernel.Orgs.Domain.Repositories
{
    public interface ISystemConfigRepository : IRepository<SystemConfig>
    {
        Task<SystemConfig> GetSystemConfig(string code);

        Task<IEnumerable<SystemConfig>> GetAllSystemConfigs();
    }
}
