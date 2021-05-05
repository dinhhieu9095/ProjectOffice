using DaiPhatDat.Core.Kernel.AmbientScope;

namespace DaiPhatDat.Module.Task.Entities
{
    public class AttachmentRepository : Repository<TaskContext, Attachment>, IAttachmentRepository
    {
        public AttachmentRepository(IAmbientDbContextLocator ambientDbContextLocator)
            : base(ambientDbContextLocator)
        {
        }

    }
     
}
