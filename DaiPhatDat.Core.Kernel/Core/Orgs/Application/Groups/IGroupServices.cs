using DaiPhatDat.Core.Kernel.Orgs.Application.Dto;
using System;
using System.Collections.Generic;

namespace DaiPhatDat.Core.Kernel.Orgs.Application
{
    public interface IGroupServices
    {
        IReadOnlyList<GroupDto> GetGroups(int pageIndex, int pageSize, string filterKeyword);
        GroupDto GetById(Guid id);
    }
}