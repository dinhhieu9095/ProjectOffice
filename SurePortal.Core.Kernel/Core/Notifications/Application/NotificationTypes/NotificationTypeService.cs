using AutoMapper;
using AutoMapper.QueryableExtensions;
using SurePortal.Core.Kernel.AmbientScope;
using SurePortal.Core.Kernel.JavaScript;
using SurePortal.Core.Kernel.JavaScript.DataSources;
using SurePortal.Core.Kernel.Models;
using SurePortal.Core.Kernel.Notifications.Domain.Entities;
using SurePortal.Core.Kernel.Notifications.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using SystemTask = System.Threading.Tasks.Task;

namespace SurePortal.Core.Kernel.Notifications.Application.NotificationTypes
{
    public class NotificationTypeService : INotificationTypeService
    {
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly INotificationTypeRepository _notificationTypeRepository;
        private readonly IMapper _mapper;
        private static List<NotificationTypeDto> _notificationTypes = null;
        public NotificationTypeService(IDbContextScopeFactory dbContextScopeFactory,
            INotificationTypeRepository notificationTypeRepository,
            IMapper mapper)
        {
            _dbContextScopeFactory = dbContextScopeFactory;
            _notificationTypeRepository = notificationTypeRepository;
            _mapper = mapper;
        }
        public async Task<Pagination<NotificationTypeDto>> GetPaginationAsync(DataManager dataManager)
        {
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                int count = _notificationTypeRepository.GetAll().ExecuteCount(dataManager);
                List<NotificationTypeDto> notificationTypes = new List<NotificationTypeDto>();
                if (count > 0)
                {
                    notificationTypes = await _notificationTypeRepository.Query<NotificationTypeDto>().Execute(dataManager).ToListAsync();
                }
                return new Pagination<NotificationTypeDto>(count, notificationTypes);
            }
        }
        public async Task<IReadOnlyList<NotificationTypeDto>> GetListAsync(bool force = false)
        {
            if (_notificationTypes == null || force)
            {
                using (_dbContextScopeFactory.CreateReadOnly())
                {
                    _notificationTypes = await _notificationTypeRepository
                        .GetAll()
                        .OrderBy(notificationType => notificationType.Name)
                        .ProjectTo<NotificationTypeDto>(_mapper.ConfigurationProvider)
                        .ToListAsync();
                }
            }
            return _notificationTypes;
        }
        public async Task<List<NotificationTypeDto>> GetListAsync(string name)
        {
            var items = await GetListAsync();
            return items.Where(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase)).ToList();

        }
        public async Task<NotificationTypeDto> GetAsync(Guid id)
        {
            var items = await GetListAsync();
            var item = items.FirstOrDefault(p => p.Id == id);
            if (item == null)
            {
                return new NotificationTypeDto();
            }
            return item;
        }

        public async Task<NotificationTypeDto> AddAsync(NotificationTypeDto input)
        {
            using (var dbContext = _dbContextScopeFactory.Create())
            {
                var notificationType = NotificationType.Create(
                    input.Name, input.Description, input.Template, input.ModuleCode, input.ActionType);
                _notificationTypeRepository.Add(notificationType);
                dbContext.SaveChanges();
                await GetListAsync(true);
                return _mapper.Map<NotificationTypeDto>(notificationType);
            }
        }
        public async Task<NotificationTypeDto> CopyAsync(Guid id)
        {
            using (var dbContext = _dbContextScopeFactory.Create())
            {
                var item = await _notificationTypeRepository.FindAsync(p => p.Id == id);
                var copyItem = NotificationType.Create(
                    item.Name, item.Description, item.Template, item.ModuleCode, item.ActionType);
                _notificationTypeRepository.Add(copyItem);
                dbContext.SaveChanges();
                await GetListAsync(true);
                return _mapper.Map<NotificationTypeDto>(copyItem);
            }
        }
        public async SystemTask UpdateAsync(NotificationTypeDto input)
        {
            using (var dbContext = _dbContextScopeFactory.Create())
            {
                var notificationType = await _notificationTypeRepository.FindAsync(p => p.Id == input.Id);
                notificationType.Update(
                    input.Name, input.Description, input.Template, input.ModuleCode, input.ActionType);
                dbContext.SaveChanges();
                await GetListAsync(true);
            }
        }
        public async SystemTask DeleteAsync(Guid id)
        {
            using (var dbContext = _dbContextScopeFactory.Create())
            {
                var notificationType = await _notificationTypeRepository.FindAsync(p => p.Id == id);
                _notificationTypeRepository.Delete(notificationType);
                dbContext.SaveChanges();
                await GetListAsync(true);
            }
        }
    }
}