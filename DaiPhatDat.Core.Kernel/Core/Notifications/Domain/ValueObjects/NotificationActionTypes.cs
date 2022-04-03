
namespace DaiPhatDat.Core.Kernel.Notifications.Domain.ValueObjects
{
    /// <summary>
    /// Các loại thông báo
    /// </summary>
    public enum NotificationActionTypes
    {
        /// <summary>
        /// Trên giao diện web
        /// </summary>
        Web = 1,
        /// <summary>
        /// Trên thiết bị di động
        /// </summary>
        Mobile = 2,
        /// <summary>
        /// Bằng sms
        /// </summary>
        Sms = 3,
        /// <summary>
        /// Bằng email
        /// </summary>
        Email = 4,
        Firebase = 5
    }
}
