using System;
using System.Collections.Generic;

namespace DaiPhatDat.Core.Kernel.Notifications.Models
{
    public class UpdateNotificationInput
    {
        public List<Guid> Ids { get; set; } = new List<Guid>();
    }
}
