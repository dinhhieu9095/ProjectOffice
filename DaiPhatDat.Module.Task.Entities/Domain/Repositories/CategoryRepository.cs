using DaiPhatDat.Core.Kernel.AmbientScope;

namespace DaiPhatDat.Module.Task.Entities
{
    public class TaskItemCategoryRepository : Repository<TaskContext, TaskItemCategory>, ITaskItemCategoryRepository
    {
        public TaskItemCategoryRepository(IAmbientDbContextLocator ambientDbContextLocator)
            : base(ambientDbContextLocator)
        {
        }

    }

    public class ProjectCategoryRepository : Repository<TaskContext, ProjectCategory>, IProjectCategoryRepository
    {
        public ProjectCategoryRepository(IAmbientDbContextLocator ambientDbContextLocator)
            : base(ambientDbContextLocator)
        {
        }

    }
    public class ProjectPriorityRepository : Repository<TaskContext, ProjectPriority>, IProjectPriorityRepository
    {
        public ProjectPriorityRepository(IAmbientDbContextLocator ambientDbContextLocator)
            : base(ambientDbContextLocator)
        {
        }

    }
    public class ProjectTypeRepository : Repository<TaskContext, ProjectType>, IProjectTypeRepository
    {
        public ProjectTypeRepository(IAmbientDbContextLocator ambientDbContextLocator)
            : base(ambientDbContextLocator)
        {
        }

    }
    public class ProjectStatusRepository : Repository<TaskContext, ProjectStatus>, IProjectStatusRepository
    {
        public ProjectStatusRepository(IAmbientDbContextLocator ambientDbContextLocator)
            : base(ambientDbContextLocator)
        {
        }

    }
    public class TaskItemStatusRepository : Repository<TaskContext, TaskItemStatus>, ITaskItemStatusRepository
    {
        public TaskItemStatusRepository(IAmbientDbContextLocator ambientDbContextLocator)
            : base(ambientDbContextLocator)
        {
        }

    }

    public class TaskItemPriorityRepository : Repository<TaskContext, TaskItemPriority>, ITaskItemPriorityRepository
    {
        public TaskItemPriorityRepository(IAmbientDbContextLocator ambientDbContextLocator)
            : base(ambientDbContextLocator)
        {
        }

    }

    public class NatureTaskRepository : Repository<TaskContext, NatureTask>, INatureTaskRepository
    {
        public NatureTaskRepository(IAmbientDbContextLocator ambientDbContextLocator)
            : base(ambientDbContextLocator)
        {
        }

    }

    public class ActionRepository : Repository<TaskContext, Action>, IActionRepository
    {
        public ActionRepository(IAmbientDbContextLocator ambientDbContextLocator)
            : base(ambientDbContextLocator)
        {
        }

    }
}
