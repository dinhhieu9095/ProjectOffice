using AutoMapper;
using AutoMapper.QueryableExtensions;
using DaiPhatDat.Core.Kernel.AmbientScope;
using DaiPhatDat.Core.Kernel.Application.Utilities;
using DaiPhatDat.Core.Kernel.ExternalServices;
using DaiPhatDat.Core.Kernel.Linq;
using DaiPhatDat.Core.Kernel.Logger.Application;
using DaiPhatDat.Core.Kernel.Notifications.Application.Dto;
using DaiPhatDat.Core.Kernel.Notifications.Application.Notifications.Dto;
using DaiPhatDat.Core.Kernel.Notifications.Application.NotificationTypes;
using DaiPhatDat.Core.Kernel.Notifications.Domain.Entities;
using DaiPhatDat.Core.Kernel.Notifications.Domain.Repositories;
using DaiPhatDat.Core.Kernel.Notifications.Domain.ValueObjects;
using DaiPhatDat.Core.Kernel.Orgs.Application;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace DaiPhatDat.Core.Kernel.Notifications.Application
{
    /// <summary>
    /// Lớp dịch vụ cung cấp thông tin user
    /// </summary>
    public class NotificationServices : INotificationServices
    {
        #region Attributes

        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly INotificationRepository _notificationRepository;
        private readonly INotificationSettingRepository _notificationSettingRepository;
        private readonly ISystemConfigServices _systemConfigServices;
        private readonly ILoggerServices _loggerServices;
        private readonly INotificationTypeService _notificationTypeService;

        private readonly IMapper _mapper;

        #endregion Attributes

        #region Constructors

        public NotificationServices(
          IDbContextScopeFactory dbContextScopeFactory,
          INotificationRepository notificationRepository,
          INotificationSettingRepository notificationSettingRepository,
          IMapper mapper, ISystemConfigServices systemConfigServices, ILoggerServices loggerServices, INotificationTypeService notificationTypeService)
        {
            _dbContextScopeFactory = dbContextScopeFactory;
            _notificationRepository = notificationRepository;
            _notificationSettingRepository = notificationSettingRepository;
            _mapper = mapper;
            _systemConfigServices = systemConfigServices;
            _loggerServices = loggerServices;
            _notificationTypeService = notificationTypeService;
        }

        #endregion Constructors

        #region Methods

        public async Task<IReadOnlyList<NotificationDto>> GetMessagesAsync(Guid userId,
            NotificationActionTypes actionType)
        {
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                var models = await _notificationRepository.GetByUser(userId, actionType);
                return _mapper.Map<List<NotificationDto>>(models);
            }
        }
        public NotificationDto GetById(Guid Id)
        {
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                var model = _notificationRepository.Find(e=> e.Id == Id);
                return _mapper.Map<NotificationDto>(model);
            }
        }
        public async Task<List<NotificationDto>> SearchListAsync(Guid userId,
            string moduleCode,
            NotificationActionTypes actionType, int size)
        {
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                if (string.IsNullOrEmpty(moduleCode))
                {
                    return await _notificationRepository
                        .GetAll()
                        .Where(w =>
                        !w.IsDeleted &&
                        w.RecipientId == userId &&
                        (!w.NotificationTypeId.HasValue || w.NotificationType.ActionType == actionType))
                        .OrderByDescending(w => w.CreatedDate).Skip(size).Take(15)
                        .ProjectTo<NotificationDto>(_mapper.ConfigurationProvider)
                        .ToListAsync();
                }
                return await _notificationRepository
                          .GetAll()
                          .Where(w =>
                          !w.IsDeleted &&
                          w.RecipientId == userId &&
                          w.ModuleCode.Equals(moduleCode, StringComparison.OrdinalIgnoreCase) &&
                          (!w.NotificationTypeId.HasValue || w.NotificationType.ActionType == actionType))
                          .OrderByDescending(w => w.CreatedDate).Skip(size).Take(15)
                          .ProjectTo<NotificationDto>(_mapper.ConfigurationProvider)
                          .ToListAsync();
            }
        }

        public int TotalNotificationAsync(Guid userId,
            string moduleCode,
            NotificationActionTypes actionType)
        {
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                if (string.IsNullOrEmpty(moduleCode))
                {
                    return _notificationRepository
                        .GetAll().AsNoTracking().Include(e=>e.NotificationType)
                        .Where(w =>
                        !w.IsDeleted &&
                        w.RecipientId == userId &&
                        (!w.NotificationTypeId.HasValue || w.NotificationType.ActionType == actionType) && !w.IsRead)
                        .Count();
                }
                return _notificationRepository
                          .GetAll().AsNoTracking().Include(e => e.NotificationType)
                          .Where(w =>
                          !w.IsDeleted &&
                          w.RecipientId == userId &&
                          w.ModuleCode.Equals(moduleCode, StringComparison.OrdinalIgnoreCase) &&
                         (!w.NotificationTypeId.HasValue || w.NotificationType.ActionType == actionType) && !w.IsRead)
                          .Count();
            }
        }


        public async Task<IReadOnlyList<NotificationSettingDto>> GetSettingsByUserAsync(Guid userId)
        {
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                var models = await _notificationSettingRepository.GetByUser(userId);
                return _mapper.Map<List<NotificationSettingDto>>(models);
            }
        }

        public async Task<Guid> AddAsync(CreateNotificationDto notificationDto)
        {
            using (var scope = _dbContextScopeFactory.Create())
            {
                var notification = Notification.Create(notificationDto.NotificationTypeId,
                    notificationDto.ModuleCode, notificationDto.SenderId, notificationDto.RecipientId,
                   notificationDto.ObjectId, notificationDto.Url, notificationDto.Subject, notificationDto.Body,
                    notificationDto.AdditionalData);
                _notificationRepository.Add(notification);
                await scope.SaveChangesAsync();
                return notification.Id;
            }
        }
        public async Task<bool> AddRangeAsync(List<CreateNotificationDto> list)
        {
            try
            {
                using (var scope = _dbContextScopeFactory.Create())
                {
                    List<Notification> entities = new List<Notification>();
                    foreach (var notificationDto in list)
                    {
                        var notification = Notification.Create(notificationDto.NotificationTypeId,
                        notificationDto.ModuleCode, notificationDto.SenderId, notificationDto.RecipientId,
                       notificationDto.ObjectId, notificationDto.Url, notificationDto.Subject, notificationDto.Body,
                        notificationDto.AdditionalData);
                        notification.CreatedDate = DateTime.Now;
                        notification.ModifiedDate = DateTime.Now;
                        entities.Add(notification);
                    }
                    _notificationRepository.AddRange(entities);
                    return await scope.SaveChangesAsync() > 0;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<bool> UpdateReadStatusAsync(Guid userId, List<Guid> ids, bool isRead)
        {
            using (var scope = _dbContextScopeFactory.Create())
            {
                foreach (var notifyId in ids)
                {
                    var notifyItem = await _notificationRepository
                        .FindAsync(f => f.Id == notifyId && f.RecipientId == userId);
                    if (notifyItem != null)
                    {
                        notifyItem.UpdateStatus(isRead, false);
                        _notificationRepository.Modify(notifyItem);
                    }
                }
                await scope.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> DeleteAsync(Guid userId, List<Guid> deletedIds)
        {
            using (var scope = _dbContextScopeFactory.Create())
            {
                foreach (var notifyId in deletedIds)
                {
                    var notifyItem = await _notificationRepository
                        .FindAsync(f => f.Id == notifyId && f.RecipientId == userId);
                    if (notifyItem != null)
                    {
                        notifyItem.UpdateStatus(true, true);
                        _notificationRepository.Modify(notifyItem);
                    }
                }
                await scope.SaveChangesAsync();
                return true;
            }
        }

        public async Task SendOtpAsync(SendOtpInput input)
        {
            switch (input.SendOtpType.ToLower())
            {
                case "sendquick":
                    await SendQuickAsync(input);
                    return;
                case "vnptbranch":
                    await SendVnptBranchNameAsync(input);
                    return;
                default:
                    await SendDefaultOtpAsync(input);
                    return;
            }

        }

        #endregion Methods
        private async Task SendDefaultOtpAsync(SendOtpInput input)
        {
            var notificationTypes = await _notificationTypeService.GetListAsync("SendOtp");
            foreach (var notificationType in notificationTypes)
            {
                string subject = ReplaceUtility.ReplaceTextProps(notificationType.Description, input);
                string body = ReplaceUtility.ReplaceTextProps(notificationType.Template, input);
                var notification = Notification.Create(notificationType.Id,
                               notificationType.ModuleCode, null, input.UserId,
                               null, string.Empty, subject, body);
                _notificationRepository.Add(notification);
            }

        }
        private async Task SendQuickAsync(SendOtpInput input)
        {
            var smsServerUrl = await _systemConfigServices.GetValueAsync(SmsGateway.ServerUrlConfigKey);
            var smsOtpTemplate = await _systemConfigServices.GetValueAsync("SmsOtpTemplate");
            // prepare
            SmsSendRequest smsSendRequest = new SmsSendRequest();
            smsSendRequest.ServerUrl = smsServerUrl;
            smsSendRequest.MobileNumber = input.Mobile;
            smsSendRequest.Text = smsOtpTemplate
                .Replace("{otp}", input.Otp)
                .Replace("{time}", input.Time);
            smsSendRequest.IsUnicode = false;
            await SmsGateway.SendQuick_Send(smsSendRequest);
        }
        private async Task SendVnptBranchNameAsync(SendOtpInput input)
        {
            var vnptBranchSetting = new VNPTBranchSetting()
            {
                ServerAddress = await _systemConfigServices.GetValueAsync(SmsVNPTBranchGateway.ConfigServerAddress),
                AGENTID = await _systemConfigServices.GetValueAsync(SmsVNPTBranchGateway.ConfigAGENTID),
                APIPASS = await _systemConfigServices.GetValueAsync(SmsVNPTBranchGateway.ConfigAPIPASS),
                APIUSER = await _systemConfigServices.GetValueAsync(SmsVNPTBranchGateway.ConfigAPIUSER),
                CONTRACTID = await _systemConfigServices.GetValueAsync(SmsVNPTBranchGateway.ConfigCONTRACTID),
                CONTRACTTYPEID = await _systemConfigServices.GetValueAsync(SmsVNPTBranchGateway.ConfigCONTRACTTYPEID),
                LABELID = await _systemConfigServices.GetValueAsync(SmsVNPTBranchGateway.ConfigLABELID),
                TEMPLATEID = await _systemConfigServices.GetValueAsync(SmsVNPTBranchGateway.ConfigTEMPLATEID),
                USERNAME = await _systemConfigServices.GetValueAsync(SmsVNPTBranchGateway.ConfigUSERNAME)
            };
            List<VnptDirectParam> directParams = new List<VnptDirectParam>()
            {
                new VnptDirectParam(){  NUM=1,CONTENT=input.Otp },
                new VnptDirectParam(){  NUM=2,CONTENT=input.Time },
            };
            SmsVNPTBranchGateway.SendMessage(vnptBranchSetting, directParams, input.Mobile);
        }
    }
}