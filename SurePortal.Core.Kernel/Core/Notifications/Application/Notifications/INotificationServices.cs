using SurePortal.Core.Kernel.Notifications.Application.Dto;
using SurePortal.Core.Kernel.Notifications.Application.Notifications.Dto;
using SurePortal.Core.Kernel.Notifications.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurePortal.Core.Kernel.Notifications.Application
{
    public interface INotificationServices
    {
        Task<IReadOnlyList<NotificationDto>> GetMessagesAsync(Guid userId, NotificationActionTypes actionType);
        Task<IReadOnlyList<NotificationDto>> SearchListAsync(Guid userId,
            string moduleCode,
            NotificationActionTypes actionType);
        Task<IReadOnlyList<NotificationSettingDto>> GetSettingsByUserAsync(Guid userId);
        Task<Guid> AddAsync(CreateNotificationDto notificationDto);
        Task<bool> DeleteAsync(Guid userId, List<Guid> deletedIds);
        Task<bool> UpdateReadStatusAsync(Guid userId, List<Guid> ids, bool isRead);
        Task SendOtpAsync(SendOtpInput input);
    }
}