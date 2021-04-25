using AutoMapper;
using SurePortal.Core.Kernel.AmbientScope;
using SurePortal.Core.Kernel.Orgs.Application.Dto;
using SurePortal.Core.Kernel.Orgs.Domain.Repositories;
using SurePortal.Core.Kernel.Orgs.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace SurePortal.Core.Kernel.Orgs.Application
{
    /// <summary>
    /// Lớp dịch vụ cung cấp thông tin user
    /// </summary>
    public class UserServices : IUserServices
    {
        #region Static Attributes

        private static List<UserDto> _allUserDtos = null;

        #endregion Static Attributes

        #region Attributes

        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IUserRepository _userRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IMapper _mapper;

        #endregion Attributes

        #region Constructors

        public UserServices(
          IDbContextScopeFactory dbContextScopeFactory,
          IUserRepository userRepository,
          IGroupRepository groupRepository,
          IMapper mapper)
        {
            _dbContextScopeFactory = dbContextScopeFactory;
            _userRepository = userRepository;
            _groupRepository = groupRepository;
            _mapper = mapper;
        }


        #endregion Constructors

        #region Methods
        public IReadOnlyList<UserDto> GetUsers(bool clearCache = false)
        {
            if (_allUserDtos == null || clearCache)
            {
                // lock for updating caching
                //lock (_allUserDtos)
                {
                    using (_dbContextScopeFactory.CreateReadOnly())
                    {
                        var allUserModels = _userRepository.GetAllUsers();
                        _allUserDtos = _mapper.Map<List<UserDto>>(allUserModels);
                    }
                }
            }
            return _allUserDtos.AsReadOnly();
        }
        public IReadOnlyList<UserDto> GetUserByRoles(List<Guid> roleIds)
        {
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                var allUserModels = _userRepository.GetAllUsers();
                allUserModels = allUserModels.Where(e => e.UserRoles.Any(r => roleIds.Contains(r.RoleID))).ToList();
                _allUserDtos = _mapper.Map<List<UserDto>>(allUserModels);
            }
            return _allUserDtos.AsReadOnly();
        }
        public IList<UserDto> GetUsers(int pageIndex, int pageSize, string filterKeyword)
        {
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                var models = _userRepository.GetUsers(pageIndex, pageSize, filterKeyword);
                return _mapper.Map<List<UserDto>>(models);
            }
        }

        public UserDto GetUser(Guid id)
        {
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                var model = _userRepository.GetUser(id);
                if (model == null)
                {
                    return new UserDto();
                }
                return _mapper.Map<UserDto>(model);
            }
        }

        public UserDto GetUser(string userName)
        {
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                var model = _userRepository.GetUser(userName);
                if (model == null)
                {
                    return new UserDto();
                }
                return _mapper.Map<UserDto>(model);
            }
        }

        public UserDto GetUserByEmail(string email)
        {
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                var model = _userRepository.GetUserByEmail(email);
                if (model == null)
                {
                    return new UserDto();
                }
                return _mapper.Map<UserDto>(model);
            }
        }

        public UserDto GetUserByCode(string code)
        {
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                var model = _userRepository.GetUserByCode(code);
                if (model == null)
                {
                    return new UserDto();
                }
                return _mapper.Map<UserDto>(model);
            }
        }

        public byte[] GetAvatarContent(Guid id)
        {
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                return _userRepository.GetUserAvatar(id);
            }
        }

        public byte[] GetAvatarContent(string userName)
        {
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                return _userRepository.GetUserAvatar(userName);
            }
        }

        public IList<Guid> GetDirectManager(Guid userId, Guid jobTitleId)
        {
            return new List<Guid>();
            //const string query = @"
            //    WITH CTE
            //    AS
            //    (
            //        SELECT
            //            d.ID
            //            , d.ParentID
            //            , ud.UserID
            //            , jt.ID AS JobTitleID
            //            , jt.Code
            //            , 1 as Lv
            //            FROM Departments d
            //            INNER JOIN UserDepartments ud ON ud.DeptID = d.id
            //            INNER JOIN JobTitles jt ON jt.ID = ud.JobTitleID
            //            WHERE d.IsActive = 1
            //            AND ud.DeptID IN
            //            (
            //                SELECT DeptID
            //                FROM UserDepartments
            //                WHERE UserID = @UserID
            //            )
            //    UNION ALL
            //    SELECT
            //        d.ID
            //        , d.ParentID
            //        , ud.UserID
            //        , jt.ID AS JobTitleID
            //        , jt.Code
            //        , (cte.Lv + 1 ) AS Lv
            //        FROM Departments d
            //        INNER JOIN UserDepartments ud ON ud.DeptID = d.ID
            //        INNER JOIN JobTitles jt ON jt.ID = ud.JobTitleID
            //        INNER JOIN cte ON cte.ParentID = d.ID
            //    )

            //    SELECT DISTINCT UserID, LV
            //    FROM cte
            //    WHERE JobTitleID = @JobTitleID
            //    ORDER BY Lv";

            //return unitOfWork
            //    .GetContext<SurePortalContext>()
            //    .Database
            //    .SqlQuery<UserDirectManager>(query
            //        , new SqlParameter("@UserID", userId)
            //        , new SqlParameter("@JobTitleID", jobTitleId))
            //    .Where(predicate => predicate.LV == 1)
            //    .Select(selector => selector.UserID)
            //    .ToList();
        }

        public IList<Guid> GetDirectManager(Guid userId)
        {
            return new List<Guid>();
            //return GetDirectManager(userId, _jobTitleService
            //    .GetAll()
            //    .AsNoTracking()
            //    .Where(jobTitle => jobTitle.Code.ToLower() == "tp"
            //                       && jobTitle.IsActive)
            //    .Select(jobTitle => jobTitle.ID)
            //    .FirstOrDefault());
        }



        public UserDto GetByUserName(string userName)
        {
            if (!string.IsNullOrEmpty(userName))
            {
                return GetUsers().FirstOrDefault(f => f.UserName.ToLower() == userName.ToLower() || f.AccountName == userName);
            }
            else
            {
                return new UserDto();
            }
        }
        public UserDto GetById(Guid userId)
        {
            return GetUsers().FirstOrDefault(f => f.Id == userId);
        }

        public List<string> GetUserPermission(string userName)
        {
            List<string> permissions = new List<string>();
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                List<string> groupCodes = new List<string>();
                List<string> userCodes = new List<string>();
                var groups = _groupRepository.GetAll()
                    .Include(e => e.GroupRoles.Select(g=>g.Role.RolePermissions.Select(r=>r.Permission)))
                    .Where(e => e.IsActive && e.UserGroups.Any(r => r.User.UserName == userName)).ToList();
                if (groups != null && groups.Any())
                {
                    groupCodes = groups.SelectMany(e => e.GroupRoles.SelectMany(g=>g.Role.RolePermissions.SelectMany(r=>r.Permission.Name))).Select(e=>e.ToString()).ToList();
                }
                var user = _userRepository.GetAll()
                    .Include(e => e.UserRoles.Select(u=>u.Role.RolePermissions.Select(r=>r.Permission)))
                    .Where(e => e.UserName == userName && e.IsActive).FirstOrDefault();
                if(user!= null)
                {
                    userCodes = user.UserRoles.SelectMany(e => e.Role.RolePermissions.Select(p => p.Permission.Name)).Select(e => e.ToString()).ToList();
                }
                permissions.AddRange(groupCodes);
                permissions.AddRange(userCodes);
                permissions = permissions.Distinct().ToList();
            }
            return permissions;
        }
        #endregion Methods
    }
}