using SurePortal.Core.Kernel.AmbientScope;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurePortal.Module.Task.Entities
{
    public interface ITaskItemCategoryRepository : IRepository<TaskItemCategory>
    {
    }

    public interface IProjectCategoryRepository : IRepository<ProjectCategory>
    {
    }
    public interface IProjectPriorityRepository : IRepository<ProjectPriority>
    {
    }
    public interface IProjectTypeRepository : IRepository<ProjectType>
    {
    }
    public interface IProjectStatusRepository : IRepository<ProjectStatus>
    {
    }

    public interface ITaskItemStatusRepository : IRepository<TaskItemStatus>
    {
    }

    public interface ITaskItemPriorityRepository : IRepository<TaskItemPriority>
    {
    }
    public interface INatureTaskRepository : IRepository<NatureTask>
    {
    }

    public interface IActionRepository : IRepository<Action>
    {
    }
}
