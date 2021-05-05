using DaiPhatDat.Core.Kernel.AmbientScope;

namespace DaiPhatDat.Module.Task.Entities
{
    public class ReportRepository : Repository<TaskContext, Report>, IReportRepository
    {
        public ReportRepository(IAmbientDbContextLocator ambientDbContextLocator)
            : base(ambientDbContextLocator)
        {
        }

    }
}
