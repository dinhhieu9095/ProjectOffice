
using SurePortal.Module.Task.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurePortal.Module.Task.Services
{
    public interface IProjectFolderService
    {
        Task<List<ProjectFolder>> GetByUser(Guid userId);
    }
}