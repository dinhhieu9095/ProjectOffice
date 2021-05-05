using SurePortal.Core.Kernel.AmbientScope;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurePortal.Module.Task.Entities
{
    public class ProjectFilterParamRepository : Repository<TaskContext, ProjectFilterParam>, IProjectFilterParamRepository
    {
        public ProjectFilterParamRepository(IAmbientDbContextLocator ambientDbContextLocator)
            : base(ambientDbContextLocator)
        {
        }
    }
    public class ProjectFolderRepository : Repository<TaskContext, ProjectFolder>, IProjectFolderRepository
    {
        public ProjectFolderRepository(IAmbientDbContextLocator ambientDbContextLocator)
            : base(ambientDbContextLocator)
        {
        }
    }
}
