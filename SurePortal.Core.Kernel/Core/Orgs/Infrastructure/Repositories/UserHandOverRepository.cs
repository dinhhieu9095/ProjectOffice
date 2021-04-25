﻿using SurePortal.Core.Kernel.AmbientScope;
using SurePortal.Core.Kernel.Orgs.Domain;
using SurePortal.Core.Kernel.Orgs.Domain.Entities;
using SurePortal.Core.Kernel.Orgs.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace SurePortal.Core.Kernel.Orgs.Infrastructure.Repositories
{
    public class UserHandOverRepository : Repository<OrgDbContext, UserHandOver>,
        IUserHandOverRepository
    {
        public UserHandOverRepository(IAmbientDbContextLocator ambientDbContextLocator)
            : base(ambientDbContextLocator)
        {
        }

        public async Task<List<UserHandOver>> GetUserHandOverFromUserAsync(Guid fromUserId)
        {
            return await DbSet.Where(w => w.FromUserID == fromUserId)
                .OrderByDescending(o => o.FromDate)
                .ToListAsync();
        }

        public async Task<List<UserHandOver>> GetUserHandOverToUserAsync(Guid toUserId)
        {
            return await DbSet.Where(w => w.ToUserID == toUserId)
                 .OrderByDescending(o => o.FromDate)
                 .ToListAsync();
        }
    }
}
