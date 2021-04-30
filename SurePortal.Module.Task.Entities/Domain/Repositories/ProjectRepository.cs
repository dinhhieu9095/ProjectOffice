using SurePortal.Core.Kernel.AmbientScope;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurePortal.Module.Task.Entities
{
    public class ProjectRepository: Repository<TaskContext, Project>, IProjectRepository
    {
        public ProjectRepository(IAmbientDbContextLocator ambientDbContextLocator)
            : base(ambientDbContextLocator)
        {
        }
    }

}
