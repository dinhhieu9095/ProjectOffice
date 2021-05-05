using DaiPhatDat.Core.Kernel.AmbientScope;
using DaiPhatDat.Core.Kernel.Orgs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DaiPhatDat.Core.Kernel.Orgs.Domain.Repositories
{
    public interface IUserHandOverRepository : IRepository<UserHandOver>
    {
        Task<List<UserHandOver>> GetUserHandOverToUserAsync(Guid toUserId);
        Task<List<UserHandOver>> GetUserHandOverFromUserAsync(Guid fromUserId);
    }
}
