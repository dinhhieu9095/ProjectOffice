using AutoMapper;
using DaiPhatDat.Core.Kernel.Controllers;
using DaiPhatDat.Core.Kernel.Firebase.Models;
using DaiPhatDat.Core.Kernel.Logger.Application;
using DaiPhatDat.Core.Kernel.Orgs.Application;
using DaiPhatDat.Module.Task.Entities;
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
    [RouteArea("Task")]
    [RoutePrefix("SystemManagement")]
    public class SystemManagementController : CoreController
    {
        private IProjectFilterParamService _projectFilterParamService;
        private ISettingsService _settingsService;
        private readonly IMapper _mapper;
        public SystemManagementController(ILoggerServices loggerServices, IUserServices userService, IUserDepartmentServices userDepartmentServices, IProjectFilterParamService projectFilterParamService, IMapper mapper, ISettingsService settingsService) : base(loggerServices, userService, userDepartmentServices)
        {
            _projectFilterParamService = projectFilterParamService;
            _settingsService = settingsService;
            _mapper = mapper;
        }
         
        [Route("Index")]
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        [Route("GetByKeys")]
        public async Task<JsonResult> GetKeys()
        {
            var result = new List<Setting>();
            try
            {
                var keys = new List<string>();
                keys.Add(SettingCode.TypeBarProcesTask);
                keys.Add(SettingCode.DataSourceProcesTaskCombobox);

                var settingDtos = await _settingsService.GetByKeys(keys);

                result = _mapper.Map<List<Setting>>(settingDtos);
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.Message);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Route("GetSettingsById")]
        public async Task<JsonResult> SettingsById(string Id)
        {
            var result = new SettingsModel();
            try
            {
                var keys = new List<string>();
                keys.Add(Id);

                var data = await _settingsService.GetByKeys(keys);
                var dto = data.FirstOrDefault();

                result = _mapper.Map<SettingsModel>(dto);
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.Message);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("SaveSettings")]
        public async Task<JsonResult> SaveSettings(List<SettingsModel> models)
        {
            try
            {
                var isSuccess = await _settingsService.SaveAsync(_mapper.Map<List<SettingsDto>>(models));
                if (isSuccess)
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.Message);
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
    }
}