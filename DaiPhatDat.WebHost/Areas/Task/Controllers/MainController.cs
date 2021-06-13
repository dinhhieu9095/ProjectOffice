using AutoMapper;
using DaiPhatDat.Core.Kernel.Controllers;
using DaiPhatDat.Core.Kernel.Logger.Application;
using DaiPhatDat.Core.Kernel.Orgs.Application;
using DaiPhatDat.Module.Task.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DaiPhatDat.Module.Task.Web
{
    [Authorize]
    public class MainController : CoreController
    {
        private IProjectFilterParamService _projectFilterParamService;
        private ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public MainController(ILoggerServices loggerServices, IUserServices userService, IUserDepartmentServices userDepartmentServices, IProjectFilterParamService projectFilterParamService, IMapper mapper, ICategoryService categoryService) : base(loggerServices, userService, userDepartmentServices)
        {
            _projectFilterParamService = projectFilterParamService;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        /// <summary>
        /// Lấy cây danh mục filter
        /// </summary>
        /// <returns></returns>
        public async Task<JsonResult> GetAdvanceFilterTree(Guid parentID = default(Guid), string keySearch = null)
        {
            try
            {
                var dataModel = await _projectFilterParamService.GetTreeFilterByParentId(CurrentUser.Id, parentID, keySearch);

                return Json(dataModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.Message);
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Render view menu trái
        /// </summary>
        /// <returns></returns>
        public JsonResult NavigationLeftFilter()
        {
            var datas = _projectFilterParamService.GetRootCheckSubProjectFilterParams(CurrentUser.Id);
            var dataModel = datas.Where(w => w.IsActive == true && (w.ParentID.HasValue == false || w.ParentID == Guid.Empty))
                .Select(s => new TreeFilterDocumentModel()
                {
                    Code = s.Code,
                    Count = 0,
                    ID = s.Id,
                    IsActive = s.IsActive == true,
                    IsCount = s.IsCount,
                    Name = s.Name,
                    NoOrder = s.NoOrder,
                    ParamNames = "",
                    ParamValues = s.ParamValue,
                    ParentID = s.ParentID,
                    TypeShow = s.TypeShow,
                    IconLink = "",
                    Permission = "",
                    Icon = s.Icon ?? "icon-notebook"
                }).ToList();
            return Json(dataModel, JsonRequestBehavior.AllowGet);
        }

    }
}