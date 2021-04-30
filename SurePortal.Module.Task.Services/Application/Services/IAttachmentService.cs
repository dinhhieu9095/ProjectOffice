using SurePortal.Core.Kernel.Orgs.Models;
using SurePortal.Module.Task.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurePortal.Module.Task.Services
{
    public interface IAttachmentService
    {
        Task<List<AttachmentDto>> GetAttachmentDtoHistoryProject(Guid projectId, Guid? historyId = null);

        Task<List<AttachmentDto>> GetAttachments(Guid projectId, Guid itemId, Source source = Source.Project);

        Task<Attachment> GetById(Guid id);
    }
}