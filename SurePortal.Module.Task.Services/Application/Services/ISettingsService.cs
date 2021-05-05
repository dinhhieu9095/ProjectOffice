using DaiPhatDat.Core.Kernel.Models;
using DaiPhatDat.Module.Task.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DaiPhatDat.Module.Task.Services
{
    public interface ISettingsService
    {
        Task<List<SettingsDto>> GetByKeys(List<string> keys);
        Task<bool> SaveAsync(List<SettingsDto> dtos);
        SettingsDto GetByKey(string key);
    }
}