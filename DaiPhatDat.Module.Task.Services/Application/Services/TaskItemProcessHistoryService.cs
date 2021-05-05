using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DaiPhatDat.Core.Kernel;
using DaiPhatDat.Core.Kernel.AmbientScope;
using DaiPhatDat.Core.Kernel.Firebase.Models;
using DaiPhatDat.Core.Kernel.Logger.Application;
using DaiPhatDat.Core.Kernel.Models;
using DaiPhatDat.Core.Kernel.Orgs.Application;
using DaiPhatDat.Core.Kernel.Orgs.Application.Dto;
using DaiPhatDat.Module.Task.Entities;

namespace DaiPhatDat.Module.Task.Services
{
    public class TaskItemProcessHistoryService : ITaskItemProcessHistoryService
    {
        private readonly ILoggerServices _loggerServices;
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly ITaskItemProcessHistoryRepository _taskItemProcessHistoryRepository;
        private readonly IMapper _mapper;
        private readonly IUserServices _userServices;

        public TaskItemProcessHistoryService(ILoggerServices loggerServices, IDbContextScopeFactory dbContextScopeFactory, IMapper mapper, IUserServices userServices, ITaskItemProcessHistoryRepository taskItemProcessHistoryRepository)
        {
            _loggerServices = loggerServices;
            _dbContextScopeFactory = dbContextScopeFactory;
            _mapper = mapper;
            _taskItemProcessHistoryRepository = taskItemProcessHistoryRepository;
            _userServices = userServices;
        }

        public async Task<List<TaskItemProcessHistory>> GetProcessHistoryByTaskItem(Guid taskItemId)
        {
            using (var scope = _dbContextScopeFactory.CreateReadOnly())
            {
                var asdfas = _taskItemProcessHistoryRepository.GetAll().ToList();
                return _taskItemProcessHistoryRepository.GetAll().Where(tfd => tfd.TaskItemAssignId == null && tfd.TaskItemId.HasValue && tfd.TaskItemId == taskItemId).OrderBy(tfd => tfd.CreatedDate).ToList();
            }
        }
    }
}
