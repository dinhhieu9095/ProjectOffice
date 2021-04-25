using SurePortal.Core.Kernel.Firebase.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurePortal.Core.Kernel.Firebase.Application
{
    public interface IFirebaseService
    {
        Task<SendMessageResponse> SendMessageAsync(SendMessageData messageData, List<Guid> userIds);
    }
}