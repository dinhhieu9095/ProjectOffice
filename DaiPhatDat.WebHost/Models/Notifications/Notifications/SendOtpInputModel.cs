using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurePortal.WebHost.Models.Notifications.Notifications
{
    public class SendOtpInputModel
    {
        public Guid UserId { get; set; }
        public string Mobile { get; set; }
        public string Otp { get; set; }
        public DateTime DueDate { get; set; }
        public string Time
        {
            get
            {
                return DueDate.ToString("HH:mm dd/MM/yyyy");
            }
        }
        public string SendOtpType { get; set; }
    }
}
