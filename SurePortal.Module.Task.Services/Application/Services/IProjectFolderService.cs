
using DaiPhatDat.Module.Task.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DaiPhatDat.Module.Task.Services
{
    public interface IProjectFolderService
    {
        Task<List<ProjectFolder>> GetByUser(Guid userId);
    }
}