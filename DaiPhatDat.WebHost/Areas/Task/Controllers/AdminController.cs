using AutoMapper;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using DaiPhatDat.Core.Kernel.Application.Utilities;
using DaiPhatDat.Core.Kernel.Controllers;
using DaiPhatDat.Core.Kernel.Firebase.Models;
using DaiPhatDat.Core.Kernel.Logger.Application;
using DaiPhatDat.Core.Kernel.Orgs.Application;
using DaiPhatDat.Module.Task.Entities;
using DaiPhatDat.Module.Task.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DaiPhatDat.Core.Kernel;
using Newtonsoft.Json;
using DaiPhatDat.Core.Kernel.Models;
using DaiPhatDat.Core.Kernel.Helper;
using DaiPhatDat.Core.Kernel.Orgs.Application.Contract;

namespace DaiPhatDat.Module.Task.Web
{
    [Authorize]
    [RouteArea("Task")]
    [RoutePrefix("Admin")]
    public class AdminController : CoreController
    {
        public AdminController(ILoggerServices loggerServices, IUserServices userService, IUserDepartmentServices userDepartmentServices, IMapper mapper, IAdminCategoryService adminCategoryService, ITaskItemService taskItemService) : base(loggerServices, userService, userDepartmentServices)
        {
            _mapper = mapper;
            _adminCategoryService = adminCategoryService;
            _taskItemService = taskItemService;
        }
        #region properties
        private IMapper _mapper;
        private IAdminCategoryService _adminCategoryService;
        private ITaskItemService _taskItemService;
        #endregion

        public ActionResult Index(Guid? parentId, Guid? filterId, Guid? folderId, string view, string type = "")
        {
            if (!CurrentUser.HavePermission((EnumModulePermission.Task_FullControl)))
            {
                return RedirectToAction("AccessDenied", "Home");
            }
            TaskViewTypeModel viewType = new TaskViewTypeModel();
            viewType.Name = HomeController.ViewGrantt;
            ViewBag.col_defs = getTitleHeaderByView(view);
            return View(viewType);
        }
        private string getTitleHeaderByView(string view)
        {
            string strReturn = "[]";
            switch (view)
            {
                case null:
                case "":
                    List<TitleTableViewModel> returnData = new List<TitleTableViewModel>();
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    dic.Add("Name", ResourceManagement.GetResourceText("Task.Home.TitleTableName", "Nội dung", "Name"));
                    
                    dic.Add("DateFormatHtml", ResourceManagement.GetResourceText("Task.Home.TitleTableDateFormat", "Thời gian", "Date time"));
                    dic.Add("MemberHtml", ResourceManagement.GetResourceText("Task.Home.TitleTableMember", "Thành viên", "Member"));
                    foreach (KeyValuePair<string, string> item in dic)
                    {
                        TitleTableViewModel add = new TitleTableViewModel();
                        add.field = item.Key;
                        add.displayName = item.Value;
                        if (!item.Key.Equals("Name"))
                        {
                            add.visible = true;
                        }
                        if (item.Key.Length > 4 && item.Key.Substring(item.Key.Length - 4, 4).ToUpper().Equals("HTML"))
                        {
                            add.typeHtml = true;
                        }
                        add.viewName = view;
                        returnData.Add(add);
                    }
                    strReturn = JsonConvert.SerializeObject(returnData);
                    break;
                default:
                    break;
            }
            return strReturn;
        }
        [HttpPost]
        public JsonResult GetAdminCategory(Guid? id)
        {
            AdminCategoryModel model = new AdminCategoryModel();
            try
            {
                AdminCategoryDto dto = new AdminCategoryDto();
                if (id.HasValue)
                {
                    dto = _adminCategoryService.GetById(id.Value);
                    if (dto == null)
                    {
                        var rs = SendMessageResponse.CreateFailedResponse("NotExist");
                        return Json(rs, JsonRequestBehavior.AllowGet);
                    }
                    if (!CurrentUser.HavePermission(EnumModulePermission.Task_FullControl))
                    {
                        var rs = SendMessageResponse.CreateFailedResponse("AccessDenied");
                        return Json(rs, JsonRequestBehavior.AllowGet);
                    }
                }
                model = _mapper.Map<AdminCategoryModel>(dto);
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public async Task<JsonResult> GetAll()
        {
            List<AdminCategoryDto> model = new List<AdminCategoryDto>();
            try
            {
                model = await _adminCategoryService.GetAll();
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetAdminCategories(Guid? parentId, Guid? filterId, Guid? folderId, string view, AdvanceFilterModel filter)
        {
            bool bResult = true;
            string strMsg = string.Empty;
            string strHtmlDate = "<div class='timeline'><span>{0} </span> {1} </div>";
            string strUrl = "/feature/account/Avartar";
            int numberShowInMember = 2;
            Pagination<FetchProjectsTasksResult> pagingData = null;
            try
            {
                if (!string.IsNullOrWhiteSpace(view)) { }
                else
                {
                    //Mặc định là view grantt
                    view = ViewGrantt;
                }
                ProjectFilterParamDto otherParams = new ProjectFilterParamDto();
                
                var fetchTaskItemsResult = new List<FetchProjectsTasksResult>();
                var lstParams = new List<string>();
                if (parentId.HasValue && !parentId.Value.Equals(Guid.Empty))
                {
                    lstParams.Add($"@ParentId:{parentId}");
                }
                pagingData = _adminCategoryService.GetTaskWithFilterPaging(filter.KeyWord, lstParams, filter.CurrentPage, filter.PageSize, " CreatedDate DESC ", CurrentUser, parentId != null ? false : true);
                if (pagingData != null && pagingData.Result != null && pagingData.Result.Count() > 0)
                {
                    var userDepartments = _userDepartmentServices.GetCachedUserDepartmentDtos();
                    var data = pagingData.Result;
                    fetchTaskItemsResult = data
                        .Select(x =>
                        {
                            x.CurrentPage = x.CurrentPage;
                            x.TotalRecord = x.TotalRecord;
                            x.PageSize = x.PageSize;
                            x.ParentId = x.ParentId;
                            if (x.Type == "project")
                            {
                                x.UserId = x.AssignBy;
                            }
                            else
                            {
                                x.DragDrop = CurrentUser.Id == x.AssignBy || CurrentUser.Id == x.CreatedBy;
                            }
                            if (string.IsNullOrEmpty(x.Type))
                            {
                                x.Type = "task";
                            }
                            if (view != null && (view.Equals("grantt") || view.Equals("table")))
                            {
                                DateTime dtNow = DateTime.Today;
                                if (x.FromDate.HasValue && x.ToDate.HasValue)
                                {
                                    int result = DateTime.Compare(dtNow, x.ToDate.Value);
                                    string strReturn = string.Concat((x.FromDate.Value.Year == DateTime.Today.Year) ? ConvertToStringExtensions.DateToStringLocal(x.FromDate, "dd MMM") : ConvertToStringExtensions.DateToStringLocal(x.FromDate, "dd MMM, yyyy"), " - ", (x.ToDate.Value.Year == DateTime.Today.Year) ? ConvertToStringExtensions.DateToStringLocal(x.ToDate, "dd MMM") : ConvertToStringExtensions.DateToStringLocal(x.ToDate, "dd MMM, yyyy"));
                                    if (result <= 0)
                                    {
                                        TimeSpan value = x.ToDate.Value.Subtract(dtNow);
                                        x.DateFormatHtml = string.Format(strHtmlDate, strReturn, "");
                                    }
                                    else
                                    {
                                        x.DateFormatHtml = string.Format(strHtmlDate, strReturn, "");
                                    }
                                }
                                
                                x.MemberHtml = htmlUserProcess(userDepartments.Result, x.UsersPrimary, x.UsersSecond, x.UsersThird, strUrl, numberShowInMember);
                            }
                            return x;
                        })
                        .ToList();
                    if (parentId == null)
                    {
                        FetchProjectsTasksResult dataEmpty = data.FirstOrDefault();
                        int iTotalRecord = dataEmpty.TotalRecord;
                        int iCurrentPage = dataEmpty.CurrentPage;
                        int iPageSize = dataEmpty.PageSize;
                        if (iCurrentPage * iPageSize < iTotalRecord)
                        {
                            FetchProjectsTasksResult newPagination = new FetchProjectsTasksResult();
                            newPagination.Id = Guid.NewGuid();
                            newPagination.ParentId = parentId;
                            newPagination.Name = ResourceManagement.GetResourceText("Task.Home.TableMore", "Xem thêm", "More");
                            newPagination.CurrentPage = iCurrentPage + 1;
                            newPagination.TotalRecord = iTotalRecord;
                            newPagination.PageSize = iPageSize;
                            newPagination.HasPagination = true;
                            newPagination.HasChildren = false;
                            newPagination.DragDrop = false;
                            newPagination.Type = dataEmpty.Type;
                            fetchTaskItemsResult.Add(newPagination);
                        }
                    }
                }
                pagingData.Result = fetchTaskItemsResult;
                return Json(new { status = bResult, msg = strMsg, data = pagingData }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                bResult = false;
                strMsg = ex.Message.ToString();
                _loggerServices.WriteError(ex.Message.ToString());
            }
            return Json(new { status = bResult, msg = strMsg, data = pagingData });
        }
        private string htmlUserProcess(IEnumerable<UserDepartmentDto> userDepartments, string strPrimary, string strSecond, string strThird, string url, int iShow = 2)
        {
            int iIndex = 0;
            List<UserProcessViewDto> lstView = new List<UserProcessViewDto>();
            StringBuilder strBuilder = new StringBuilder();
            try
            {
                if (strPrimary != null && strPrimary.Length > 0)
                {
                    Guid[] values = strPrimary.Split(';').Select(s => Guid.Parse(s.ToString())).ToArray();
                    var primary = userDepartments.Where(x => values.Contains(x.UserID)).OrderBy(e => e.JobTitleOrderNumber).ThenBy(e => e.FullName).ToList();
                    if (primary != null && primary.Count > 0)
                    {
                        foreach (UserDepartmentDto item in primary)
                        {
                            iIndex++;
                            lstView.Add(new UserProcessViewDto() { ViewType = 1, ViewTypeName = "user-primary", Name = item.FullName, JobTitle = item.JobTitleName, UserId = item.Id, STT = iIndex, DeptName = item.DeptName, SrcImage = item.UserID.ToString() });
                        }
                    }
                }
                if (strSecond != null && strSecond.Length > 0)
                {
                    Guid[] values = strSecond.Split(';').Select(s => Guid.Parse(s.ToString())).ToArray();
                    var second = userDepartments.Where(x => values.Contains(x.UserID)).OrderBy(e => e.JobTitleOrderNumber).ThenBy(e => e.FullName).ToList();
                    if (second != null && second.Count > 0)
                    {
                        foreach (UserDepartmentDto item in second)
                        {
                            iIndex++;
                            lstView.Add(new UserProcessViewDto() { ViewType = 2, ViewTypeName = "user-second", Name = item.FullName, JobTitle = item.JobTitleName, UserId = item.Id, STT = iIndex, DeptName = item.DeptName, SrcImage = item.UserID.ToString() });
                        }
                    }
                }
                if (strThird != null && strThird.Length > 0)
                {
                    Guid[] values = strThird.Split(';').Select(s => Guid.Parse(s.ToString())).ToArray();
                    var third = userDepartments.Where(x => values.Contains(x.UserID)).OrderBy(e => e.JobTitleOrderNumber).ThenBy(e => e.FullName).ToList();
                    if (third != null && third.Count > 0)
                    {
                        foreach (UserDepartmentDto item in third)
                        {
                            iIndex++;
                            lstView.Add(new UserProcessViewDto() { ViewType = 3, ViewTypeName = "user-third", Name = item.FullName, JobTitle = item.JobTitleName, UserId = item.Id, STT = iIndex, DeptName = item.DeptName, SrcImage = item.UserID.ToString() });
                        }
                    }
                }
                if (lstView.Count > 0)
                {
                    strBuilder.AppendLine("<div class='dropdown dropdown-inline'>");
                    strBuilder.AppendLine("<a data-toggle='dropdown' aria-haspopup='true' aria-expanded='false'> <div class='symbol-group'>");

                    var lstHtml2 = lstView.OrderBy(x => x.ViewType).Take(iShow).Skip(0);
                    string html2 = "<div class='symbol symbol-25 symbol-circle {2}'>";
                    html2 += "<img alt = 'Pic' src='{1}'>";
                    html2 += "<div class='symbol-name'>{0}</div>";
                    html2 += "</div>";
                    foreach (var item in lstHtml2)
                    {
                        strBuilder.AppendLine(string.Format(html2, item.Name, string.Concat(url, "/", item.SrcImage), item.ViewTypeName));
                    }
                    if (lstView.Count - iShow > 0)
                    {
                        strBuilder.AppendLine("<div class='symbol symbol-25 symbol-circle symbol-light dropdown dropdown-inline' data-toggle='tooltip' title=''>");
                        strBuilder.AppendLine(string.Format("<span class='symbol-label font-weight-bold'>+{0}</span> </div>", lstView.Count - iShow));
                    }
                    strBuilder.AppendLine(" </div></a>");
                    //
                    strBuilder.AppendLine(" <div class='dropdown-menu p-0 m-0 dropdown-menu-md dropdown-menu-right'>");
                    strBuilder.AppendLine(" <ul class='navi p-3'>");


                    string html4 = " <li class='navi-item d-flex py-2'>";
                    html4 += " <div class='symbol symbol-30 symbol-circle {4}' data-toggle='tooltip' title='' data-original-title='{0}'>";
                    html4 += " <img alt = 'Pic' src='{1}'>";
                    html4 += " </div>";
                    html4 += " <div class='d-block ml-3'> <div class='font-weight-bold'><span class='text-dark font-weight-bolder'>{0}</span><span class='text-muted font size-sm'>{2}</span> </div> <div class='text-dark-75 font-size-sm font-weight-bold'>{3}</div></div>";
                    html4 += " </li>";
                    foreach (var item in lstView.OrderBy(x => x.ViewType))
                    {
                        strBuilder.AppendLine(string.Format(html4, item.Name, string.Concat(url, "/", item.SrcImage), item.JobTitle, item.DeptName, item.ViewTypeName));
                    }
                    strBuilder.AppendLine(" </ul>");
                    strBuilder.AppendLine(" </div>");
                    strBuilder.AppendLine(" </div> ");
                }
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.Message);
            }
            return strBuilder.ToString();
        }
        [HttpPost]
        public async Task<JsonResult> SaveAdminCategory(AdminCategoryModel model)
        {
            SendMessageResponse rs = null;
            try
            {

                AdminCategoryDto dto = new AdminCategoryDto();
                //check permission
                dto = _mapper.Map<AdminCategoryDto>(model);
                if (!CurrentUser.HavePermission(EnumModulePermission.Task_FullControl))
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
                dto.ModifiedBy = CurrentUser.Id;
                dto.ModifiedDate = DateTime.Now;
                
                rs = await _adminCategoryService.SaveAsync(dto);

            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
            }
            return Json(rs, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<JsonResult> DeleteAdminCategory(AdminCategoryModel model)
        {
            SendMessageResponse rs = null;
            try
            {
                AdminCategoryDto dto = new AdminCategoryDto();
                dto = _mapper.Map<AdminCategoryDto>(model);
                if (!CurrentUser.HavePermission(EnumModulePermission.Task_FullControl))
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
                dto.ModifiedBy = CurrentUser.Id;
                dto.ModifiedDate = DateTime.Now;
                rs = await _adminCategoryService.Delete(dto);
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
            }
            return Json(rs, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<JsonResult> GetNewTaskItem(TaskItemModel model)
        {
            try
            {
                TaskItemDto dto = _mapper.Map<TaskItemDto>(model);
                dto = await _taskItemService.GetNewAdminTask(dto);
                dto.TaskItemAssigns.Add(new TaskItemAssignDto
                {
                    AssignToFullName = CurrentUser.FullName,
                    AssignToJobTitleName = CurrentUser.Departments != null ? CurrentUser.Departments[0].JobTitle : string.Empty,
                    Department = CurrentUser.Departments != null ? CurrentUser.Departments[0].Name : string.Empty,
                    AssignTo = CurrentUser.Id,
                    TaskType = Entities.TaskType.Primary
                });
                dto.AssignBy = CurrentUser.Id; dto.AssignByFullName = CurrentUser.FullName;
                model = _mapper.Map<TaskItemModel>(dto);
                model.TaskItemAssignIds = model.TaskItemAssigns.Select(e => e.AssignTo).ToList();
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        private async Task<SendMessageResponse> SaveTask(TaskItemModel model, Entities.TaskItemStatusId taskItemStatusId = Entities.TaskItemStatusId.New)
        {
            SendMessageResponse rs = null;
            try
            {
                TaskItemDto dto = new TaskItemDto();
                //check permission
                dto = _mapper.Map<TaskItemDto>(model);
                dto.IsFullControl = false;
                if (CurrentUser.HavePermission(EnumModulePermission.Task_FullControl))
                {
                    dto.IsFullControl = true;
                }
                dto.ModifiedBy = CurrentUser.Id;
                dto.ModifiedDate = DateTime.Now;
                dto.Attachments = new List<AttachmentDto>();
                if (Request.Files.Count > 0)
                {
                    foreach (string file in Request.Files)
                    {
                        var fileContent = Request.Files[file];
                        byte[] document = Utility.ReadAllBytes(fileContent);
                        string ext = Path.GetExtension(fileContent.FileName).Replace(".", "");

                        AttachmentDto attachmentDto = new AttachmentDto()
                        {
                            Id = Guid.NewGuid(),
                            CreateByFullName = CurrentUser.FullName,
                            CreatedBy = CurrentUser.Id,
                            CreatedDate = DateTime.Now,
                            FileExt = ext,
                            FileContent = document,
                            FileName = fileContent.FileName,
                            FileSize = fileContent.ContentLength,
                            Source = Entities.Source.TaskItem,
                        };
                        dto.Attachments.Add(attachmentDto);
                    }
                }
                rs = await _taskItemService.SaveAdminTaskAsync(dto, taskItemStatusId);

            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
            }
            return rs;
        }
        [HttpPost]
        public async Task<JsonResult> SaveTaskItem(TaskItemModel model)
        {
            SendMessageResponse rs = null;
            rs = await SaveTask(model);
            return Json(rs, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<JsonResult> GetTaskItem(Guid? id, bool isAdminCategory = false)
        {
            TaskItemModel model = new TaskItemModel();
            try
            {
                TaskItemDto dto = null;
                if (id.HasValue)
                {
                    dto = await _taskItemService.GetAdminTaskById(id.Value);
                    if (dto == null)
                    {
                        var rs = SendMessageResponse.CreateFailedResponse("NotExist");
                        return Json(rs, JsonRequestBehavior.AllowGet);
                    }
                    if (dto.AssignBy != CurrentUser.Id && !CurrentUser.HavePermission(EnumModulePermission.Task_FullControl))
                    {
                        var rs = SendMessageResponse.CreateFailedResponse("AccessDenied");
                        return Json(rs, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    dto = new TaskItemDto { AssignBy = CurrentUser.Id, AssignByFullName = CurrentUser.FullName };
                    if (isAdminCategory)
                    {
                        dto.IsAdminCategory = true;
                    }
                    dto.TaskItemAssigns.Add(new TaskItemAssignDto
                    {
                        AssignToFullName = CurrentUser.FullName,
                        AssignToJobTitleName = CurrentUser.Departments != null ? CurrentUser.Departments[0].JobTitle : string.Empty,
                        Department = CurrentUser.Departments != null ? CurrentUser.Departments[0].Name : string.Empty,
                        AssignTo = CurrentUser.Id,
                        TaskType = Entities.TaskType.Primary
                    });
                }
                model = _mapper.Map<TaskItemModel>(dto);
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #region Const
        public const string PartialViewTaskInMain = "_ViewTaskInMain";
        public const string ViewGrantt = "grantt";
        #endregion
    }
}
