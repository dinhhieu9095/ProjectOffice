using SurePortal.Core.Kernel.AmbientScope;
using SurePortal.Core.Kernel.Linq;
using SurePortal.Core.Kernel.Orgs.Domain;
using SurePortal.Core.Kernel.Orgs.Domain.Entities;
using SurePortal.Core.Kernel.Orgs.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace SurePortal.Core.Kernel.Orgs.Infrastructure
{
    public class GroupRepository : Repository<OrgDbContext, Group>,
         IGroupRepository
    {
        public GroupRepository(IAmbientDbContextLocator ambientDbContextLocator) :
            base(ambientDbContextLocator)
        {
        }

    }
}