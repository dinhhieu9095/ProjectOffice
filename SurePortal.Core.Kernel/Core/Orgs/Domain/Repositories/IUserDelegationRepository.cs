using DaiPhatDat.Core.Kernel.AmbientScope;
using DaiPhatDat.Core.Kernel.Orgs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DaiPhatDat.Core.Kernel.Orgs.Domain.Repositories
{
    public interface IUserDelegationRepository : IRepository<UserDelegation>
    {
        Task<List<UserDelegation>> GetUserDelegationFromUserAsync(Guid fromUserId);
        Task<List<UserDelegation>> GetUserDelegationToUserAsync(Guid toUserId);
    }
}
