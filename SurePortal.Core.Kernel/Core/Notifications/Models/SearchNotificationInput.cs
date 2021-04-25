using SurePortal.Core.Kernel.Notifications.Domain.ValueObjects;

namespace SurePortal.Core.Kernel.Notifications.Models
{
    public class SearchNotificationInput
    {
        public string ModuleCode { get; set; }
        public NotificationActionTypes ActionType { get; set; }
    }
}
