using AutoMapper;
using SurePortal.Core.Kernel.AmbientScope;
using SurePortal.Core.Kernel.Orgs.Application.Contract;
using SurePortal.Core.Kernel.Orgs.Domain.Repositories;
using SurePortal.Core.Kernel.Orgs.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurePortal.Core.Kernel.Orgs.Application
{
    /// <summary>
    /// Lớp dịch vụ cung cấp thông tin user trong department
    /// </summary>
    public class UserDepartmentServices : IUserDepartmentServices
    {
        #region Static Attributes

        private static IReadOnlyList<UserDepartmentDto> _allUserDepartmentDtos = null;

        #endregion Static Attributes

        #region Attributes

        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IUserDepartmentRepository _userDepartmentRepository;
        private readonly IMapper _mapper;
        private readonly IUserServices _userServices;
        private readonly IDepartmentServices _departmentServices;
        private readonly IUserJobTitleRepository _userJobTitleRepository;

        #endregion Attributes

        #region Contructors
        public UserDepartmentServices(IDbContextScopeFactory dbContextScopeFactory,
          IUserDepartmentRepository userDepartmentRepository,
          IUserJobTitleRepository userJobTitleRepository,
          IUserServices userServices, IDepartmentServices departmentServices,
          IMapper mapper)
        {
            _dbContextScopeFactory = dbContextScopeFactory;
            _userDepartmentRepository = userDepartmentRepository;
            _userJobTitleRepository = userJobTitleRepository;
            _userServices = userServices;
            _departmentServices = departmentServices;
            _mapper = mapper;
        }
        #endregion

        #region Methods

        public async Task<IList<ReadUserDepartmentDto>> GetUserDepartmentDtos(Guid deptId, int pageIndex,
            int pageSize, string filterKeyword)
        {
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                var results = await _userDepartmentRepository.GetUserDepartments(deptId, pageIndex, pageSize, filterKeyword);
                //var allJobTitle = await _userJobTitleRepository.GetListUserJobTitle();
                //var allDeptDtos = await _departmentServices.GetAllCachedDepartments();
                //var allUserDtos = _userServices.GetAllCachedUsers();
                var models = _mapper.Map<List<ReadUserDepartmentDto>>(results);
                //ConvertToUserDepartmentDto(models, allJobTitle, allUserDtos, allDeptDtos, (pageIndex - 1) * pageSize);
                var startIndex = (pageIndex - 1) * pageSize;
                foreach (var userDept in models)
                {
                    startIndex++;
                    userDept.RecordID = startIndex;
                }
                return models;
            }
        }

        public UserDepartmentDto GetDepartmentDto(Guid userId, Guid deptId)
        {
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                var model = _userDepartmentRepository.GetUserDepartment(userId, deptId);
                return _mapper.Map<UserDepartmentDto>(model);
            }
        }

        public IList<UserDepartmentDto> GetCachedUserDepartmentsByUser(Guid userId)
        {
            if (_allUserDepartmentDtos != null)
            {
                return _allUserDepartmentDtos.Where(w => w.UserID == userId)
                    .OrderBy(o => o.DeptOrderNumber).ToList();
            }
            return null;
        }

        public IList<UserDepartmentDto> GetUserDepartmentsByUser(Guid userId)
        {
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                var models = _userDepartmentRepository.GetUserDepartmentsByUser(userId);
                return _mapper.Map<List<UserDepartmentDto>>(models);
            }
        }

        public IList<UserDepartmentDto> GetUserDepartmentsByDept(Guid deptId)
        {
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                var models = _userDepartmentRepository.GetUserDepartmentsByUser(deptId);
                return _mapper.Map<List<UserDepartmentDto>>(models);
            }
        }

        public async Task<IReadOnlyList<UserDepartmentDto>> GetCachedUserDepartmentDtos(bool clearCache = false)
        {
            if (_allUserDepartmentDtos == null || clearCache)
            {
                using (_dbContextScopeFactory.CreateReadOnly())
                {
                    var allUserDeptModels = await _userDepartmentRepository.GetAllUserDepartments();
                    var allItem = _mapper.Map<List<UserDepartmentDto>>(allUserDeptModels);
                    var allJobTitle = await _userJobTitleRepository.GetListUserJobTitle();
                    var allUserDtos = _userServices.GetUsers();
                    var allDeptDtos = await _departmentServices.GetDepartmentsAsync();
                    ConvertToUserDepartmentDto(allItem, allJobTitle, allUserDtos, allDeptDtos);
                    _allUserDepartmentDtos = allItem;
                }
            }
            return _allUserDepartmentDtos;
        }

        private static void ConvertToUserDepartmentDto(
            List<UserDepartmentDto> allItem,
            List<Domain.Entities.UserJobTitle> allJobTitle,
            IEnumerable<Dto.UserDto> allUserDtos,
            IEnumerable<Dto.DepartmentDto> allDeptDtos,
            int startIndex = 0)
        {
            foreach (var userDept in allItem)
            {
                startIndex++;
                userDept.RecordID = startIndex;
                var deptDto = allDeptDtos.FirstOrDefault(f => f.Id == userDept.DeptID);
                if (deptDto != null)
                {
                    userDept.DeptIndex = deptDto.DeptIndex;
                    userDept.DeptName = deptDto.Name;
                    userDept.DeptCode = deptDto.Code;
                    userDept.DeptOrderNumber = deptDto.OrderNumber;
                    userDept.RootDBID = deptDto.RootDBID;
                }

                var userDto = allUserDtos.FirstOrDefault(f => f.Id == userDept.UserID);
                if (userDto != null)
                {
                    userDept.UserIndex = userDto.UserIndex;
                    userDept.UserName = userDto.UserName;
                    userDept.FullName = userDto.FullName;
                    userDept.HomePhone = userDto.HomePhone;
                    userDept.Email = userDto.Email;
                    userDept.Phone = userDto.Mobile;
                    userDept.AccountName = userDto.AccountName;
                }
                if (userDept.JobTitleID != null && userDept.JobTitleID != Guid.Empty)
                {
                    var jobTitle = allJobTitle.FirstOrDefault(f => f.Id == userDept.JobTitleID);
                    if (jobTitle != null)
                    {
                        userDept.JobTitleCode = jobTitle.Code;
                        userDept.JobTitleName = jobTitle.Name;
                        userDept.JobTitleOrderNumber = jobTitle.OrderNumber;
                    }
                }
            }
        }


        #endregion
    }
}