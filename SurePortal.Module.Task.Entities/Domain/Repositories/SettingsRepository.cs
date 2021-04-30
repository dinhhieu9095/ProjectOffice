using SurePortal.Core.Kernel.AmbientScope;

namespace SurePortal.Module.Task.Entities
{
    public class SettingsRepository : Repository<TaskContext, Setting>, ISettingsRepository
    {
        public SettingsRepository(IAmbientDbContextLocator ambientDbContextLocator)
            : base(ambientDbContextLocator)
        {
        }

    }
     
}
