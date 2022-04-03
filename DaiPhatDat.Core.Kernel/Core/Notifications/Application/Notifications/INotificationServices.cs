using DaiPhatDat.Core.Kernel.Notifications.Application.Dto;
using DaiPhatDat.Core.Kernel.Notifications.Application.Notifications.Dto;
using DaiPhatDat.Core.Kernel.Notifications.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DaiPhatDat.Core.Kernel.Notifications.Application
{
    public interface INotificationServices
    {
        Task<IReadOnlyList<NotificationDto>> GetMessagesAsync(Guid userId, NotificationActionTypes actionType);
        NotificationDto GetById(Guid Id);
        Task<List<NotificationDto>> SearchListAsync(Guid userId,
            string moduleCode,
            NotificationActionTypes actionType, int size);
        int TotalNotificationAsync(Guid userId,
            string moduleCode,
            NotificationActionTypes actionType);
        Task<IReadOnlyList<NotificationSettingDto>> GetSettingsByUserAsync(Guid userId);
        Task<Guid> AddAsync(CreateNotificationDto notificationDto);
        Task<bool> AddRangeAsync(List<CreateNotificationDto> list);
        Task<bool> DeleteAsync(Guid userId, List<Guid> deletedIds);
        Task<bool> UpdateReadStatusAsync(Guid userId, List<Guid> ids, bool isRead);
        Task SendOtpAsync(SendOtpInput input);
    }
}