using DaiPhatDat.Core.Kernel.AmbientScope;
using DaiPhatDat.Core.Kernel.Orgs.Domain;
using DaiPhatDat.Core.Kernel.Orgs.Domain.Entities;
using DaiPhatDat.Core.Kernel.Orgs.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace DaiPhatDat.Core.Kernel.Orgs.Infrastructure.Repositories
{
    public class UserJobTitleRepository : Repository<OrgDbContext, UserJobTitle>,
        IUserJobTitleRepository
    {
        public UserJobTitleRepository(IAmbientDbContextLocator ambientDbContextLocator) :
            base(ambientDbContextLocator)
        {
        }

        public async Task<UserJobTitle> GetUserJobTitle(Guid id)
        {
            return await DbSet.FirstOrDefaultAsync(f => f.Id == id && f.IsActive == true);
        }

        public async Task<List<UserJobTitle>> GetListUserJobTitle()
        {
            return await DbSet.Where(w => w.IsActive == true)
                .OrderBy(o => o.OrderNumber)
                .ToListAsync();
        }
    }
}
