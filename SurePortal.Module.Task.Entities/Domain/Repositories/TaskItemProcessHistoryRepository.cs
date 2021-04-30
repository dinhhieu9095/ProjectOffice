using SurePortal.Core.Kernel.AmbientScope;

namespace SurePortal.Module.Task.Entities
{
    public class TaskItemProcessHistoryRepository : Repository<TaskContext, TaskItemProcessHistory>, ITaskItemProcessHistoryRepository
    {
        public TaskItemProcessHistoryRepository(IAmbientDbContextLocator ambientDbContextLocator)
            : base(ambientDbContextLocator)
        {
        }

    }
}
