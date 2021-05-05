using DaiPhatDat.Core.Kernel.Orgs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DaiPhatDat.Core.Kernel.Orgs.Domain.Repositories
{
    public interface IUserJobTitleRepository
    {
        Task<List<UserJobTitle>> GetListUserJobTitle();
        Task<UserJobTitle> GetUserJobTitle(Guid id);
    }
}
