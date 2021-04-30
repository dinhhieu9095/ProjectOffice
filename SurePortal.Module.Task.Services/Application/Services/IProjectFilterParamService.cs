using SurePortal.Core.Kernel.Orgs.Models;
using SurePortal.Module.Task.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurePortal.Module.Task.Services
{
    public interface IProjectFilterParamService
    {
        Task<List<JsTreeViewModel>> GetTreeFilterByParentId(Guid userID, Guid parentID = default(Guid), string keySearch = null, bool isMobile = false);

        List<ProjectFilterParam> GetRootCheckSubProjectFilterParams(Guid userID);

        ProjectFilterParamDto GetById(Guid id);

        List<ProjectFilterParamDto> GetProjectFilterParam(string parentCode, Guid userId, bool isGetLv2 = false);

        bool Save(ProjectFilterParamDto dto, Guid currenUserId);
        bool Delete(Guid Id);
    }
}