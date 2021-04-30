using SurePortal.Core.Kernel.AmbientScope;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurePortal.Module.Task.Entities
{
    public interface IProjectHistoryRepository : IRepository<ProjectHistory>
    {
    }
    public class ProjectHistoryRepository : Repository<TaskContext, ProjectHistory>, IProjectHistoryRepository
    {
        public ProjectHistoryRepository(IAmbientDbContextLocator ambientDbContextLocator)
            : base(ambientDbContextLocator)
        {
        }
    }
}
