using SurePortal.Core.Kernel.AmbientScope;
using SurePortal.Core.Kernel.Linq;
using SurePortal.Core.Kernel.Orgs.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SurePortal.Core.Kernel.Orgs.Infrastructure
{
    public class UserRepository : Repository<OrgDbContext, User>, IUserRepository
    {
        public UserRepository(IAmbientDbContextLocator ambientDbContextLocator) :
            base(ambientDbContextLocator)
        {
        }

        public IList<User> GetAllUsers()
        {
            return DbSet.AsNoTracking().OrderBy(o => o.UserName).ToList();
        }

        public IList<User> GetUsers(int pageIndex, int pageSize,
           string filterKeyword)
        {
            IQueryable<User> queryable = DbSet.AsNoTracking()
                .OrderBy(o => o.UserName);
            if (!string.IsNullOrEmpty(filterKeyword))
            {
                queryable = queryable.Where(w => w.FullName.Contains(filterKeyword) ||
                w.UserName.Contains(filterKeyword));
            }
            return queryable.Skip((pageIndex - 1) * pageSize).Take(pageIndex * pageSize).ToList();
        }

        public User GetUser(Guid id)
        {
            return DbSet.FirstOrDefault(f => f.Id == id);
        }

        public User GetUser(string userName)
        {
            if (!string.IsNullOrEmpty(userName))
            {
                return DbSet.FirstOrDefault(f => f.UserName.ToLower() == userName.ToLower());
            }
            else
            {
                return null;
            }
        }

        public User GetUserByEmail(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                return DbSet.FirstOrDefault(f => f.Email != null &&
                f.Email.ToLower() == email.ToLower());
            }
            else
            {
                return null;
            }
        }

        public User GetUserByCode(string code)
        {
            if (!string.IsNullOrEmpty(code))
            {
                return DbSet.FirstOrDefault(f => f.UserCode != null &&
                f.UserCode.ToLower() == code.ToLower());
            }
            else
            {
                return null;
            }
        }

        public byte[] GetUserAvatar(Guid id)
        {
            var model = GetUser(id);
            return model != null ? model.Avartar : null;
        }

        public byte[] GetUserAvatar(string userName)
        {
            var model = GetUser(userName);
            return model != null ? model.Avartar : null;
        }
    }
}