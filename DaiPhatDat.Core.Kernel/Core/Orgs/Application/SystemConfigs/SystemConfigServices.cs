using AutoMapper;
using DaiPhatDat.Core.Kernel.AmbientScope;
using DaiPhatDat.Core.Kernel.JavaScript;
using DaiPhatDat.Core.Kernel.JavaScript.DataSources;
using DaiPhatDat.Core.Kernel.Models;
using DaiPhatDat.Core.Kernel.Orgs.Application.Dto;
using DaiPhatDat.Core.Kernel.Orgs.Domain.Entities;
using DaiPhatDat.Core.Kernel.Orgs.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace DaiPhatDat.Core.Kernel.Orgs.Application
{
    public class SystemConfigServices : ISystemConfigServices
    {
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly ISystemConfigRepository _systemConfigRepository;
        private readonly IMapper _mapper;
        private static IReadOnlyList<SystemConfigDto> _systemConfigs = null;


        public SystemConfigServices(IDbContextScopeFactory dbContextScopeFactory,
            ISystemConfigRepository systemConfigRepository,
            IMapper mapper)
        {
            _dbContextScopeFactory = dbContextScopeFactory;
            _systemConfigRepository = systemConfigRepository;
            _mapper = mapper;
        }

        public async Task<SystemConfigDto> GetAsync(string code)
        {
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                var items = await GetListAsync();
                return items.FirstOrDefault(p => p.Code.Equals(code, StringComparison.OrdinalIgnoreCase));
            }
        }
        public async Task<string> GetValueAsync(string code)
        {
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                var items = await GetListAsync();
                return items
                    .Where(p => p.Code.Equals(code, StringComparison.OrdinalIgnoreCase))
                    .Select(p => p.Value)
                    .FirstOrDefault() + string.Empty;

            }
        }
        public async Task<string> GetValueAsync(SystemConfigKey key)
        {
            return await GetValueAsync(key.ToString());
        }
        public async Task<Pagination<SystemConfigDto>> GetPaginationAsync(DataManager dataManager)
        {
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                int count = _systemConfigRepository.GetAll().ExecuteCount(dataManager);
                List<SystemConfigDto> systemConfigs = new List<SystemConfigDto>();
                if (count > 0)
                {
                    systemConfigs = await _systemConfigRepository
                        .Query<SystemConfigDto>().Execute(dataManager)
                        .ToListAsync();
                }
                return new Pagination<SystemConfigDto>(count, systemConfigs);
            }
        }
        public async Task<IEnumerable<SystemConfigDto>> GetListAsync(bool force = false)
        {
            if (_systemConfigs == null || force)
            {
                using (_dbContextScopeFactory.CreateReadOnly())
                {
                    _systemConfigs = await _systemConfigRepository
                        .Query<SystemConfigDto>()
                        .OrderBy(systemConfig => systemConfig.Code).ToListAsync(); ;
                }
            }
            return _systemConfigs;
        }
        public async Task<SystemConfigDto> AddAsync(SystemConfigDto input)
        {
            using (var dbContext = _dbContextScopeFactory.Create())
            {
                var systemConfig = SystemConfig.Create(input.Code, input.Name, input.Value);
                _systemConfigRepository.Add(systemConfig);
                dbContext.SaveChanges();
                await GetListAsync(true);
                return _mapper.Map<SystemConfigDto>(systemConfig);
            }
        }
        public async Task UpdateAsync(SystemConfigDto input)
        {
            using (var dbContext = _dbContextScopeFactory.Create())
            {
                var systemConfig = await _systemConfigRepository
                    .FindAsync(p => p.Code.Equals(input.Code, StringComparison.OrdinalIgnoreCase));
                systemConfig.Update(input.Name, input.Value);
                dbContext.SaveChanges();
                await GetListAsync(true);
            }
        }
        public async Task DeleteAsync(string code)
        {
            using (var dbContext = _dbContextScopeFactory.Create())
            {
                var systemConfig = await _systemConfigRepository
                    .FindAsync(p => p.Code.Equals(code, StringComparison.OrdinalIgnoreCase));
                _systemConfigRepository.Delete(systemConfig);
                dbContext.SaveChanges();
                await GetListAsync(true);

            }
        }


    }
}
