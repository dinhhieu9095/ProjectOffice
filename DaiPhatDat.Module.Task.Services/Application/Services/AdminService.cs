using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using RefactorThis.GraphDiff;
using DaiPhatDat.Core.Kernel;
using DaiPhatDat.Core.Kernel.AmbientScope;
using DaiPhatDat.Core.Kernel.Firebase.Models;
using DaiPhatDat.Core.Kernel.Helper;
using DaiPhatDat.Core.Kernel.Linq;
using DaiPhatDat.Core.Kernel.Logger.Application;
using DaiPhatDat.Core.Kernel.Models;
using DaiPhatDat.Core.Kernel.Orgs.Application;
using DaiPhatDat.Core.Kernel.Orgs.Application.Contract;
using DaiPhatDat.Core.Kernel.Orgs.Application.Dto;
using DaiPhatDat.Module.Task.Entities;
using SystemTask = System.Threading.Tasks.Task;
namespace DaiPhatDat.Module.Task.Services
{
    public class AdminService : IAdminService
    {
        private readonly ILoggerServices _loggerServices;
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IMapper _mapper;
        private readonly IProjectRepository _objectRepository;
        private readonly ITaskItemRepository _taskItemRepository;
        private readonly ICategoryService _categoryService;
        private readonly IDepartmentServices _departmentServices;
        private readonly IAttachmentService _attachmentService;
        private readonly IProjectCategoryRepository _projectCategoryRepository;
        private readonly IProjectHistoryRepository _projectHistoryRepository;
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IUserServices _userServices;
        private readonly IUserDepartmentServices _userDepartmentServices;
        private readonly IActionRepository _actionRepository;
        public AdminService(ILoggerServices loggerServices, IDbContextScopeFactory dbContextScopeFactory, IMapper mapper, IProjectRepository objectRepository, IUserServices userServices, ICategoryService categoryService, IDepartmentServices departmentServices, IProjectHistoryRepository projectHistoryRepository, IAttachmentRepository attachmentRepository, IProjectCategoryRepository projectCategoryRepository, IUserDepartmentServices userDepartmentServices, IAttachmentService attachmentService, ITaskItemRepository taskItemRepository, IActionRepository actionRepository)
        {
            _loggerServices = loggerServices;
            _dbContextScopeFactory = dbContextScopeFactory;
            _categoryService = categoryService;
            _mapper = mapper;
            _objectRepository = objectRepository;
            _departmentServices = departmentServices;
            _attachmentService = attachmentService;
            _userServices = userServices;
            _userDepartmentServices = userDepartmentServices;
            _projectCategoryRepository = projectCategoryRepository;
            _projectHistoryRepository = projectHistoryRepository;
            _attachmentRepository = attachmentRepository;
            _taskItemRepository = taskItemRepository;
            _actionRepository = actionRepository;
        }

        public Task<SendMessageResponse> SaveCategoryProjectAsync(ProjectDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
