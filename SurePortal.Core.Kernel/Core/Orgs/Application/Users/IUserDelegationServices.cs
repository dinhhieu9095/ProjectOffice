using DaiPhatDat.Core.Kernel.Orgs.Application.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DaiPhatDat.Core.Kernel.Orgs.Application
{
    public interface IUserDelegationServices
    {
        Task<List<UserDelegationDto>> GetUserDelegationFromUserAsync(Guid fromUserId);
        Task<List<UserDelegationDto>> GetUserDelegationToUserAsync(Guid toUserId);
    }
}