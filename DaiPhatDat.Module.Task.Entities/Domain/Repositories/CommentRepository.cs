using DaiPhatDat.Core.Kernel.AmbientScope;

namespace DaiPhatDat.Module.Task.Entities
{
    public class CommentRepository : Repository<TaskContext, Comment>, ICommentRepository
    {
        public CommentRepository(IAmbientDbContextLocator ambientDbContextLocator)
            : base(ambientDbContextLocator)
        {
        }

    }
     
    public class FileCommentRepository : Repository<TaskContext, FileComment>, IFileCommentRepository
    {
        public FileCommentRepository(IAmbientDbContextLocator ambientDbContextLocator)
            : base(ambientDbContextLocator)
        {
        }

    }
     
}
