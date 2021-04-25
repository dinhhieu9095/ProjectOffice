
namespace SurePortal.Core.Kernel.Notifications.Domain.ValueObjects
{
    /// <summary>
    /// Các loại thông báo
    /// </summary>
    public enum NotificationActionTypes
    {
        /// <summary>
        /// Trên giao diện web
        /// </summary>
        Web,
        /// <summary>
        /// Trên thiết bị di động
        /// </summary>
        Mobile,
        /// <summary>
        /// Bằng sms
        /// </summary>
        Sms,
        /// <summary>
        /// Bằng email
        /// </summary>
        Email,
        Firebase
    }
}
