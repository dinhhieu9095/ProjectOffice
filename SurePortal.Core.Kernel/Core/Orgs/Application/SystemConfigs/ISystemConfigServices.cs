using SurePortal.Core.Kernel.JavaScript;
using SurePortal.Core.Kernel.Models;
using SurePortal.Core.Kernel.Orgs.Application.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurePortal.Core.Kernel.Orgs.Application
{
    public interface ISystemConfigServices
    {
        Task<Pagination<SystemConfigDto>> GetPaginationAsync(DataManager dataManager);
        Task<SystemConfigDto> GetAsync(string code);
        Task<string> GetValueAsync(string code);
        Task<string> GetValueAsync(SystemConfigKey key);
        Task<SystemConfigDto> AddAsync(SystemConfigDto input);
        Task UpdateAsync(SystemConfigDto input);
        Task DeleteAsync(string code);
        Task<IEnumerable<SystemConfigDto>> GetListAsync(bool force = false);

    }
}
