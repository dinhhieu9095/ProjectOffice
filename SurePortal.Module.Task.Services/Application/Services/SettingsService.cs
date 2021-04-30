using AutoMapper;
using Newtonsoft.Json;
using SurePortal.Core.Kernel;
using SurePortal.Core.Kernel.AmbientScope;
using SurePortal.Core.Kernel.Logger.Application;
using SurePortal.Core.Kernel.Models;
using SurePortal.Module.Task.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurePortal.Module.Task.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly ILoggerServices _loggerServices;
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly ISettingsRepository _settingsRepository;
        private readonly IMapper _mapper;
        public SettingsService(ILoggerServices loggerServices, IDbContextScopeFactory dbContextScopeFactory, IMapper mapper, ISettingsRepository settingsRepository)
        {
            _loggerServices = loggerServices;
            _dbContextScopeFactory = dbContextScopeFactory;
            _settingsRepository = settingsRepository;
            _mapper = mapper;
        }

        public async Task<List<SettingsDto>> GetByKeys(List<string> keys)
        {
            var result = new List<SettingsDto>();
            using (var scope = _dbContextScopeFactory.CreateReadOnly())
            {
                var data = _settingsRepository.GetAll().Where(e => keys.Contains(e.Code)).ToList();

                result = _mapper.Map<List<SettingsDto>>(data);
            }

            return result;
        }
        public SettingsDto GetByKey(string key)
        {
            var result = new SettingsDto();
            using (var scope = _dbContextScopeFactory.CreateReadOnly())
            {
                var data = _settingsRepository.GetAll().Where(e => key == e.Code).FirstOrDefault();

                result = _mapper.Map<SettingsDto>(data);
            }

            return result;
        }
        public async Task<bool> SaveAsync(List<SettingsDto> dtos)
        {
            using (var scope = _dbContextScopeFactory.Create())
            {
                foreach (var dto in dtos)
                {
                    var data = await _settingsRepository.FindAsync(e => dto.Code == e.Code);
                    if (data != null)
                    {

                        data.Name = dto.Name;
                        data.Value = dto.Value;
                        _settingsRepository.Modify(data);
                    }
                    else
                    {
                        data.Value = dto.Value;
                        data = _mapper.Map<Setting>(dto);
                        _settingsRepository.Add(data);
                    }
                }

                scope.SaveChanges();
            }
            return true;
        }
    }
}
