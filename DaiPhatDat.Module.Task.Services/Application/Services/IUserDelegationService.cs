
using DaiPhatDat.Core.Kernel.Orgs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DaiPhatDat.Module.Task.Services
{
    public interface IUserDelegationService
    {
        IEnumerable<UserDelegation> FindBy(Expression<Func<UserDelegation, bool>> predicate);
        IQueryable<UserDelegation> GetAllByDB(Guid deptDBID);

        List<Guid> GetListUserIdDelegateToUser(DateTime getDate, Guid toUserId);
    }
}