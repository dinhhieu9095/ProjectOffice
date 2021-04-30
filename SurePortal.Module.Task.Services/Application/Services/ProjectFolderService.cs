using AutoMapper;
using SurePortal.Core.Kernel.AmbientScope;
using SurePortal.Core.Kernel.Logger.Application;
using SurePortal.Core.Kernel.Orgs.Models;
using SurePortal.Module.Task.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SurePortal.Core.Kernel.Orgs.Application;
using System.Globalization;

namespace SurePortal.Module.Task.Services
{
    public class ProjectFolderService : IProjectFolderService
    {
        private readonly ILoggerServices _loggerServices;
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IProjectFolderRepository _objectRepository;
        private readonly IMapper _mapper;
        private readonly IUserServices _userServices;
        public ProjectFolderService(ILoggerServices loggerServices, IDbContextScopeFactory dbContextScopeFactory, IMapper mapper, IProjectFolderRepository objectRepository, IUserServices userServices)
        {
            _loggerServices = loggerServices;
            _objectRepository = objectRepository;
            _dbContextScopeFactory = dbContextScopeFactory;
            _mapper = mapper;
            _userServices = userServices;
        }

        #region Attributes
         
        #endregion

        /// <summary>
        /// Lấy danh sách folder theo user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<ProjectFolder>> GetByUser(Guid userId)
        {
            IReadOnlyList<ProjectFolder> models = null;
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                models = await _objectRepository
                .GetAsync(w => w.IsActive == true && w.UserId == userId && w.IsPersonal != null && w.IsPersonal == true);
            }
            return models.OrderBy(x => x.Name).ToList();
        }
    }
}
