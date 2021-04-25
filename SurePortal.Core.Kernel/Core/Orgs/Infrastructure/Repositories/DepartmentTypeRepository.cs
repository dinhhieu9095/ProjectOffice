using SurePortal.Core.Kernel.AmbientScope;
using SurePortal.Core.Kernel.Orgs.Domain;
using SurePortal.Core.Kernel.Orgs.Domain.Entities;
using SurePortal.Core.Kernel.Orgs.Domain.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace SurePortal.Core.Kernel.Orgs.Infrastructure.Repositories
{
    public class DepartmentTypeRepository : Repository<OrgDbContext, DepartmentType>,
        IDepartmentTypeRepository
    {
        public DepartmentTypeRepository(IAmbientDbContextLocator ambientDbContextLocator)
            : base(ambientDbContextLocator)
        {
        }

        public async Task<List<DepartmentType>> GetDepartmentTypes()
        {
            return await DbSet.AsNoTracking()
                .Where(w => w.IsActive)
                .OrderBy(o => o.Name)
                .ToListAsync();
        }
    }
}
