using AutoMapper;
using AutoMapper.QueryableExtensions;
using SurePortal.Core.Kernel.AmbientScope;
using SurePortal.Core.Kernel.Orgs.Application.Dto;
using SurePortal.Core.Kernel.Orgs.Domain.Entities;
using SurePortal.Core.Kernel.Orgs.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace SurePortal.Core.Kernel.Orgs.Application
{
    public class UserDeviceServices : IUserDeviceServices
    {
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IUserDeviceRepository _userDeviceRepository;
        private readonly IMapper _mapper;

        public UserDeviceServices(IDbContextScopeFactory dbContextScopeFactory,
            IUserDeviceRepository userDeviceRepository,
            IMapper mapper)
        {
            _dbContextScopeFactory = dbContextScopeFactory;
            _userDeviceRepository = userDeviceRepository;
            _mapper = mapper;
        }

        public async Task UpdateUserDeviceAsync(UserDeviceDto userDeviceDto)
        {
            if (userDeviceDto == null)
            {
                return;
            }

            using (var dbContext = _dbContextScopeFactory.Create())
            {
                var device = await _userDeviceRepository
                    .FindAsync(w => w.UserId == userDeviceDto.UserId && w.IMEI == userDeviceDto.IMEI);
                if (device == null)
                {
                    device = UserDevice.Create(userDeviceDto.UserId,
                        userDeviceDto.Name, userDeviceDto.AppName, userDeviceDto.AppVersion,
                        userDeviceDto.OSPlatform, userDeviceDto.OSVersion, userDeviceDto.SerialNumber,
                        userDeviceDto.IMEI, userDeviceDto.FireBaseToken);
                    _userDeviceRepository.Add(device);
                }
                else
                {
                    device.Update(
                        userDeviceDto.Name, userDeviceDto.AppName, userDeviceDto.AppVersion,
                        userDeviceDto.OSPlatform, userDeviceDto.OSVersion, userDeviceDto.FireBaseToken);
                }

                await dbContext.SaveChangesAsync();
            }
        }

        public async Task RemoveUserDeviceAsync(UserDeviceDto userDeviceDto)
        {
            if (userDeviceDto == null)
            {
                return;
            }

            using (var dbContext = _dbContextScopeFactory.Create())
            {
                var device = await _userDeviceRepository
                   .FindAsync(w => w.UserId == userDeviceDto.UserId && w.IMEI == userDeviceDto.IMEI);
                if (device != null)
                {
                    device.DeActive();
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        public async Task<IReadOnlyList<UserDeviceDto>> GetUserDevicesAsync(Guid userId)
        {
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                return await _userDeviceRepository
                     .GetAll()
                     .AsNoTracking()
                     .Where(device => device.IsActive && device.UserId == userId)
                     .ProjectTo<UserDeviceDto>(_mapper.ConfigurationProvider)
                     .ToListAsync();
            }
        }

        public async Task<IReadOnlyList<UserDeviceInfoDto>> GetUserDeviceInfosAsync(Guid userId)
        {
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                return await _userDeviceRepository
                     .GetAll()
                     .AsNoTracking()
                     .Where(device => device.IsActive && device.UserId == userId)
                     .ProjectTo<UserDeviceInfoDto>(_mapper.ConfigurationProvider)
                     .ToListAsync();
            }
        }
        public async Task<IReadOnlyList<UserDeviceInfoDto>> GetUserDeviceInfosAsync(List<Guid> userIds)
        {
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                return await _userDeviceRepository
                     .GetAll()
                     .AsNoTracking()
                     .Where(device => device.IsActive && userIds.Contains(device.UserId))
                     .ProjectTo<UserDeviceInfoDto>(_mapper.ConfigurationProvider)
                     .ToListAsync();
            }
        }
    }
}
