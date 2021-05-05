using DaiPhatDat.Core.Kernel.Notifications.Application.NotificationTypes;

namespace DaiPhatDat.Core.Kernel.Notifications.Models
{
    public class CrudNotificationTypeInput
    {
        public string Key { get; set; }
        public string Action { get; set; }
        public NotificationTypeDto Value { get; set; }
    }
}
