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
using System.Data;

namespace DaiPhatDat.Module.Task.Services
{
    public class AdminCategoryService : IAdminCategoryService
    {
        private readonly ILoggerServices _loggerServices;
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IMapper _mapper;
        private readonly IAdminCategoryRepository _objectRepository;
        private readonly ITaskItemRepository _taskItemRepository;
        public AdminCategoryService(ILoggerServices loggerServices, IDbContextScopeFactory dbContextScopeFactory, IMapper mapper, IAdminCategoryRepository objectRepository, ITaskItemRepository taskItemRepository)
        {
            _loggerServices = loggerServices;
            _dbContextScopeFactory = dbContextScopeFactory;
            _mapper = mapper;
            _objectRepository = objectRepository;
            _taskItemRepository = taskItemRepository;
        }

        public Pagination<FetchProjectsTasksResult> GetTaskWithFilterPaging(string keyWord, List<string> paramValues, int page = 1, int pageSize = 15, string orderBy = "CreatedDate DESC", UserDto currentUser = default, bool isCount = false)
        {
            var dataResult = new Pagination<FetchProjectsTasksResult>(0, null);
            dataResult.Result = new List<FetchProjectsTasksResult>();
            try
            {
                if (string.IsNullOrEmpty(orderBy)) orderBy = "CreatedDate DESC";

                var sqlQuery = new StringBuilder("EXEC [dbo].[SP_Select_AdminCategories] ");
                sqlQuery.Append(" @ProjectId = '' ");
                foreach (var param in paramValues)
                {
                    if (!string.IsNullOrEmpty(param))
                    {
                        var arrParams = param.Split(new[] { ':' }, 2);
                        var key = arrParams[0];
                        var value = arrParams[1];
                        sqlQuery.AppendFormat(", {0} = '{1}' ", key, value);
                    }
                }
                if (isCount)
                {
                    sqlQuery.AppendFormat(", {0} = '{1}' ", "@Page", page);
                    sqlQuery.AppendFormat(", {0} = '{1}' ", "@PageSize", pageSize);
                    sqlQuery.AppendFormat(", {0} = '{1}' ", "@IsCount", 1);
                }
                using (var scope = _dbContextScopeFactory.CreateReadOnly())
                {
                    var dbContext = scope.DbContexts.Get<TaskContext>();
                    var dataEntity = dbContext.Database.SqlQuery<FetchProjectsTasksResult>(sqlQuery.ToString()).ToList();
                    dataResult.Count = (dataEntity != null && dataEntity.Count > 0) ? dataEntity[0].TotalRecord : 0;
                    dataResult.Skip = page;
                    dataResult.Take = pageSize;
                    dataResult.Result = dataEntity;
                }
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.Message);
            }
            return dataResult;
        }
        public async Task<SendMessageResponse> SaveAsync(AdminCategoryDto dto)
        {
            SendMessageResponse sendMessage = new SendMessageResponse();
            try
            {
                using (var scope = _dbContextScopeFactory.Create())
                {
                    AdminCategory entity = _objectRepository.GetAll().Where(e => e.Id == dto.Id && e.Id != Guid.Empty).FirstOrDefault();
                    if (entity != null)
                    {
                        entity.Summary = dto.Summary;
                        entity.ModifiedBy = dto.ModifiedBy;
                        entity.ModifiedDate = dto.ModifiedDate;
                        _objectRepository.Modify(entity);
                    } else
                    {
                        dto.Id = Guid.NewGuid();
                        dto.CreatedBy = dto.ModifiedBy;
                        dto.CreatedDate = dto.ModifiedDate;
                        dto.IsActive = true;
                        entity = _mapper.Map<AdminCategory>(dto);
                        _objectRepository.Add(entity);
                    }
                    await scope.SaveChangesAsync();
                }
                sendMessage = SendMessageResponse.CreateSuccessResponse(string.Empty);
                return sendMessage;
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
                sendMessage = SendMessageResponse.CreateFailedResponse(string.Empty);
                return sendMessage;
            }
        }
        public async Task<SendMessageResponse> Delete(AdminCategoryDto dto)
        {
            SendMessageResponse sendMessage = new SendMessageResponse();
            try
            {
                List<Guid> Ids = new List<Guid>();
                using (var scope = _dbContextScopeFactory.Create())
                {
                    AdminCategory entity = await _objectRepository.FindAsync(e => e.Id == dto.Id);
                    if (entity == null)
                    {
                        sendMessage = SendMessageResponse.CreateFailedResponse("NotFound");
                        return sendMessage;
                    }
                    entity.IsActive = false;
                    entity.ModifiedBy = dto.ModifiedBy;
                    entity.ModifiedDate = dto.ModifiedDate;
                    _objectRepository.Modify(entity, new List<Expression<Func<AdminCategory, object>>>() { p => p.ModifiedBy, p => p.ModifiedDate, p => p.IsActive });
                    await scope.SaveChangesAsync();
                    sendMessage = SendMessageResponse.CreateSuccessResponse(string.Empty);

                    return sendMessage;
                }
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
                sendMessage = SendMessageResponse.CreateFailedResponse(string.Empty);
                return sendMessage;
            }
        }
        public AdminCategoryDto GetById(Guid id)
        {
            AdminCategoryDto dto = new AdminCategoryDto();
            try
            {
                using (var scope = _dbContextScopeFactory.CreateReadOnly())
                {
                    AdminCategory entity = _objectRepository.GetAll().Where(p => p.IsActive == true && p.Id == id).FirstOrDefault();
                    dto = _mapper.Map<AdminCategoryDto>(entity);
                    dto.TaskItems = null;
                }
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
            }
            return dto;
        }
        public async Task<List<AdminCategoryDto>> GetAll()
        {
            using (var scope = _dbContextScopeFactory.CreateReadOnly())
            {
                var projectTypes = await _objectRepository.GetAsync(e => e.IsActive == true);

                return _mapper.Map<List<AdminCategoryDto>>(projectTypes);
            }
        }
    }
}
