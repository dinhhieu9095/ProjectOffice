using SurePortal.Core.Kernel.AmbientScope;

namespace SurePortal.Module.Task.Entities
{
    public class AttachmentRepository : Repository<TaskContext, Attachment>, IAttachmentRepository
    {
        public AttachmentRepository(IAmbientDbContextLocator ambientDbContextLocator)
            : base(ambientDbContextLocator)
        {
        }

    }
     
}
