using DaiPhatDat.Core.Kernel.AmbientScope;
using DaiPhatDat.Core.Kernel.Orgs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaiPhatDat.Module.Task.Entities
{
    public class ProjectFilterParamRepository : Repository<TaskContext, ProjectFilterParam>, IProjectFilterParamRepository
    {
        public ProjectFilterParamRepository(IAmbientDbContextLocator ambientDbContextLocator)
            : base(ambientDbContextLocator)
        {
        }

        
    }
}
