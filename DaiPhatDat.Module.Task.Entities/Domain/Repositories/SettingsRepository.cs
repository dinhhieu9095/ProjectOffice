using DaiPhatDat.Core.Kernel.AmbientScope;

namespace DaiPhatDat.Module.Task.Entities
{
    public class SettingsRepository : Repository<TaskContext, Setting>, ISettingsRepository
    {
        public SettingsRepository(IAmbientDbContextLocator ambientDbContextLocator)
            : base(ambientDbContextLocator)
        {
        }

    }
     
}
