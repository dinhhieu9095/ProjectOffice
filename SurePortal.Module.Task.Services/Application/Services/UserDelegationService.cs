using AutoMapper;
using SurePortal.Core.Kernel.AmbientScope;
using SurePortal.Core.Kernel.Logger.Application;
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
using System.Linq.Expressions;
using SurePortal.Core.Kernel.Orgs.Domain;
using SurePortal.Core.Kernel.Orgs.Domain.Entities;

namespace SurePortal.Module.Task.Services
{
    public class UserDelegationService : IUserDelegationService
    {
        private readonly ILoggerServices _loggerServices;
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IUserDelegationRepository _objectRepository;
        private readonly IMapper _mapper;
        private readonly IUserServices _userServices;

        public UserDelegationService(ILoggerServices loggerServices, IDbContextScopeFactory dbContextScopeFactory, IMapper mapper, IUserDelegationRepository objectRepository, IUserServices userServices)
        {
            _loggerServices = loggerServices;
            _dbContextScopeFactory = dbContextScopeFactory;
            _objectRepository = objectRepository;
            _mapper = mapper;
            _userServices = userServices;

        }

        public void Add(UserDelegation item)
        {
            if (item != null)
            {
                _objectRepository.Add(item);
            }
        }

        public int Count()
        {
            return _objectRepository.Count();
        }

        public void Delete(object id)
        {
            if (id != null)
            {
                var item = GetByID(id);
                if (item != null)
                {
                    _objectRepository.Delete(item);
                }
            }
        }

        public void Delete(UserDelegation item)
        {
            if (item != null)
            {
                _objectRepository.Delete(item);
            }
        }

        public IQueryable<UserDelegation> GetAll()
        {
            return _objectRepository.GetAll().AsNoTracking();
        }

        public UserDelegation GetByID(object id)
        {
            var guid = new Guid(id.ToString());
            return _objectRepository.Find(x => x.ID == guid);
        }

        public void Update(UserDelegation item)
        {
            if (item != null)
            {
                _objectRepository.Modify(item);
            }
        }

        public IEnumerable<UserDelegation> FindBy(Expression<Func<UserDelegation, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }

        public IQueryable<UserDelegation> GetAllByDB(Guid deptDBID)
        {
            using (var scope = _dbContextScopeFactory.CreateReadOnly())
            {
                var dbcontext = scope.DbContexts.Get<DbContext>();
                var deptRepository = new RepositoryBase<Department>(dbcontext);
                var currentDBID = deptRepository.GetByID(deptDBID).RootDBID;

                var _userDeptRepository = new RepositoryBase<UserDepartment>(dbcontext);

                return (from ud in _userDeptRepository.GetQueryable()
                        join udl in _objectRepository.GetAll() on ud.UserID equals udl.FromUserID
                        where ud.Department.RootDBID == currentDBID.Value && ud.Department.IsActive == true && udl.IsActive == true
                        select udl).Distinct();
            } 
        }

        public List<Guid> GetListUserIdDelegateToUser(DateTime getDate, Guid toUserId)
        {
            return _objectRepository.GetAll()
                 .Where(
                    t => t.ToUserID == toUserId && t.IsActive == true && t.FromUserID != null && (getDate >= t.FromDate && getDate <= t.ToDate))
                .Select(t => t.FromUserID.Value).ToList();
        }

    }
}