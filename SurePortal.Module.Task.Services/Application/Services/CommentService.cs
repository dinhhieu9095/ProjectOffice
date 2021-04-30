using AutoMapper;
using SurePortal.Core.Kernel.AmbientScope;
using SurePortal.Core.Kernel.Logger.Application;
using SurePortal.Core.Kernel.Orgs.Models;
using SurePortal.Module.Task.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SurePortal.Core.Kernel.Orgs.Application;
using System.Globalization;
using SystemTask = System.Threading.Tasks.Task;
using SurePortal.Core.Kernel.Orgs.Domain.Entities;
using SurePortal.Core.Kernel.Helper;

namespace SurePortal.Module.Task.Services
{
    public class CommentService : ICommentService
    {
        private readonly ILoggerServices _loggerServices;
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly ICommentRepository _commentRepository;
        private readonly IFileCommentRepository _fileCommentRepository;
        private readonly ITaskItemProcessHistoryService _taskItemProcessHistoryService;
        private readonly IMapper _mapper;
        private readonly IUserServices _userServices;

        public CommentService(ILoggerServices loggerServices, IDbContextScopeFactory dbContextScopeFactory, IMapper mapper, IUserServices userServices, IAttachmentRepository attachmentRepository, ITaskItemProcessHistoryService taskItemProcessHistoryService, ICommentRepository commentRepository, IFileCommentRepository fileCommentRepository)
        {
            _loggerServices = loggerServices;
            _dbContextScopeFactory = dbContextScopeFactory;
            _mapper = mapper;
            _userServices = userServices;
            _commentRepository = commentRepository;
            _fileCommentRepository = fileCommentRepository;
            _taskItemProcessHistoryService = taskItemProcessHistoryService;
        }

        public void Add(Comment item)
        {
            if (item != null)
            {
                using (var scope = _dbContextScopeFactory.Create())
                {
                    _commentRepository.Add(item);
                    scope.SaveChanges();
                }
            }
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public int Count(Guid objectId)
        {
            int count = GetAll().Count(x => x.ObjectID == objectId);

            return count;
        }

        public void Delete(Comment item)
        {
            throw new NotImplementedException();
        }

        public void Delete(object id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Comment> GetAll()
        {
            return _commentRepository.GetAll();
        }

        public Comment GetByID(object id)
        {
            Guid guid = new Guid(id.ToString());
            using (var scope = _dbContextScopeFactory.Create())
            {
                return _commentRepository.Find(x => x.ID == guid);
            }
        }

        public void Update(Comment item)
        {
            using (var scope = _dbContextScopeFactory.Create())
            {
                _commentRepository.Modify(item);
                scope.SaveChanges();
            }
        }
        public List<CommentDto> GetByObjectID(Guid objectId)
        {
            var userDto = _userServices.GetUsers();
            var rs = GetAll().Where(x => x.ObjectID == objectId)
                .Select(x => new CommentDto
                {
                    ID = x.ID,
                    Content = x.Content,
                    Created = x.Created,
                    UserID = x.UserID,
                    IsActive = x.IsActive,
                    ModuleCode = x.ModuleCode,
                    ObjectID = x.ObjectID,
                    HistoryContent = x.HistoryContent,
                    IsChange = x.IsChange
                })
                .OrderBy(x => x.Created).ToList();

            rs.ForEach(x =>
            {
                x.FullName = userDto.Where(y => y.Id == x.UserID)
                    .Select(y => y.FullName)
                    .FirstOrDefault() ?? String.Empty;
            });
            return rs;
        }

        public bool CreateComment(string content, Guid taskItemId, Guid currentUserId)
        {
            var comment = new Comment()
            {
                ID = Guid.NewGuid(),
                IsActive = true,
                UserID = currentUserId,
                Created = DateTime.Now,
                ObjectID = taskItemId,
                ModuleCode = "Task",
                Content = content,
                IsChange = false,
                HistoryContent = string.Empty
            };
            this.Add(comment);
            var data = new
            {
                code = 200,
                message = string.Empty
            };
            return true;
        }

        public bool UpdateComment(string content, Guid commentId, Guid currentUserId)
        {
            var comment = this.GetByID(commentId);
            comment.HistoryContent = comment.Content;
            comment.Content = content;
            comment.IsChange = true;
            this.Update(comment);
            return true;
        }

        public IEnumerable<FileCommentDto> FileComments(Guid commentId)
        { 
            using (var scope = _dbContextScopeFactory.Create())
            {
                return _fileCommentRepository.GetAll().Include(x => x.Comment)
                .Where(x => x.Comment.IsActive != null && x.Comment.IsActive == true)
                .Select(x => new FileCommentDto
                {
                    CommentID = x.CommentID,
                    ID = x.ID,
                    Name = x.Name,
                    Ext = x.Ext
                });
            }
        }
    }


}
