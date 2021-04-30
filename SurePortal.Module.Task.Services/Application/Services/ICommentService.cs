using SurePortal.Core.Kernel.Orgs.Models;
using SurePortal.Module.Task.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurePortal.Module.Task.Services
{
    public interface ICommentService
    {
        IEnumerable<FileCommentDto> FileComments(Guid commentId);

        List<CommentDto> GetByObjectID(Guid objectId);
        bool CreateComment(string content, Guid taskItemId, Guid currentUserId);
        bool UpdateComment(string content, Guid commentId, Guid currentUserId);

        int Count(Guid objectId);
    }
}