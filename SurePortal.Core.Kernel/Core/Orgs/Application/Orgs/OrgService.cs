using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.Ajax.Utilities;
using OfficeOpenXml;
using SurePortal.Core.Kernel.AmbientScope;
using SurePortal.Core.Kernel.Models;
using SurePortal.Core.Kernel.Notifications.Domain.Entities;
using SurePortal.Core.Kernel.Notifications.Domain.Repositories;
using SurePortal.Core.Kernel.Orgs.Application.Contract;
using SurePortal.Core.Kernel.Orgs.Application.Dto;
using SurePortal.Core.Kernel.Orgs.Domain;
using SurePortal.Core.Kernel.Orgs.Domain.Repositories;
using SurePortal.Core.Kernel.Orgs.Infrastructure;
using SurePortal.Core.Kernel.Orgs.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SurePortal.Core.Kernel.Orgs.Application
{
    public class OrgService : IOrgService
    {
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IDepartmentTypeRepository _departmentTypeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserDepartmentRepository _userDepartmentRepository;
        private readonly ISignatureRepository _signatureRepository;
        private readonly INotificationSettingRepository _notificationSettingRepository;
        private readonly IUserJobTitleRepository _userJobTitleRepository;

        private readonly IMapper _mapper;

        private readonly IDepartmentServices _departmentServices;
        private readonly IUserDepartmentServices _userDepartmentServices;
        private readonly IUserServices _userServices;
        private static IReadOnlyList<UserInfo> _userInfos = null;

        public OrgService(
            IDbContextScopeFactory dbContextScopeFactory,
            IDepartmentTypeRepository departmentTypeRepository,
            IUserRepository userRepository,
            IUserDepartmentRepository userDepartmentRepository,
            ISignatureRepository signatureRepository,
            INotificationSettingRepository notificationSettingRepository,
            IUserJobTitleRepository userJobTitleRepository,
            IDepartmentServices departmentServices,
            IUserDepartmentServices userDepartmentServices,
            IMapper mapper,
            IUserServices userServices)
        {
            _dbContextScopeFactory = dbContextScopeFactory;
            _departmentTypeRepository = departmentTypeRepository;
            _userDepartmentRepository = userDepartmentRepository;
            _userRepository = userRepository;
            _signatureRepository = signatureRepository;
            _notificationSettingRepository = notificationSettingRepository;
            _userJobTitleRepository = userJobTitleRepository;

            _mapper = mapper;

            _departmentServices = departmentServices;
            _userDepartmentServices = userDepartmentServices;
            _userServices = userServices;
        }
        public async Task<IReadOnlyList<DepartmentInfo>> GetActiveDepartmentsAsync()
        {
            var departmentDtos = await _departmentServices.GetDepartmentsAsync();
            if (departmentDtos == null)
            {
                return null;
            }
            departmentDtos = departmentDtos.Where(department => department.IsActive == true).ToList();
            return _mapper.Map<List<DepartmentInfo>>(departmentDtos);
        }
        public async Task<DepartmentInfo> GetDepartmentInfoAsync(Guid departmentId)
        {
            var departmentDto = await _departmentServices.GetAsync(departmentId);
            if (departmentDto == null)
            {
                return new DepartmentInfo();
            }
            return _mapper.Map<DepartmentInfo>(departmentDto);
        }
        public async Task<UserInfo> GetUserInfoAsync(Guid userId, bool includeDepartment = false)
        {
            var userDto = await GetDefaultUserDepartmentAsync(userId);
            if (userDto == null)
            {
                return null;
            }

            var userInfo = _mapper.Map<UserInfo>(userDto);
            if (includeDepartment)
            {
                var userDepartments = await _userDepartmentServices.GetCachedUserDepartmentDtos();
                userInfo.Departments = userDepartments
                    .OrderBy(ud => ud.DeptOrderNumber)
                    .Where(u => u.UserID == userId)
                    .Select(ud => new DepartmentInfo()
                    {
                        Id = ud.DeptID,
                        Code = ud.DeptCode,
                        Name = ud.DeptName,
                        Order = ud.DeptOrderNumber.HasValue ? ud.DeptOrderNumber.Value : 100,
                    }).ToList();
            }
            return userInfo;
        }

        public async Task<IReadOnlyList<UserInfo>> GetAllUserInfoAsync()
        {
            if (_userInfos == null)
            {
                var userDepartments = await _userDepartmentServices.GetCachedUserDepartmentDtos();
                userDepartments = userDepartments
                    .OrderBy(ud => ud.DeptOrderNumber)
                    .ThenBy(ud => ud.JobTitleOrderNumber)
                    .DistinctBy(ud => ud.UserID)
                    .ToList();
                _userInfos = _mapper.Map<List<UserInfo>>(userDepartments);
            }
            return _userInfos;

        }
        public async Task<Pagination<UserInfo>> SearchUsersAsync(UserFilterInput filter)
        {
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                var users = await GetAllUserInfoAsync();
                int count = users.Count;
                if (!string.IsNullOrEmpty(filter.Key))
                {
                    count = _userInfos.Count(user => user.SearchText.Contains(filter.Key));
                }
                if (count == 0)
                {
                    return new Pagination<UserInfo>(0, new List<UserInfo>());
                }
                int skip = (filter.Page - 1) * filter.PageSize;
                int take = filter.PageSize;
                var searchUsers = _userInfos
                    .Where(user => user.SearchText.Contains(filter.Key))
                    .Skip(skip)
                    .Take(take)
                    .ToList();
                return new Pagination<UserInfo>(count, searchUsers, skip, take);
            }
        }
        public async Task<IReadOnlyList<Guid>> GetRecuresiveDeptIDAsync(Guid departmentId)
        {
            var departmentDto = await _departmentServices.GetAsync(departmentId);
            if (departmentDto == null)
            {
                return new List<Guid>();

            }
            var parentDepartmentIds = new List<Guid>();
            Guid? parentId = departmentDto.ParentID;
            do
            {
                if (!parentId.HasValue)
                {
                    break;
                }
                var parentDepartment = await _departmentServices.GetAsync(parentId.Value);
                if (parentDepartment == null)
                {
                    break;
                }
                parentDepartmentIds.Add(parentDepartment.Id);
                parentId = parentDepartment.ParentID;
            }
            while (parentId.HasValue);
            return parentDepartmentIds;
        }

        private async Task<UserDepartmentDto> GetDefaultUserDepartmentAsync(Guid userId)
        {
            var userDepartmentDtos = await _userDepartmentServices.GetCachedUserDepartmentDtos();
            var userDepartmentDto = userDepartmentDtos
                .OrderBy(ud => ud.DeptIndex)
                .ThenBy(ud => ud.DeptOrderNumber)
                .FirstOrDefault(ud => ud.UserID == userId);
            if (userDepartmentDto == null)
            {
                var userDto = _userServices.GetById(userId);
                if (userDto != null)
                {
                    userDepartmentDto = new UserDepartmentDto()
                    {
                        UserID = userDto.Id,
                        UserName = userDto.UserName,
                        FullName = userDto.FullName,

                    };
                }
            }
            return userDepartmentDto;
        }

        public async Task<IList<JsTreeViewModel>> GetOrgTreeAsync()
        {
            var departments = await _departmentServices.GetHierarchyDepartmentsAsync();
            return ConvertToJsTreeViewModel(departments);
        }
        public async Task<IList<JsTreeViewModel>> GetOrgTreeNoCachedAsync()
        {
            var departments = await _departmentServices.GetLiveHierarchyDepartmentsAsync();
            var tree = ConvertToJsTreeViewModel(departments);
            return tree;
        }
        private IList<JsTreeViewModel> ConvertToJsTreeViewModel(IList<DepartmentDto> departments)
        {
            try
            {
                List<JsTreeViewModel> lstTree = new List<JsTreeViewModel>();
                if (departments != null)
                {
                    for (int i = 0; i < departments.Count; i++)
                    {
                        var department = departments[i];
                        JsTreeViewModel jsTree = new JsTreeViewModel();
                        jsTree.text = departments[i].Name;
                        jsTree.id = departments[i].Id.ToString();
                        jsTree.li_attr = new
                        {
                            type = "department",
                            text = departments[i].Name,
                            code = departments[i].Code,
                            id = departments[i].Id
                        };
                        if (department.Children != null && department.Children.Count > 0)
                        {
                            jsTree.children = ConvertToJsTreeViewModel(department.Children);
                        }
                        else
                        {
                            jsTree.children = false;
                        }
                        lstTree.Add(jsTree);
                    }
                }
                return lstTree;
            }
            catch (Exception)
            {

                return new List<JsTreeViewModel>();
            }
        }
        public async Task<Guid> CreateUserAsync(CreateUserDto createUserDto)
        {
            using (var scope = _dbContextScopeFactory.Create())
            {
                // create user
                var createUser = _userRepository.GetUserByEmail(createUserDto.Email.ToLower());
                if (createUser == null)
                {
                    createUser = User.CreateUser(createUserDto.UserName, createUserDto.FullName,
                        createUserDto.Gender, createUserDto.Email, createUserDto.Mobile,
                        createUserDto.Address, createUserDto.HomePhone, createUserDto.Ext,
                        createUserDto.BirthDate, createUserDto.UserCode, null,
                        createUserDto.Language, createUserDto.UserIndex);
                    _userRepository.Add(createUser);
                }
                else
                {
                    createUser.Update(createUserDto.FullName,
                        createUserDto.Gender, createUserDto.Email, createUserDto.Mobile,
                        createUserDto.Address, createUserDto.HomePhone, createUserDto.Ext,
                        createUserDto.BirthDate, createUserDto.UserCode, null,
                        createUserDto.Language, createUserDto.UserIndex);
                }

                // create user depart
                foreach (var deptId in createUserDto.Departments)
                {
                    var createUserDept = _userDepartmentRepository
                        .Get(w => w.UserID == createUser.Id && w.DeptID == deptId)
                        .FirstOrDefault();
                    if (createUserDept == null)
                    {
                        createUserDept = UserDepartment.CreateUserDepartment(
                        createUser.Id, deptId, createUserDto.JobDescription,
                        createUserDto.OrderNumber, createUserDto.JobTitleId,
                        createUserDto.IsManager);
                        _userDepartmentRepository.Add(createUserDept);
                    }
                    else
                    {
                        createUserDept.Update(createUserDto.JobDescription,
                       createUserDto.OrderNumber, createUserDto.JobTitleId,
                       createUserDto.IsManager);
                    }
                }

                // create signature
                if (createUserDto.UserSignatures != null && createUserDto.UserSignatures.Count > 0)
                {
                    foreach (var signature in createUserDto.UserSignatures)
                    {
                        var createSignature = UserSignature.CreateUserSignature(
                            signature.SignServerId, createUser.Id,
                            signature.Title, signature.CertificateBin,
                            signature.SignImage, signature.SignImageType,
                            signature.StampImage, signature.StampImageType,
                            signature.Account, signature.Password);
                        _signatureRepository.Add(createSignature);
                    }
                }

                // create notification setting
                if (createUserDto.NotificationSettings != null && createUserDto.NotificationSettings.Count > 0)
                {
                    foreach (var notifySetting in createUserDto.NotificationSettings)
                    {
                        var createMotifySetting = NotificationSetting.Create(createUser.Id,
                            notifySetting.NotificationTypeId, notifySetting.IsUrgent);
                        _notificationSettingRepository.Add(createMotifySetting);
                    }
                }

                await scope.SaveChangesAsync();
                return createUser.Id;
            }
        }

        public async Task<bool> UpdateUserAsync(UpdateUserDto updateUserDto)
        {
            using (var scope = _dbContextScopeFactory.Create())
            {
                // update user
                var createUser = await _userRepository.FindAsync(
                    f => f.Id == updateUserDto.UserId);
                if (createUser == null)
                {
                    throw new Exception("User not found !");
                }
                else
                {
                    createUser.Update(updateUserDto.FullName,
                        updateUserDto.Gender, updateUserDto.Email, updateUserDto.Mobile,
                        updateUserDto.Address, updateUserDto.HomePhone, updateUserDto.Ext,
                        updateUserDto.BirthDate, updateUserDto.UserCode, null,
                        updateUserDto.Language, updateUserDto.UserIndex);
                }

                // remove
                if (updateUserDto.RemovedDepartments != null)
                {
                    foreach (var removeDeptId in updateUserDto.RemovedDepartments)
                    {
                        var delDept = _userDepartmentRepository.Get
                            (w => w.UserID == updateUserDto.UserId && w.DeptID == removeDeptId)
                            .FirstOrDefault();
                        _userDepartmentRepository.Delete(delDept);
                    }
                }

                if (updateUserDto.AddedDepartments != null)
                {
                    // create user depart
                    foreach (var deptId in updateUserDto.AddedDepartments)
                    {
                        var createUserDept = _userDepartmentRepository
                         .Get(w => w.UserID == createUser.Id && w.DeptID == deptId)
                         .FirstOrDefault();
                        if (createUserDept == null)
                        {
                            createUserDept = UserDepartment.CreateUserDepartment(
                            createUser.Id, deptId, updateUserDto.JobDescription,
                            updateUserDto.OrderNumber, updateUserDto.JobTitleId,
                            updateUserDto.IsManager);
                            _userDepartmentRepository.Add(createUserDept);
                        }
                        else
                        {
                            createUserDept.Update(updateUserDto.JobDescription,
                           updateUserDto.OrderNumber, updateUserDto.JobTitleId,
                           updateUserDto.IsManager);
                        }
                    }
                }

                if (updateUserDto.RemovedUserSignatures != null && updateUserDto.RemovedUserSignatures.Count > 0)
                {
                    foreach (var removeSignatureId in updateUserDto.RemovedUserSignatures)
                    {
                        var removeSign = await _signatureRepository.FindAsync(f => f.Id == removeSignatureId);
                        _signatureRepository.Delete(removeSign);
                    }
                }

                // create signature 
                if (updateUserDto.AddedUserSignatures != null && updateUserDto.AddedUserSignatures.Count > 0)
                {
                    foreach (var signature in updateUserDto.AddedUserSignatures)
                    {
                        var createSignature = UserSignature.CreateUserSignature(
                            signature.SignServerId, createUser.Id,
                            signature.Title, signature.CertificateBin,
                            signature.SignImage, signature.SignImageType,
                            signature.StampImage, signature.StampImageType,
                            signature.Account, signature.Password);
                        _signatureRepository.Add(createSignature);
                    }
                }

                // create notification setting
                //if (updateUserDto.NotificationSettings != null && updateUserDto.NotificationSettings.Count > 0)
                //{
                //    foreach (var notifySetting in updateUserDto.NotificationSettings)
                //    {
                //        var createMotifySetting = NotificationSetting.Create(createUser.Id,
                //            notifySetting.NotificationTypeId, notifySetting.IsUrgent);
                //        _notificationSettingRepository.Add(createMotifySetting);
                //    }
                //}

                await scope.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> DeleteUserDepartmentAsync(Guid userId, Guid deptId)
        {
            using (var scope = _dbContextScopeFactory.Create())
            {
                var deleteItem = await _userDepartmentRepository
                    .FindAsync(f => f.DeptID == deptId && f.UserID == userId);
                _userDepartmentRepository.Delete(deleteItem);
                await scope.SaveChangesAsync();
                return true;
            }
        }

        public async Task<IReadOnlyList<UserJobTitleDto>> GetJobtitlesAsync()
        {
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                var jobTitles = await _userJobTitleRepository.GetListUserJobTitle();
                return _mapper.Map<List<UserJobTitleDto>>(jobTitles);
            }
        }

        public async Task<IReadOnlyList<DepartmentTypeDto>> GetDepartmentTypesAsync()
        {

            using (_dbContextScopeFactory.CreateReadOnly())
            {
                var items = await _departmentTypeRepository
                .GetAll()
                .Where(w => w.IsActive)
                .OrderBy(o => o.Name)
                .ProjectTo<DepartmentTypeDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
                return items;
            }


        }

        public async Task<bool> ImportOrgAsync(string importDataFilePath)
        {
            if (!File.Exists(importDataFilePath))
            {
                return false;
            }

            byte[] bin = File.ReadAllBytes(importDataFilePath);

            //create a new Excel package in a memorystream
            using (MemoryStream stream = new MemoryStream(bin))
            using (ExcelPackage excelPackage = new ExcelPackage(stream))
            {
                //loop all worksheets
                var worksheet = excelPackage.Workbook.Worksheets.FirstOrDefault();
                if (worksheet != null)
                {

                    // get number of rows and columns in the sheet
                    int rows = worksheet.Dimension.Rows; // 20
                    int columns = worksheet.Dimension.Columns; // 7

                    // loop through the worksheet rows and columns
                    for (int ridx = 1; ridx <= rows; ridx++)
                    {
                        for (int cidx = 1; cidx <= columns; cidx++)
                        {
                            string content = worksheet.Cells[ridx, cidx].Value.ToString();

                        }
                    }
                }
                return true;
            }
        }

        public async Task<bool> UpdateAdInfo(string userName, string employeeID, string mail, string ext, string mobile,
            byte[] avatar)
        {
            using (var scope = _dbContextScopeFactory.Create())
            {
                var user = await _userRepository.FindAsync(f => f.UserName == userName);
                if (user != null)
                {
                    user.UpdateAdInfo(mail, mobile, ext, employeeID, avatar);
                    await scope.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<IList<JsTreeViewModel>> GetOrgUserTree(bool isGetUser = true)
        {
            IList<JsTreeViewModel> trees = new List<JsTreeViewModel>();
            try
            {
                IReadOnlyList<DepartmentDto> departmentDtos = await _departmentServices.GetDepartmentsAsync();

                IReadOnlyList<UserDepartmentDto> userDepartmentDtos = null;
                if (isGetUser)
                    userDepartmentDtos = await _userDepartmentServices.GetCachedUserDepartmentDtos();
                trees = await GetHierarchyDepartmentsAsync(departmentDtos, userDepartmentDtos, 0);
            }
            catch (Exception ex)
            {

                throw;
            }

            return trees;
        }
        public async Task<List<JsTreeViewModel>> GetHierarchyDepartmentsAsync(IReadOnlyList<DepartmentDto> departmentDtos, IReadOnlyList<UserDepartmentDto> userDepartmentDtos, int treeLevel, Guid parentId = default(Guid))
        {
            List<JsTreeViewModel> trees = new List<JsTreeViewModel>();
            List<DepartmentDto> deparments = new List<DepartmentDto>();
            if (parentId == Guid.Empty)
            {
                deparments = departmentDtos
                    .Where(w => w.IsActive == true && w.ParentID == null || w.ParentID == Guid.Empty)
                    .OrderBy(o => o.OrderNumber).ToList();
            }
            else
            {
                deparments = departmentDtos
                   .Where(w => w.IsActive == true && w.ParentID == parentId)
                   .OrderBy(o => o.OrderNumber).ToList();
            }
            foreach (var dept in deparments)
            {
                dept.TreeLevel = treeLevel;
                bool isOpened = dept.TreeLevel < 2;
                JsTreeViewModel node = new JsTreeViewModel()
                {
                    icon = "jstree-icon jstree-themeicon fa fa-folder text-warning jstree-themeicon-custom",
                    id = dept.Id.ToString(),
                    state = new JsTreeStateObject { closed = true, opened = isOpened },
                    text = dept.Name,
                    parent = dept.ParentID.HasValue && dept.ParentID != Guid.Empty ? dept.ParentID.ToString() : "#",
                    li_attr = new
                    {
                        type = "department",
                        text = dept.Name,
                        code = dept.Code,
                        id = dept.Id
                    }
                };

                List<JsTreeViewModel> children = new List<JsTreeViewModel>();
                children.AddRange(await GetHierarchyDepartmentsAsync(departmentDtos, userDepartmentDtos, treeLevel + 1, dept.Id));
                if (userDepartmentDtos != null)
                {
                    var userDepts = userDepartmentDtos.Where(e => e.DeptID == dept.Id).OrderBy(e => e.OrderNumber).ThenBy(e => e.FullName).ToList();
                    foreach (var userDept in userDepts)
                    {
                        JsTreeViewModel nodeUser = new JsTreeViewModel()
                        {
                            id = userDept.Id.ToString(),
                            icon = "jstree-icon jstree-themeicon jstree-themeicon-custom",
                            children = false,
                            state = new JsTreeStateObject { closed = true },
                            text = userDept.FullName + " ("+ userDept.JobTitleName +")",
                            parent = dept.Id.ToString(),
                            li_attr = new
                            {
                                text = userDept.FullName,
                                code = userDept.UserName,
                                jobTitle = userDept.JobTitleName,
                                department = userDept.DeptName,
                                type = "user",
                                id = userDept.UserID
                            }
                        };
                        children.Add(nodeUser);
                    }
                }
                if (children != null && children.Any())
                {
                    trees.AddRange(children);
                }
                else
                {
                    node.children = false;
                }
                trees.Add(node);
            }

            return trees;
        }

    }
}