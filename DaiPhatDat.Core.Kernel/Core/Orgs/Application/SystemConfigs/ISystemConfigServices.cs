using DaiPhatDat.Core.Kernel.JavaScript;
using DaiPhatDat.Core.Kernel.Models;
using DaiPhatDat.Core.Kernel.Orgs.Application.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DaiPhatDat.Core.Kernel.Orgs.Application
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
