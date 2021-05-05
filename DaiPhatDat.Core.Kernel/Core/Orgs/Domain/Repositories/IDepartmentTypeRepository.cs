using DaiPhatDat.Core.Kernel.AmbientScope;
using DaiPhatDat.Core.Kernel.Orgs.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DaiPhatDat.Core.Kernel.Orgs.Domain.Repositories
{
    public interface IDepartmentTypeRepository : IRepository<DepartmentType>
    {
        Task<List<DepartmentType>> GetDepartmentTypes();
    }
}
