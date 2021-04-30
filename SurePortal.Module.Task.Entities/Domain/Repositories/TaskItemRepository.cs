using SurePortal.Core.Kernel.AmbientScope;

namespace SurePortal.Module.Task.Entities
{
    public class TaskItemRepository : Repository<TaskContext, TaskItem>, ITaskItemRepository
    {
        public TaskItemRepository(IAmbientDbContextLocator ambientDbContextLocator)
            : base(ambientDbContextLocator)
        {
        }

    }
    public class TaskItemAssignRepository : Repository<TaskContext, TaskItemAssign>, ITaskItemAssignRepository
    {
        public TaskItemAssignRepository(IAmbientDbContextLocator ambientDbContextLocator)
            : base(ambientDbContextLocator)
        {
        }

    }
}
