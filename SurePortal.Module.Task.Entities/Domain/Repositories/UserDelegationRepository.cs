﻿using SurePortal.Core.Kernel.AmbientScope;
using SurePortal.Core.Kernel.Orgs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurePortal.Module.Task.Entities
{
    public class UserDelegationRepository : Repository<TaskContext, UserDelegation>, IUserDelegationRepository
    {
        public UserDelegationRepository(IAmbientDbContextLocator ambientDbContextLocator)
            : base(ambientDbContextLocator)
        {
        }
    }
}
