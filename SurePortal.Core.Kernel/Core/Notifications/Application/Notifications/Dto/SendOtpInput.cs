using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurePortal.Core.Kernel.Notifications.Application.Notifications.Dto
{
    public class SendOtpInput
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
