using DaiPhatDat.Core.Kernel.AmbientScope;

namespace DaiPhatDat.Module.Task.Entities
{
    public class TaskItemProcessHistoryRepository : Repository<TaskContext, TaskItemProcessHistory>, ITaskItemProcessHistoryRepository
    {
        public TaskItemProcessHistoryRepository(IAmbientDbContextLocator ambientDbContextLocator)
            : base(ambientDbContextLocator)
        {
        }

    }
}
