using SurePortal.Core.Kernel.AmbientScope;
using SurePortal.Core.Kernel.Orgs.Domain;
using System;
using System.Collections.Generic;

namespace SurePortal.Core.Kernel.Orgs.Infrastructure
{
    public interface IUserRepository : IRepository<User>
    {
        IList<User> GetAllUsers();

        User GetUser(Guid id);

        User GetUser(string userName);

        byte[] GetUserAvatar(string userName);

        byte[] GetUserAvatar(Guid id);

        User GetUserByCode(string code);

        User GetUserByEmail(string email);

        IList<User> GetUsers(int pageIndex, int pageSize, string filterKeyword);
    }
}