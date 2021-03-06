using DaiPhatDat.Core.Kernel.Orgs.Models;
using DaiPhatDat.Module.Task.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DaiPhatDat.Module.Task.Services
{
    public interface IAttachmentService
    {
        Task<List<AttachmentDto>> GetAttachmentDtoHistoryProject(Guid projectId, Guid? historyId = null);

        Task<List<AttachmentDto>> GetAttachments(Guid projectId, Guid itemId, Source source = Source.Project);

        Task<Attachment> GetById(Guid id);
        List<AttachmentDto> GetAllAttachments(Guid? projectId = null, Guid? itemId = null, Source? source = null);
        Task<List<AttachmentDto>> GetAllAttachmentChilds(Guid projectId, Guid? itemId);
    }
}