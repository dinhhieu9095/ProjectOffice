using AutoMapper;
using DaiPhatDat.WebHost.Common;
using DaiPhatDat.WebHost.Models.Navigation;
using DaiPhatDat.WebHost.Modules.Navigation.Application.Dto;
using DaiPhatDat.WebHost.Modules.Navigation.Application.Services;
using DaiPhatDat.WebHost.Modules.Navigation.Domain.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DaiPhatDat.WebHost.Controllers
{
    public class NavNodeController : BaseMenuController
    {
        private readonly INavNodeService _navNodeService;

        public NavNodeController(IMenuService menuService, IMapper mapper,
            INavNodeService navNodeService) : base(menuService, mapper)
        {
            _navNodeService = navNodeService;
        }

        #region Index
        // GET: NavNode
        public async Task<ActionResult> Index()
        {
            await RefreshMenu(false);
            var model = new NavNodeIndexModel();
            return View(model);
        }
        [HttpPost]
        [ValidateInput(true)]
        public async Task<ActionResult> GetPaging(int pageIndex, string keyWord)
        {
            int _pageSize = 10;
            var model = new NavNodePagingModel() { PageIndex = pageIndex,PageSize= _pageSize };
            try
            {
                string curentUser = User.Identity.Name;
                //if (IS_SUPER_ADMIN()) { curentUser = string.Empty; }
                var iqueryData = await _navNodeService.GetPagingAsync(new SearchMenuDto
                {
                    UserName = curentUser,
                    KeyWord = keyWord,
                    PageIndex = pageIndex,
                    PageSize = _pageSize
                });
                model.TotalPage = iqueryData.Item1;
                model.LstData = _mapper.Map<List<NavNodeModel>>(iqueryData.Item2);
            }
            catch (Exception ex) { /*this.LogError(this, ex); */}
            return PartialView(model);
        }
        private async Task RefreshMenu(bool isUpdate)
        {
            try
            {
                if (isUpdate)
                {
                    var lstData = await _navNodeService.GetList();
                    Assembly asm = Assembly.GetExecutingAssembly();
                    var LstControllerActiosn = asm.GetTypes()
                       .Where(type => typeof(Controller).IsAssignableFrom(type))
                       .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                       .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any())
                       .ToList();
                    var LstControllerAction = asm.GetTypes()
                        .Where(type => typeof(Controller).IsAssignableFrom(type))
                        .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                        .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any())
                        .Select(x => new
                        {
                            Controller = x.DeclaringType.Name.Replace("Controller", ""),
                            Action = x.Name,
                            ReturnType = x.ReturnType.Name,
                            Attributes = string.Join(",", x.GetCustomAttributes().Select(a => a.GetType().Name.Replace("Attribute", "")))
                        })
                        .OrderBy(x => x.Controller).ThenBy(x => x.Action)
                        .GroupBy(x => new { x.Controller, x.Action })
                        .Select(g => new NavNodeDto
                        {
                            Id = Guid.NewGuid(),
                            Controller = g.Key.Controller?.ToLower(),
                            Action = g.Key.Action?.ToLower(),
                            Description = g.Select(y => y.ReturnType).FirstOrDefault() + " " + g.Select(y => y.Attributes).FirstOrDefault(),
                            ActiveFag = Core.Kernel.CommonUtility.ActiveFag.Active,
                            Status = g.Select(y => y.ReturnType).FirstOrDefault() != "ActionResult" ? CommonValues.NavNode.NavNodeStatus.Private : CommonValues.NavNode.NavNodeStatus.Ignore,
                            CreatedBy = User.Identity.Name,
                            CreatedDate = DateTime.Now,
                            ModifiedDate = DateTime.Now,
                            Method = g.Select(y => y.Attributes).FirstOrDefault()
                        }).Where(x => lstData.Where(p => p.Controller.ToLower() == x.Controller.ToLower() && p.Action.ToLower() == x.Action.ToLower()).Count() == 0).ToList();
                    if (LstControllerAction.Count >= 1)
                    {
                        await _navNodeService.AddRangeAsync(LstControllerAction);
                    }
                }
            }
            catch (Exception ex) { }
        }
        #endregion

        #region Insert
        public ActionResult Insert(bool isNav = true)
        {
            var model = new NavNodeUpdateModel() { IsLayoutNavNode = isNav };
            return PartialView(model);
        }
        [HttpPost]
        public async Task<JsonResult> Insert(NavNodeDto dto)
        {

            try
            {
                bool result = false;
                string mesage = string.Empty;
                if (ModelState.IsValid)
                {
                    //var dto = _mapper.Map<NavNodeDto>(model);
                    dto.Status = CommonValues.NavNode.NavNodeStatus.Public;
                    dto.CreatedBy = User.Identity.Name;
                    dto.ModifiedBy = User.Identity.Name;
                    var resultDict = await InsertAsync(dto);
                    if (resultDict.ContainsKey(true)) { result = true; }
                    else
                    {
                        mesage = (" " + resultDict.Select(x => x.Value).FirstOrDefault());
                    }
                }
                return Json(new { status = result, msg = mesage }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = false, msg = "" }, JsonRequestBehavior.AllowGet);
            }
            
        }

        private async Task<Dictionary<bool, string>> InsertAsync(NavNodeDto dto)
        {
            var resultDict = new Dictionary<bool, string>() { [false] = "Tạo mới không thành công. Vui lòng thử lại" };
            var _appEventResult = "Fail";
            string _transData = $"ERROR! Not Add  Signature {dto.Name}  by user: {User.Identity.Name}.";
            try
            {
                var result = await _navNodeService.AddAsync(dto);
                if (result)
                {
                    resultDict = new Dictionary<bool, string> { [true] = string.Empty };
                    _transData = $"SUCCESSFUL! ADD  Signature {dto.Name}  by user: {User.Identity.Name}; ";
                    _appEventResult = "Success";
                }
            }
            catch (Exception ex) { }
            #region Log
            //Insert to log
            //this.LogSystem(new SystemLogInsertModel
            //{
            //    Module = AppModule.SignatureManagement,
            //    Action = AppAction.Insert,
            //    Description = _transData,
            //    EventResult = _appEventResult
            //});
            #endregion
            return resultDict;
        }
        #endregion

        #region Update
        [HttpGet]
        public async Task<ActionResult> Update(Guid id)
        {
            var model = new NavNodeUpdateModel();
            var iquery = await _navNodeService.GetByIdAsync(id);
            if (iquery != null)
            {
                model = new NavNodeUpdateModel
                {
                    Id = iquery.Id,
                    Name = iquery.Name,
                    Areas = iquery.Areas,
                    Controller = iquery.Controller,
                    Action = iquery.Action,
                    Params = iquery.Params,
                    ModeAction = CommonValues.Action.Update
                };
                StringBuilder _display = new StringBuilder();
                if (!string.IsNullOrEmpty(iquery.Areas)) { _display.Append(string.Concat("/", iquery.Areas)); }
                if (!string.IsNullOrEmpty(iquery.Controller)) { _display.Append(string.Concat("/", iquery.Controller)); }
                if (!string.IsNullOrEmpty(iquery.Action)) { _display.Append(string.Concat("/", iquery.Action)); }
                if (!string.IsNullOrEmpty(iquery.Params)) { _display.Append(string.Concat("?", iquery.Params)); }
                model.Link = _display.ToString();
            }
            return PartialView(model);
        }

        [HttpPost]
        [ValidateInput(true)]
        public async Task<JsonResult> Update(NavNodeUpdateModel model)
        {
            bool result = false;
            string mesage = string.Empty;
            if (ModelState.IsValid)
            {
                var dto = _mapper.Map<NavNodeDto>(model);
                //dto.Status = CommonValues.NavNode.NavNodeStatus.Public;
                dto.ModifiedBy = User.Identity.Name;
                result = await this.UpdateAsync(dto);
                if (!result)
                {
                    mesage = "Cập nhật không thành công";
                }

            }
            return Json(new { status = result, msg = mesage }, JsonRequestBehavior.AllowGet);
        }

        private async Task<bool> UpdateAsync(NavNodeDto dto)
        {

            bool result = false;
            var _appEventResult = "Fail";
            string _transData = $"ERROR! Not Update Image Signature  {dto.Name} by user: {User.Identity.Name}.";
            try
            {
                result = await _navNodeService.UpdateAsync(dto);
                if (result)
                {
                    _transData = $"SUCCESSFUL! Update Image Signature {dto.Name} by user: {User.Identity.Name}; ";
                    _appEventResult = "Success";
                }
            }
            catch (Exception ex) { }
            #region Log
            //Insert to log
            //this.LogSystem(new SystemLogInsertModel
            //{
            //    Module = AppModule.SignatureManagement,
            //    Action = AppAction.Update,
            //    Description = _transData,
            //    EventResult = _appEventResult
            //});
            #endregion

            return result;
        }
        #endregion


        #region Delete      
        [HttpPost]
        [ValidateInput(true)]
        public async Task<JsonResult> Delete(Guid id)
        {
            bool result = false;
            string _appEventResult = "Fail";
            string _transData = $"ERROR! Not Delete Signature {id}  by User {User.Identity.Name}. ";
            try
            {
                result = await _navNodeService.DeleteAsync(id);
                if (result)
                {
                    _transData = $"SUCCESSFUL!  Signature ID {id} by User {User.Identity.Name}. ";
                    _appEventResult = "Success";
                }
            }
            catch (Exception ex) { }
            #region Log
            //Insert to log
            //this.LogSystem(new SystemLogInsertModel
            //{
            //    Module = AppModule.SignatureManagement,
            //    Action = AppAction.Delete,
            //    Description = _transData,
            //    EventResult = _appEventResult
            //});
            #endregion
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}