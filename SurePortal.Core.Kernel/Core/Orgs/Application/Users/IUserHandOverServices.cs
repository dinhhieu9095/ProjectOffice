using SurePortal.Core.Kernel.Orgs.Application.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurePortal.Core.Kernel.Orgs.Application
{
    public interface IUserHandOverServices
    {
        Task<List<UserHandOverDto>> GetUserHandOverFromUserAsync(Guid fromUserId);
        Task<List<UserHandOverDto>> GetUserHandOverToUserAsync(Guid toUserId);
    }
}