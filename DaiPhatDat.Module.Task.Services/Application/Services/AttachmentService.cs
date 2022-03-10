using AutoMapper;
using DaiPhatDat.Core.Kernel.AmbientScope;
using DaiPhatDat.Core.Kernel.Logger.Application;
using DaiPhatDat.Core.Kernel.Orgs.Models;
using DaiPhatDat.Module.Task.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DaiPhatDat.Core.Kernel.Orgs.Application;
using System.Globalization;
using SystemTask = System.Threading.Tasks.Task;
using DaiPhatDat.Core.Kernel.Orgs.Domain.Entities;
using DaiPhatDat.Core.Kernel.Helper;

namespace DaiPhatDat.Module.Task.Services
{
    public class AttachmentService : IAttachmentService
    {
        private readonly ILoggerServices _loggerServices;
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly ITaskItemAssignRepository _taskItemAssignRepository;
        private readonly ITaskItemProcessHistoryService _taskItemProcessHistoryService;
        private readonly IMapper _mapper;
        private readonly IUserServices _userServices;

        public AttachmentService(ILoggerServices loggerServices, IDbContextScopeFactory dbContextScopeFactory, IMapper mapper, IUserServices userServices, IAttachmentRepository attachmentRepository, ITaskItemProcessHistoryService taskItemProcessHistoryService, ITaskItemAssignRepository taskItemAssignRepository)
        {
            _loggerServices = loggerServices;
            _dbContextScopeFactory = dbContextScopeFactory;
            _mapper = mapper;
            _userServices = userServices;
            _attachmentRepository = attachmentRepository;
            _taskItemProcessHistoryService = taskItemProcessHistoryService;
            _taskItemAssignRepository = taskItemAssignRepository;
        }

        public async Task<List<AttachmentDto>> GetAttachmentDtoHistoryProject(Guid projectId, Guid? historyId = null)
        {
            using (var scope = _dbContextScopeFactory.CreateReadOnly())
            {
                if (historyId != null)
                    return await _attachmentRepository.GetAll()
                        .Include(i => i.ProcessHistory)
                        .OrderByDescending(o => o.CreatedDate)
                        .Where(w => w.ProjectId == projectId && w.ItemId == historyId.GetValueOrDefault() && w.Source == Source.Project)
                           .Select(e => new AttachmentDto()
                           {
                               FileName = e.FileName,
                               Id = e.Id,
                               FileExt = e.FileExt,
                               CreatedBy = e.CreatedBy,
                               CreatedDate = e.CreatedDate,
                               Source = e.Source
                           }).ToListAsync();

                return await _attachmentRepository.GetAll()
                        .Include(i => i.ProcessHistory)
                        .OrderByDescending(o => o.CreatedDate)
                        .Where(w => w.ProjectId == projectId && w.ProjectId != w.ItemId && w.Source == Source.Project)
                           .Select(e => new AttachmentDto()
                           {
                               FileName = e.FileName,
                               Id = e.Id,
                               FileExt = e.FileExt,
                               CreatedBy = e.CreatedBy,
                               CreatedDate = e.CreatedDate,
                               Source = e.Source
                           }).ToListAsync();
            }
        }

        public async Task<List<AttachmentDto>> GetAttachments(Guid projectId, Guid itemId, Source source = Source.Project)
        {
            var result = new List<AttachmentDto>();
            using (var dbContextReadOnlyScope = _dbContextScopeFactory.CreateReadOnly())
            {
                if (source != Source.Project)
                {
                    var taskItemProcesses = await _taskItemProcessHistoryService.GetProcessHistoryByTaskItem(itemId);

                    var guilds = taskItemProcesses.Select(e => e.Id).ToList();

                    result = await _attachmentRepository.GetAll()
                        .Include(i => i.ProcessHistory)
                        .OrderByDescending(o => o.CreatedDate)
                        .Where(w => w.ProjectId == projectId && w.Source != Source.Project
                        && (w.ItemId == itemId || (w.ItemId.HasValue && guilds.Contains(w.ItemId.Value)))
                        )
                           //&& (w.ProcessHistory.TaskItemId == itemId || w.ProcessHistory.TaskItemAssignId == itemId))
                           .Select(e => new AttachmentDto()
                           {
                               FileName = e.FileName,
                               Id = e.Id,
                               ItemId = e.ItemId,
                               FileExt = e.FileExt,
                               CreatedBy = e.CreatedBy,
                               CreatedDate = e.CreatedDate,
                               Source = e.Source

                           }).ToListAsync();
                }
                else
                {
                    result = await _attachmentRepository.GetAll()
                       .Where(x => x.ProjectId == projectId && x.ItemId == null)
                       .Select(e => new AttachmentDto()
                       {
                           FileName = e.FileName,
                           Id = e.Id,
                           FileExt = e.FileExt,
                           CreatedBy = e.CreatedBy,
                           CreatedDate = e.CreatedDate,
                           Source = e.Source

                       }).ToListAsync();
                }
                var userDtos = _userServices.GetUsers();
                foreach (var attachment in result)
                {
                    if (attachment.CreatedBy != null)
                        attachment.CreateByFullName = userDtos
                            .FirstOrDefault(x => x.Id == attachment.CreatedBy.GetValueOrDefault())
                            ?.FullName ?? string.Empty;
                    attachment.DateFormat = ConvertToStringExtensions.DateToString(attachment.CreatedDate);
                }
            }

            return result;
        }

        public async Task<Attachment> GetById(Guid id)
        {
            using (var dbContextReadOnlyScope = _dbContextScopeFactory.CreateReadOnly())
            {
                return await _attachmentRepository.FindAsync(x => x.Id == id);
            }
        }
        public List<AttachmentDto> GetAllAttachments(Guid? projectId, Guid? itemId, Source? source)
        {
            List<AttachmentDto> attachmentDtos = new List<AttachmentDto>();
            var result = _attachmentRepository.GetAll();
            if (projectId.HasValue)
            {
                result = result.Where(e => e.ProjectId == projectId);
            }
            if (itemId.HasValue)
            {
                result = result.Where(e => e.ItemId == itemId);
            }
            if (source.HasValue)
            {
                result = result.Where(e => e.Source == source);
            }
            attachmentDtos = result.OrderByDescending(e => e.CreatedDate).Select(e => new AttachmentDto()
            {
                FileName = e.FileName,
                Id = e.Id,
                ItemId = e.ItemId,
                FileExt = e.FileExt,
                CreatedBy = e.CreatedBy,
                CreatedDate = e.CreatedDate,
                Source = e.Source
            }).ToList();
            var userDtos = _userServices.GetUsers();
            foreach (var attachment in attachmentDtos)
            {
                if (attachment.CreatedBy != null)
                    attachment.CreateByFullName = userDtos
                        .FirstOrDefault(x => x.Id == attachment.CreatedBy.GetValueOrDefault())
                        ?.FullName ?? string.Empty;
                attachment.DateFormat = ConvertToStringExtensions.DateToString(attachment.CreatedDate);
            }
            return attachmentDtos;
        }
        public async Task<List<AttachmentDto>> GetAllAttachmentChilds(Guid projectId, Guid? itemId)
        {
            var result = new List<AttachmentDto>();
            using (var scope = _dbContextScopeFactory.CreateReadOnly())
            {
                if (itemId.HasValue)
                {
                    var dbContext = scope.DbContexts.Get<TaskContext>();
                    IReadOnlyList<Guid> taskItemIds = new List<Guid>();
                    taskItemIds = dbContext.Database.SqlQuery<Guid>(
                      string.Format(@"
                EXEC dbo.SP_Select_TaskItem_Children
		            @Id= '{0}'", itemId),
                              new object[] { }).ToList();
                    var lstItemIds = _taskItemAssignRepository.GetAll().Where(e => e.TaskItemId.HasValue && taskItemIds.Contains(e.TaskItemId.Value)).Select(e => e.Id).ToList();
                    lstItemIds.AddRange(taskItemIds);
                    result = await _attachmentRepository.GetAll()
                        .OrderByDescending(o => o.CreatedDate)
                        .Where(w => w.ProjectId == projectId && w.Source != Source.Project
                        && (w.ItemId == itemId || (w.ItemId.HasValue && lstItemIds.Contains(w.ItemId.Value)))
                        )
                           .Select(e => new AttachmentDto()
                           {
                               FileName = e.FileName,
                               Id = e.Id,
                               ItemId = e.ItemId,
                               FileExt = e.FileExt,
                               CreatedBy = e.CreatedBy,
                               CreatedDate = e.CreatedDate,
                               Source = e.Source
                           }).ToListAsync();
                }
                else
                {
                    result = await _attachmentRepository.GetAll()
                       .Where(x => x.ProjectId == projectId)
                       .Select(e => new AttachmentDto()
                       {
                           FileName = e.FileName,
                           Id = e.Id,
                           FileExt = e.FileExt,
                           CreatedBy = e.CreatedBy,
                           CreatedDate = e.CreatedDate,
                           Source = e.Source

                       }).ToListAsync();
                }
                var userDtos = _userServices.GetUsers();
                foreach (var attachment in result)
                {
                    if (attachment.CreatedBy != null)
                        attachment.CreateByFullName = userDtos
                            .FirstOrDefault(x => x.Id == attachment.CreatedBy.GetValueOrDefault())
                            ?.FullName ?? string.Empty;
                    attachment.DateFormat = ConvertToStringExtensions.DateToString(attachment.CreatedDate);
                }
            }

            return result;
        }
    }


}
