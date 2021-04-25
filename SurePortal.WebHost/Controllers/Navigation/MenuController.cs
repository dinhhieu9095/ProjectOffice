using AutoMapper;
using SurePortal.Core.Kernel;
using SurePortal.Core.Kernel.Domain.ValueObjects;
using SurePortal.Core.Kernel.Orgs.Application;
using SurePortal.WebHost.Models;
using SurePortal.WebHost.Models.Navigation;
using SurePortal.WebHost.Modules.Navigation.Application.Dto;
using SurePortal.WebHost.Modules.Navigation.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SurePortal.WebHost.Controllers
{
    public class MenuController : BaseMenuController
    {
        #region ctor
        private readonly INavNodeService _navNodeService;
        private readonly IGroupServices _groupServices;
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="menuService"></param>
        /// <param name="mapper"></param>
        public MenuController(IMenuService menuService, IMapper mapper,
            INavNodeService navNodeService, IUserDepartmentServices userDepartmentServices, IUserServices userServices, IGroupServices groupServices) : base(menuService, mapper, userDepartmentServices, userServices)
        {
            _navNodeService = navNodeService;
            _groupServices = groupServices;
        }

        #endregion
        // GET: Menu
        #region Index
        public async Task<ActionResult> Index()
        {
            var model = new MenuIndexModel();
            return View(model);
        }
        [HttpPost]
        [ValidateInput(true)]
        public async Task<ActionResult> GetPaging(int pageIndex, string keyWord)
        {
            int _pageSize = 10;
            var model = new MenuPagingModel() { PageIndex = pageIndex,PageSize= _pageSize };
            try
            {
                string curentUser = User.Identity.Name;
                //if (IS_SUPER_ADMIN()) { curentUser = string.Empty; }
                var iqueryData = await _menuService.GetPagingAsync(new SearchMenuDto
                {
                    UserName = curentUser,
                    KeyWord = keyWord,
                    PageIndex = pageIndex,
                    PageSize = _pageSize
                });
                model.TotalPage = iqueryData.Item1;
                model.LstData = _mapper.Map<List<MenuModel>>(iqueryData.Item2);
            }
            catch (Exception ex) { /*this.LogError(this, ex); */}
            return PartialView(model);
        }
        #endregion

        #region Insert
        public ActionResult Insert()
        {
            var model = new MenuUpdateModel();
            var lstMenu = _menuService.GetAllRecursive();
            var roles = _menuService.GetAllRole();
            model.LstMenu = lstMenu
              .Select(x => new SelectListItem
              {
                  Text = x.Name,
                  Value = x.Id.ToString()
              });
            model.ListRole = roles
              .Select(x => new SelectListItem
              {
                  Text = x.Name,
                  Value = x.ID.ToString()
              });
            return View(model);
        }


        [HttpPost]
        public async Task<ActionResult> Insert(MenuUpdateModel model)
        {
            string msg = string.Empty;
            if (ModelState.IsValid)
            {
                var dto = _mapper.Map<MenuDto>(model);
                dto.CreatedBy = User.Identity.Name;
                dto.ModifiedBy = User.Identity.Name;
                var resultDict = await InsertAsync(dto);
                if (resultDict.ContainsKey(true))
                {
                    return RedirectToAction(Constant.Action.Index, Constant.Controller.Menu, new { });
                }
                msg = (" " + resultDict.Select(x => x.Value).FirstOrDefault());
            }
            if (string.IsNullOrEmpty(msg)) { msg = string.Concat("Tạo mới không thành công. Vui lòng thử lại!", msg); }
            else { string.Concat(msg, ". Vui lòng thử lại!"); }
            ModelState.AddModelError("ErrSubmit", (msg));
            return View(model);
        }

        private async Task<Dictionary<bool, string>> InsertAsync(MenuDto dto)
        {
            var resultDict = new Dictionary<bool, string>();
            var _appEventResult = "Fail";
            string _transData = $"ERROR! Not Add  Signature {dto.Name}  by user: {User.Identity.Name}.";
            try
            {
                var result = await _menuService.AddAsync(dto);
                if (result != null)
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
            var model = new MenuUpdateModel();
            var iquery = await _menuService.GetByIdAsync(id);
            if (iquery != null)
            {
                var lstMenu = _menuService.GetAllRecursive();
                var roles = _menuService.GetAllRole();
                model = new MenuUpdateModel
                {
                    Id = iquery.Id,
                    ParentId = iquery.ParentId,
                    Layout = iquery.Layout,
                    Status = iquery.Status,
                    TypeModule = iquery.TypeModule,
                    Name = iquery.Name,
                    Code = iquery.Code,
                    Target = iquery.Target,
                    NavNodeId = iquery.NavNodeId,
                    Icon = iquery.Icon,
                    Order = iquery.Order,
                    GroupOrUsers = iquery.GroupOrUsers,
                    Action = Common.CommonValues.Action.Update,
                    ListRole = roles.Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.ID.ToString(),
                        Selected = !string.IsNullOrEmpty(iquery.Roles) && iquery.Roles.Contains(x.ID.ToString()) ? true : false
                    }),
                    LstMenu = lstMenu
                          .Select(x => new SelectListItem
                          {
                              Text = x.Name,
                              Value = x.Id.ToString()
                          })
                };
                List<SelectListItem> listGroupOrUser = new List<SelectListItem>();
                if (!string.IsNullOrEmpty(model.GroupOrUsers))
                {
                    var groupOrUsers = model.GroupOrUsers.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    foreach (var item in groupOrUsers)
                    {
                        var listItem = new SelectListItem();
                        if (item.Contains("_User"))
                        {
                            var userId = new Guid(item.Replace("_User", ""));
                            var user = _userServices.GetById(userId);
                             
                            listItem.Text = user.FullName + " (" + user.UserName + ")";
                            listItem.Value = item;
                        }
                        if (item.Contains("_Group"))
                        {
                            var groupId = new Guid(item.Replace("_Group", ""));

                            var group = _groupServices.GetById(groupId);
                            listItem.Text = group.Name;
                            listItem.Value = item;
                        }

                        listGroupOrUser.Add(listItem);
                    }
                }

                model.ListGroupOrUser = listGroupOrUser;

                if (iquery.NavNodeId.HasValue)
                {
                    var iqueryNav = await _navNodeService.GetByIdAsync(iquery.NavNodeId.Value);
                    if (iqueryNav != null)
                    {
                        StringBuilder _display = new StringBuilder();
                        if (!string.IsNullOrEmpty(iqueryNav.Areas)) { _display.Append(string.Concat("/", iqueryNav.Areas)); }
                        if (!string.IsNullOrEmpty(iqueryNav.Controller)) { _display.Append(string.Concat("/", iqueryNav.Controller)); }
                        if (!string.IsNullOrEmpty(iqueryNav.Action)) { _display.Append(string.Concat("/", iqueryNav.Action)); }
                        if (!string.IsNullOrEmpty(iqueryNav.Params)) { _display.Append(string.Concat("?", iqueryNav.Params)); }
                        model.Link = _display.ToString();
                        model.navAutocomplete = iqueryNav.Name;
                    }
                }
            }
            return View(model);
        }



        [HttpPost]
        [ValidateInput(true)]
        public async Task<ActionResult> Update(MenuUpdateModel model)
        {
            if (ModelState.IsValid)
            {
                var dto = _mapper.Map<MenuDto>(model);
                dto.ModifiedBy = User.Identity.Name;
                var result = await this.UpdateAsync(dto);
                if (result)
                {
                    return RedirectToAction(Constant.Action.Index, Constant.Controller.Menu, new { });
                }
            }
            ModelState.AddModelError("ErrSubmit", "Lỗi! cập nhật chữ ký");
            return View(model);
        }

        private async Task<bool> UpdateAsync(MenuDto dto)
        {

            bool result = false;
            var _appEventResult = "Fail";
            string _transData = $"ERROR! Not Update Image Signature  {dto.Name} by user: {User.Identity.Name}.";
            try
            {
                result = await _menuService.UpdateAsync(dto);
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

        #region Active,Deactive       
        /// <summary>
        /// ChangeActive
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(true)]
        public async Task<JsonResult> ChangeActive(Guid id)
        {
            bool result = false;
            try
            {
                if (id != Guid.Empty)
                {

                }
            }
            catch (Exception) { result = false; }
            return Json(result, JsonRequestBehavior.AllowGet);
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
                result = await _menuService.DeleteAsync(id);
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

        #region EasyAutocomplete
        public async Task<JsonResult> EasyAutocomplete(string keySearch)
        {
            var iquery = await _navNodeService.EasyAutocomplete(keySearch);
            var LstNavNode = _mapper.Map<List<NavNodeModel>>(iquery);
            var data = LstNavNode.Select(x => new
            {
                id = x.Id,
                name = x.Name,
                displayName = x.DisplayLink
            });
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> SearchUser(Guid deptId, int pageIndex, int pageSize, string keyword)
        {
            var users = _userServices.GetUsers(pageIndex, pageSize, keyword);
            var groups = _groupServices.GetGroups(pageIndex, pageSize, keyword);
            var result = users.Select(e => new
            {
                id = e.Id.ToString() + "_User",
                text = e.FullName + "(" + e.UserName + ")",
            }).Distinct().ToList();
            result.AddRange(groups.Select(e => new
            {
                id = e.ID.ToString() + "_Group",
                text = e.Name
            }));
            return Json(result, JsonRequestBehavior.AllowGet);
        }



        #endregion

        #region Info navnode
        [HttpPost]
        public async Task<ActionResult> NavNodeInfo(Guid navNodeId)
        {
            var iquery = await _navNodeService.GetByIdAsync(navNodeId);
            if (iquery != null)
            {

                var model = new NavNodeInfoModel
                {
                    Name = iquery.Name,
                    Areas = iquery.Areas,
                    Controller = iquery.Controller,
                    Action = iquery.Action
                };
                return PartialView(model);
            }
            return PartialView(new NavNodeInfoModel());
        }

        public async Task<JsonResult> ConvertNameToURL(string name)
        {
            string data = CommonUtility.ToUnsignString(name);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Get tree menu

        [HttpGet]
        public ActionResult LeftMenu()
        {
            var data = _menuService.GetTreeMenu("menu-left");
            return PartialView(data); 
        }

        [HttpGet]
        public ActionResult TopMenu()
        {
            var data = _menuService.GetTreeMenu("menu-top");
            return PartialView(data); 
        }
        [HttpGet]
        public ActionResult AdminMenu()
        {
            var data = _menuService.GetTreeMenu("menu-admin");
            return PartialView(data);
        }
        [HttpGet]
        public ActionResult UserProfileMenu()
        {
            var data = _menuService.GetTreeMenu("user-profile");
            return PartialView(data);
        }
        #endregion
    }
}