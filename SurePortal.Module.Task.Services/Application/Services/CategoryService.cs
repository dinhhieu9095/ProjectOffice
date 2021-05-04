using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SurePortal.Core.Kernel;
using SurePortal.Core.Kernel.AmbientScope;
using SurePortal.Core.Kernel.Firebase.Models;
using SurePortal.Core.Kernel.Linq;
using SurePortal.Core.Kernel.Logger.Application;
using SurePortal.Core.Kernel.Models;
using SurePortal.Core.Kernel.Orgs.Application;
using SurePortal.Core.Kernel.Orgs.Application.Dto;
using SurePortal.Module.Task.Entities;

namespace SurePortal.Module.Task.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ILoggerServices _loggerServices;
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IProjectCategoryRepository _projectCategoryRepository;
        private readonly ITaskItemCategoryRepository _taskItemCategoryRepository;
        private readonly ITaskItemStatusRepository _taskItemStatusRepository;
        private readonly ITaskItemPriorityRepository _taskItemPriorityRepository;
        private readonly ITaskItemRepository _taskItemRepository;
        private readonly INatureTaskRepository _natureTaskRepository;
        private readonly IProjectPriorityRepository _projectPriorityRepository;
        private readonly IProjectTypeRepository _projectTypeRepository;
        private readonly IProjectStatusRepository _projectStatusRepository;
        private readonly IMapper _mapper;
        private readonly IUserServices _userServices;
        private readonly ISettingsService _settingsService;

        public CategoryService(ILoggerServices loggerServices, IDbContextScopeFactory dbContextScopeFactory, IMapper mapper, ITaskItemCategoryRepository taskItemCategoryRepository, IUserServices userServices, IProjectCategoryRepository projectCategoryRepository, ITaskItemStatusRepository taskItemStatusRepository, ITaskItemPriorityRepository taskItemPriorityRepository, INatureTaskRepository natureTaskRepository, IProjectPriorityRepository projectPriorityRepository, IProjectTypeRepository projectTypeRepository, ISettingsService settingsService, ITaskItemRepository taskItemRepository, IProjectStatusRepository projectStatusRepository)
        {
            _loggerServices = loggerServices;
            _dbContextScopeFactory = dbContextScopeFactory;
            _projectCategoryRepository = projectCategoryRepository;
            _taskItemCategoryRepository = taskItemCategoryRepository;
            _taskItemStatusRepository = taskItemStatusRepository;
            _taskItemPriorityRepository = taskItemPriorityRepository;
            _natureTaskRepository = natureTaskRepository;
            _projectPriorityRepository = projectPriorityRepository;
            _projectTypeRepository = projectTypeRepository;
            _mapper = mapper;
            _userServices = userServices;
            _settingsService = settingsService;
            _taskItemRepository = taskItemRepository;
            _projectStatusRepository = projectStatusRepository;
        }


        #region ProjectCategory
        public async Task<List<ProjectCategoryDto>> GetProjectCategories(Guid userId)
        {
            using (var scope = _dbContextScopeFactory.CreateReadOnly())
            {
                var projectCategories = await _projectCategoryRepository.GetAsync(e => e.UserId == userId && e.IsActive == true);

                return _mapper.Map<List<ProjectCategoryDto>>(projectCategories);
            }
        }
        public async Task<List<string>> GetProjectCategoriesByProjectId(Guid projectId, Guid? taskId = null)
        {
            using (var scope = _dbContextScopeFactory.CreateReadOnly())
            {
                IReadOnlyList<ProjectCategory> projectCategories = await _projectCategoryRepository.GetAsync(e => e.ProjectId == projectId && e.IsActive == true);
                List<string> categories = projectCategories.Select(group => group.Name).ToList();
                if (taskId.HasValue)
                {
                    var taskItem = _taskItemRepository.GetAll().Where(e => e.Id == taskId && e.IsDeleted == false).FirstOrDefault();
                    string taskItemCategory = string.Empty;
                    if (taskItem != null)
                    {
                        taskItemCategory = taskItem.TaskItemCategory;
                    }
                    if (!string.IsNullOrEmpty(taskItemCategory))
                    {
                        categories.AddRange(taskItemCategory.Split(';').ToList());
                    }
                }
                categories = categories.Distinct().ToList();
                return categories;
            }
        }

        public async Task<List<ProjectCategoryDto>> GetAllOfProjectCategories()
        {
            using (var scope = _dbContextScopeFactory.CreateReadOnly())
            {
                var projectCategories = await _projectCategoryRepository.GetAsync(x => x.IsActive == true);

                return _mapper.Map<List<ProjectCategoryDto>>(projectCategories);
            }
        }
        #endregion
        #region ProjectPriority
        public async Task<List<ProjectPriorityDto>> GetAllOfProjectPriorities()
        {
            using (var scope = _dbContextScopeFactory.CreateReadOnly())
            {
                var projectPriorities = await _projectPriorityRepository.GetAsync(e => e.IsActive == true);

                return _mapper.Map<List<ProjectPriorityDto>>(projectPriorities);
            }
        }
        #endregion
        #region ProjectStatus

        public async Task<List<ProjectStatusDto>> GetAllProjectStatuses()
        {
            using (var scope = _dbContextScopeFactory.CreateReadOnly())
            {
                var taskItemStatuses = await _projectStatusRepository.GetAsync(e => e.IsActive == true);

                return _mapper.Map<List<ProjectStatusDto>>(taskItemStatuses);
            }
        }

        #endregion
        #region ProjectType
        public async Task<List<ProjectTypeDto>> GetAllOfProjectTypes()
        {
            using (var scope = _dbContextScopeFactory.CreateReadOnly())
            {
                var projectTypes = await _projectTypeRepository.GetAsync(e => e.IsActive == true);

                return _mapper.Map<List<ProjectTypeDto>>(projectTypes);
            }
        }
        #endregion
        #region TaskItemCategory
        public async Task<List<TaskItemCategoryDto>> GetTaskItemCategories(Guid userId)
        {
            using (var scope = _dbContextScopeFactory.CreateReadOnly())
            {
                var taskItemCategories = await _taskItemCategoryRepository.GetAsync(e => e.UserId == userId);

                return _mapper.Map<List<TaskItemCategoryDto>>(taskItemCategories);
            }
        }

        public async Task<List<TaskItemCategoryDto>> GetAllTaskItemCategories()
        {
            using (var scope = _dbContextScopeFactory.CreateReadOnly())
            {
                var taskItemCategories = await _taskItemCategoryRepository.GetAsync(x => x.IsActive == true);

                return _mapper.Map<List<TaskItemCategoryDto>>(taskItemCategories);
            }
        }
        #endregion

        #region TaskItemStatus

        public async Task<List<TaskItemStatusDto>> GetAllTaskItemStatuses()
        {
            using (var scope = _dbContextScopeFactory.CreateReadOnly())
            {
                var taskItemStatuses = await _taskItemStatusRepository.GetAsync(e => e.IsActive == true);

                return _mapper.Map<List<TaskItemStatusDto>>(taskItemStatuses);
            }
        }

        #endregion

        #region TaskItemPriority

        public async Task<List<TaskItemPriorityDto>> GetAllTaskItemPriorities()
        {
            using (var scope = _dbContextScopeFactory.CreateReadOnly())
            {
                var taskItemPriorities = await _taskItemPriorityRepository.GetAsync(x => x.IsActive == true);

                return _mapper.Map<List<TaskItemPriorityDto>>(taskItemPriorities);
            }
        }
        #endregion


        #region NatureTask

        public async Task<List<NatureTaskDto>> GetAllNatureTasks()
        {
            using (var scope = _dbContextScopeFactory.CreateReadOnly())
            {
                var natureTaskDtos = await _natureTaskRepository.GetAsync(x => x.IsActive == true);

                return _mapper.Map<List<NatureTaskDto>>(natureTaskDtos);
            }
        }
        #endregion
        public async Task<bool> PostTrackingUpdateDB()
        {
            try
            {
                using (var dbCtxScope = _dbContextScopeFactory.Create())
                {
                    SettingsDto versionTask = _settingsService
                           .GetByKey("CurrentTaskVersion");
                    string currentVersion = versionTask.Value;
                    int version = Int32.Parse(currentVersion);
                    List<string> queries = GetQueryUpdate(version);
                    foreach (string item in queries)
                    {
                        try
                        {
                            string query = item.Replace("GO", "");
                            await _projectCategoryRepository.SqlQueryAsync(typeof(bool), item);
                        }
                        catch (Exception ex)
                        {
                            _loggerServices.WriteError("PostTrackingUpdateDB at: " + item + " message: " + ex.ToString());
                            continue;
                        }

                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
                return false;
            }
        }
        public List<string> GetQueryUpdate(int currentVersion)
        {
            List<string> queries = new List<string>();
            int newVersion = currentVersion;
            if(newVersion < 20210503)
            {
                newVersion = 20210503;
                queries.Add(@"
  insert into  [SurePortal_DEV].[dbo].[Users] (ID,UserName,FullName,Gender,Email,IsActive,UserIndex,AccountName)
  values (NEWID(),'SWIC\spadmin','Quản trị viên', 1,'spadmin@gamil.com',1,1,'spadmin')");
            }
            if(newVersion < 20210504)
            {
                newVersion = 20210504;
                queries.Add(@"delete from  [Task].[Action];
INSERT INTO [Task].[Action]
           ([Id]
           ,[Name]
           ,[IsActive])
     VALUES
           (0,	N'Tạo mới',	1),
(1,	N'Giao việc',	1),
(2,	N'Chỉnh sửa',	1),
(3,	N'Xử lý',	1),
(4,	N'Kết thúc',	1),
(5,	N'Trả lại',	1),
(6,	N'Báo cáo',	1),
(7,	N'Gia hạn',	1),
(8,	N'Ðánh giá',	1),
(9,	N'Ðã xem',	1),
(10,	N'Import',	1),
(11,	N'Duyệt gia hạn',	1),
(12,	N'Thu hồi',	1)");
                queries.Add(@"delete from  [Task].[NatureTask];
INSERT INTO [Task].[NatureTask]
           ([Id]
      ,[Name]
      ,[IsActive])
     VALUES
           (0,	N'Thường xuyên',	1),
(1,	N'Kế hoạch',	1),
(2,	N'Đột xuất',	1)");
                queries.Add(@"delete from  [Task].[ProjectKind];
INSERT INTO [Task].[ProjectKind]
           ([Id]
      ,[Name]
      ,[IsActive])
     VALUES
           (
0,	N'Không phải dự án',	1),
(1,	N'Dự án',	1)");
                queries.Add(@"delete from  [Task].[ProjectPriority];
INSERT INTO [Task].[ProjectPriority]
           ([Id]
      ,[Name]
      ,[IsActive])
     VALUES
           (
0,	N'Bình thuờng',	1),
(1,	N'Quan trọng',	1),
(2,	N'Rất quan trọng',	1)");
                queries.Add(@"delete from  [Task].[ProjectSecret];
INSERT INTO [Task].[ProjectSecret]
           ([Id]
      ,[Name]
      ,[IsActive])
     VALUES
           (
0,	N'Bình thuờng',	1),
(1,	N'Mật',	1),
(2,	N'Tuyệt mật',	1),
(4,	N'Tối mật',	1)");
                queries.Add(@"delete from  [Task].[ProjectStatus];
INSERT INTO [Task].[ProjectStatus]
           ([Id]
           ,[Name]
           ,[Code]
           ,[IsActive])
     VALUES
           (
-1,N'Ðịnh kỳ',	'DINHKY',	1),
(0	,N'Mới',	'MOI',	1),
(1	,N'Ðang chờ duyệt',	'CHODUYET',	1),
(3	,N'Ðang xử lý',	'DANGXULY',	1),
(4	,N'Kết thúc',	'KETTHUC',	1),
(5	,N'Ðã duyệt',	'DADUYET',	1),
(12	,N'Ðang soạn',	'DANGSOAN',	1)");
                queries.Add(@"delete from  [Task].[ProjectType];
INSERT INTO [Task].[ProjectType]
           ([Id]
           ,[Name]
           ,[Code]
           ,[OrderNumber]
           ,[IsActive])
     VALUES
           (0,N'Kế hoạch công việc','PlanningApprove',	0,	1),
(1,N'Công việc giao ban','Planning',	1,	1),
(2,N'Công việc định kỳ','Scheduling',	2,	1),
(3,N'Công việc phát sinh','Unplanned',	3,	1)
GO
");
                queries.Add(@"delete from  [Task].[TaskItemPriority];
INSERT INTO [Task].[TaskItemPriority]
           ([Id]
           ,[Name]
           ,[Density]
           ,[IsActive])
     VALUES
           (0,N'Bình thuờng',	1,	1),(
1,N'Quan trọng',	1,	1),(
2,N'Rất quan trọng',	1,	1),(
3,N'Không quan trọng',	1,	1),(
4,N'Thiết yếu',	1,	1)");
                queries.Add(@"delete from  [Task].[TaskItemStatus];
INSERT INTO [Task].[TaskItemStatus]
           ([Id]
           ,[Name]
           ,[Code]
           ,[IsActive])
     VALUES
           (-1,N'Tất cả','ALL',	1),(
0,N'Mới','NEW',	1),(
1,N'Ðang xử lý','INPROCESS',	1),(
2,N'Báo cáo','REPORT',	1),(
3,N'Hủy','CANCEL',	1),(
4,N'Kết thúc','FINISH',	1),(
5,N'Gia hạn','EXTEND',	1),(
6,N'Trả lại','REPORT_RETURN',	1),(
8,N'Ðã xem','READ',	1)");
            }
			if (newVersion != currentVersion)
            {
                queries.Add(string.Format(@"if (Not Exists(select * from [dbo].[Settings] where Code = 'CurrentTaskVersion'))
  begin insert into [dbo].[Settings] values ('CurrentTaskVersion','CurrentTaskVersion','{0}')
  end
  else
  begin update [dbo].[Settings] set Value = '{0}' where Code = 'CurrentTaskVersion'
  end", newVersion));
            }

            return queries;
        }
    }
}
