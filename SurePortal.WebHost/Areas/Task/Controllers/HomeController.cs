using AutoMapper;
using Newtonsoft.Json;
using DaiPhatDat.Core.Kernel;
using DaiPhatDat.Core.Kernel.Controllers;
using DaiPhatDat.Core.Kernel.Helper;
using DaiPhatDat.Core.Kernel.Logger.Application;
using DaiPhatDat.Core.Kernel.Models;
using DaiPhatDat.Core.Kernel.Orgs.Application;
using DaiPhatDat.Core.Kernel.Orgs.Application.Contract;
using DaiPhatDat.Module.Task.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DaiPhatDat.Module.Task.Web
{
    [Authorize]
    [RouteArea("Task")]
    [RoutePrefix("Home")]
    public class HomeController : CoreController
    {
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;
        private readonly IUserDepartmentServices _userdepartmentServices;
        private IProjectFilterParamService _projectFilterParamService;
        public HomeController(ILoggerServices loggerServices
            , IUserServices userService
            , IUserDepartmentServices userDepartmentServices
            , IProjectFilterParamService projectFilterParamService
             , IProjectService projectService
            , IMapper mapper
            ) :
            base(loggerServices, userService, userDepartmentServices)
        {
            _projectFilterParamService = projectFilterParamService;
            _projectService = projectService;
            _mapper = mapper;
            _userdepartmentServices = userDepartmentServices;
        }
        [Route("Index")]
        public ActionResult Index(Guid? parentId, Guid? filterId, Guid? folderId, string view, string type = "")
        {
            TaskViewTypeModel viewType = new TaskViewTypeModel();
            bool isView = true;
            switch (view)
            {
                case ViewCalendar:
                    viewType.Name = HomeController.ViewCalendar;
                    break;
                case ViewKanban:
                    viewType.Name = HomeController.ViewKanban;
                    break;
                case ViewTable:
                case ViewGrantt:
                case null:
                case "":
                    viewType.Name = HomeController.ViewTable;
                    break;
                default:
                    isView = false;
                    viewType.Name = HomeController.ViewTable;
                    break;
            }
            if (!isView)
            {
                return RedirectToAction(HomeController.NotFoundAction, HomeController.ControllerName);
            }

            ViewBag.col_defs = getTitleHeaderByView(view);
            return View(viewType);
        }
        private string getTitleHeaderByView(string view)
        {
            string strReturn = "[]";
            switch (view)
            {
                case ViewCalendar:
                    List<TitleKanbanViewModel> viewCalendar = new List<TitleKanbanViewModel>();
                    viewCalendar.Add(new TitleKanbanViewModel() { StatusId = 0, DisplayName = ResourceManagement.GetResourceText("Task.Home.User", "Người dùng", "User") });
                    viewCalendar.Add(new TitleKanbanViewModel() { StatusId = 0, DisplayName = ResourceManagement.GetResourceText("Task.Home.MonDay", "Thứ 2", "MonDay") });
                    viewCalendar.Add(new TitleKanbanViewModel() { StatusId = 1, DisplayName = ResourceManagement.GetResourceText("Task.Home.Tuesday", "Thứ 3", "Tuesday") });
                    viewCalendar.Add(new TitleKanbanViewModel() { StatusId = 2, DisplayName = ResourceManagement.GetResourceText("Task.Home.Wednesday", "Thứ 4", "Wednesday") });
                    viewCalendar.Add(new TitleKanbanViewModel() { StatusId = 4, DisplayName = ResourceManagement.GetResourceText("Task.Home.Thursday", "Thứ 5", "Thursday") });
                    viewCalendar.Add(new TitleKanbanViewModel() { StatusId = 4, DisplayName = ResourceManagement.GetResourceText("Task.Home.Friday", "Thứ 6", "Friday") });
                    viewCalendar.Add(new TitleKanbanViewModel() { StatusId = 4, DisplayName = ResourceManagement.GetResourceText("Task.Home.Saturday", "Thứ 7", "Saturday") });
                    viewCalendar.Add(new TitleKanbanViewModel() { StatusId = 4, DisplayName = ResourceManagement.GetResourceText("Task.Home.Sunday", "Chủ nhật", "Sunday") });
                    strReturn = JsonConvert.SerializeObject(viewCalendar);
                    break;
                case ViewKanban:
                    List<TitleKanbanViewModel> viewKaban = new List<TitleKanbanViewModel>();
                    viewKaban.Add(new TitleKanbanViewModel() { StatusId = 0, DisplayName = ResourceManagement.GetResourceText("Task.Home.TitleKanBanNew", "Công việc mới", "New task") });
                    viewKaban.Add(new TitleKanbanViewModel() { StatusId = 1, DisplayName = ResourceManagement.GetResourceText("Task.Home.TitleKanBanProcessing", "Đang xử lý", "Processing") });
                    viewKaban.Add(new TitleKanbanViewModel() { StatusId = 2, DisplayName = ResourceManagement.GetResourceText("Task.Home.TitleKanBanUpdatedReport", "Cập nhật tiến độ/ báo cáo", "Updated/Report") });
                    viewKaban.Add(new TitleKanbanViewModel() { StatusId = 4, DisplayName = ResourceManagement.GetResourceText("Task.Home.TitleKanBanFinishedCancel", "Kết thúc/Hủy", "Finished/Cancel") });
                    strReturn = JsonConvert.SerializeObject(viewKaban);
                    break;
                case ViewGrantt:
                    List<TitleTableViewModel> returnData = new List<TitleTableViewModel>();
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    dic.Add("Name", ResourceManagement.GetResourceText("Task.Home.TitleTableName", "Nội dung", "Name"));
                    dic.Add("StatusHtml", ResourceManagement.GetResourceText("Task.Home.TitleTableStatus", "Trạng thái", "Status"));
                    dic.Add("DateFormatHtml", ResourceManagement.GetResourceText("Task.Home.TitleTableDateFormat", "Thời gian", "Date time"));
                    dic.Add("MemberHtml", ResourceManagement.GetResourceText("Task.Home.TitleTableMember", "Thành viên", "Member")); ;
                    dic.Add("ProcessHtml", ResourceManagement.GetResourceText("Task.Home.TitleTableProcessFinish", "Tiến độ", "Process finish"));
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
                case ViewTable:
                case null:
                case "":
                    List<TitleTableViewModel> returnDataT = new List<TitleTableViewModel>();
                    Dictionary<string, string> dicT = new Dictionary<string, string>();
                    dicT.Add("Name", ResourceManagement.GetResourceText("Task.Home.TitleTableName", "Nội dung", "Name"));
                    dicT.Add("StatusHtml", ResourceManagement.GetResourceText("Task.Home.TitleTableStatus", "Trạng thái", "Status"));
                    dicT.Add("DateFormatHtml", ResourceManagement.GetResourceText("Task.Home.TitleTableDateFormat", "Thời gian", "Date time"));
                    dicT.Add("MemberHtml", ResourceManagement.GetResourceText("Task.Home.TitleTableMember", "Thành viên", "Member")); ;
                    foreach (KeyValuePair<string, string> item in dicT)
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
                        returnDataT.Add(add);
                    }
                    strReturn = JsonConvert.SerializeObject(returnDataT);
                    break;
                default:
                    break;
            }
            return strReturn;
        }
        //[Route("treegrid")]
        //public ActionResult TreeGrid()
        //{
        //    return View();
        //}
        [Route("DemoForm")]
        public ActionResult DemoForm()
        {
            return View();
        }
        //[Route("ViewTaskInTable")]
        //public ActionResult ViewTaskInTable()
        //{
        //    return PartialView();
        //}
        //[Route("ViewTaskInKaban")]
        //public ActionResult ViewTaskInKaban()
        //{
        //    return PartialView();
        //}
        //[Route("ViewTaskInKanban")]
        //public ActionResult ViewTaskInKanban()
        //{
        //    return PartialView();
        //}
        //[Route("ViewTaskInGrantt")]
        //public ActionResult ViewTaskInGrantt()
        //{
        //    TempData[HomeController.TaskViewType] = HomeController.ActionViewTaskInGrantt;
        //    return PartialView();
        //}
        //[Route("ViewTaskInGrantt")]
        //public ActionResult ViewTaskInCalendar()
        //{
        //    TempData[HomeController.TaskViewType] = HomeController.ActionViewTaskInCalendar;
        //    return PartialView();
        //}
        [Route("ViewTaskInMain")]
        public ActionResult ViewTaskInMain()
        {
            return PartialView(HomeController.PartialViewTaskInMain);
        }
        [Route("GetDataByProject")]
        [HttpPost]
        public async Task<JsonResult> GetDataByProject(Guid? parentId, Guid? filterId, Guid? folderId, string view, AdvanceFilterModel filter)
        {
            bool bResult = true;
            string strMsg = string.Empty;
            string strHtmlProcess = "<div class='progress{1}{0}'><div class='progress-bar' style='width: {2}%;' role='progressbar' aria-valuenow='{2}' aria-valuemin='0' aria-valuemax='100'>{3}</div></div>";
            string strHtmlStatus = "<div class='progress-task {3}'> <div class='process-label'>{2}</div><div class='progress'><div class='progress-bar' role='progressbar' style='width: {0}%;' aria-valuenow='{0}' aria-valuemin='0' aria-valuemax='100'></div>{1}</div></div>";
            string strHtmlDate = "<div class='timeline'><span>{0} </span> {1} </div>";
            string strHtmlDateHover = " <div class='timeline-day'><i class='far fa-clock mr-2'></i>{0}</div>";
            string strUrl = "/module/account/Avartar";
            string strValueDay = ResourceManagement.GetResourceText("Task.Home.TableLeftDay", "còn lại {0} ngày", "{0} day left");
            string strValueDays = ResourceManagement.GetResourceText("Task.Home.TableLeftDays", "còn lại {0} ngày", "{0} days left");
            int numberShowInMember = 2;
            Pagination<FetchProjectsTasksResult> pagingData = null;
            try
            {
                if (view != null && view.Length > 0) { }
                else
                {
                    view = ViewTable;
                }
                ProjectFilterParamDto otherParams = new ProjectFilterParamDto();
                if (filterId.HasValue && !filterId.Equals(Guid.Empty))
                {
                    otherParams = _projectFilterParamService.GetById(filterId.Value);

                }
                var fetchTaskItemsResult = new List<FetchProjectsTasksResult>();
                var lstParams = new List<string>();
                if (parentId.HasValue && !parentId.Value.Equals(Guid.Empty))
                {
                    lstParams.Add($"@ParentId:{parentId}");
                }
                if (!string.IsNullOrEmpty(otherParams.ParamValue))
                {
                    var arrStr = otherParams.ParamValue.Split(';');
                    lstParams.AddRange(arrStr.ToList());
                }
                if (folderId.HasValue && !folderId.Value.Equals(Guid.Empty))
                {
                    pagingData = _projectService.GetProjectsByFolderPaging(
                                         filter.KeyWord,
                                          lstParams,
                                       filter.CurrentPage,
                                        filter.PageSize,
                                          " CreatedDate DESC ",
                                          CurrentUser,
                                          true);
                }
                else
                {
                    pagingData = _projectService.GetTaskWithFilterPaging(filter.KeyWord, lstParams, filter.CurrentPage, filter.PageSize, " CreatedDate DESC ", CurrentUser, parentId != null ? false : true);
                }
                {
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
                                if (view != null && view.Equals(ViewKanban))
                                {
                                    x.FullName = getPrimaryNameByViewKanBan(userDepartments.Result, x.UsersPrimary, x.Type);
                                }
                                //x.Process = x.Process;
                                x.ProcessClass = x.ProcessClass;
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
                                            string strHover = string.Format(strHtmlDateHover, value.Days > 1 ? string.Format(strValueDays, value.Days) : string.Format(strValueDay, value.Days));
                                            x.DateFormatHtml = string.Format(strHtmlDate, strReturn, strHover);
                                        }
                                        else
                                        {
                                            x.DateFormatHtml = string.Format(strHtmlDate, strReturn, "");
                                        }
                                    }
                                    if (view.Equals("grantt") && x.FromDate.HasValue)
                                    {
                                        x.ProcessHtml = TaskInDueDate.HtmlProcess(x.Process, x.ProcessClass, strHtmlProcess, x.PercentFinish);
                                    }
                                    else
                                    {
                                        x.ProcessHtml = "";
                                    }
                                    x.MemberHtml = htmlUserProcess(userDepartments.Result, x.UsersPrimary, x.UsersSecond, x.UsersThird, strUrl, numberShowInMember);
                                }
                                else
                                {
                                    x.ProcessHtml = x.PercentFinish != null ? x.PercentFinish.ToString() + "%" : "";
                                }
                                x.Status = x.Status;
                                x.StatusId = getStatusId(x.Status, x.Type);
                                if (view != null && (view.Equals("kanban")))
                                {
                                    //Notthing
                                }
                                else
                                {
                                    x.StatusHtml = x.Type == "group"? string.Empty: TaskInDueDate.HtmlStatus(strHtmlStatus, x.StatusName,"", x.PercentFinish);
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
            }
            catch (Exception ex)
            {
                bResult = false;
                strMsg = ex.Message.ToString();
                _loggerServices.WriteError(ex.Message.ToString());
            }
            return Json(new { status = bResult, msg = strMsg, data = pagingData });
        }
        private int getStatusId(string statusCode, string type)
        {
            int iStatusId = 0;
            if (type.Equals("project"))
            {
                switch (statusCode)
                {
                    case "DANGXULY":
                        iStatusId = 1;
                        break;
                    case "KETTHUC":
                        iStatusId = 4;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (statusCode)
                {
                    case "NEW":
                    case "MOI":
                        iStatusId = 0;
                        break;
                    case "INPROCESS":
                    case "DANGXULY":
                        iStatusId = 1;
                        break;
                    case "REPORT":
                        iStatusId = 2;
                        break;
                    case "FINISH":
                    case "KETTHUC":
                        iStatusId = 4;
                        break;
                    default:
                        break;
                }
            }
            return iStatusId;
        }
        private string getPrimaryNameByViewKanBan(IEnumerable<UserDepartmentDto> userDepartments, string strPrimary, string type)
        {
            string strReturn = "";
            List<UserProcessViewDto> lstView = new List<UserProcessViewDto>();
            StringBuilder strBuilder = new StringBuilder();
            int iIndex = 0;
            try
            {
                if (strPrimary != null && strPrimary.Length > 0)
                {
                    Guid[] values = strPrimary.Split(';').Select(s => Guid.Parse(s.ToString())).ToArray();
                    var primary = userDepartments.Where(x => values.Contains(x.UserID)).ToList();
                    if (primary != null && primary.Count > 0)
                    {
                        foreach (UserDepartmentDto item in primary)
                        {
                            iIndex++;
                            lstView.Add(new UserProcessViewDto() { ViewType = 1, Name = item.FullName, JobTitle = item.JobTitleName, UserId = item.Id, STT = iIndex, DeptName = item.DeptName, SrcImage = item.UserID.ToString() });
                        }
                    }
                }
                if (lstView.Count == 0)
                {
                    switch (type)
                    {
                        case "project":
                            strReturn = ResourceManagement.GetResourceText("Task.Home.ViewTypeProject", "Dự án", "Project");
                            break;
                        case "group":
                            strReturn = ResourceManagement.GetResourceText("Task.Home.ViewTypeGroup", "Nhóm công việc", "Group task");
                            break;
                        default:
                            ResourceManagement.GetResourceText("Task.Home.ViewTypeTask", "Công việc", "Task");
                            break;
                    }
                }
                else
                {
                    strReturn = string.Join("; ", lstView.Select(x => x.Name).ToList());
                }

            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.Message);
            }
            return strReturn;
        }
        private string htmlUserProcess(IEnumerable<UserDepartmentDto> userDepartments, string strPrimary, string strSecond, string strThird, string url, int iShow = 2)
        {
            int iIndex = 0;
            string imageBase64 = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAD4AAAA+CAIAAAD8oz8TAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAyJpVFh0WE1MOmNvbS5hZG9iZS54bXAAAAAAADw/eHBhY2tldCBiZWdpbj0i77u/IiBpZD0iVzVNME1wQ2VoaUh6cmVTek5UY3prYzlkIj8+IDx4OnhtcG1ldGEgeG1sbnM6eD0iYWRvYmU6bnM6bWV0YS8iIHg6eG1wdGs9IkFkb2JlIFhNUCBDb3JlIDUuMy1jMDExIDY2LjE0NTY2MSwgMjAxMi8wMi8wNi0xNDo1NjoyNyAgICAgICAgIj4gPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4gPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9IiIgeG1sbnM6eG1wPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvIiB4bWxuczp4bXBNTT0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL21tLyIgeG1sbnM6c3RSZWY9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZVJlZiMiIHhtcDpDcmVhdG9yVG9vbD0iQWRvYmUgUGhvdG9zaG9wIENTNiAoV2luZG93cykiIHhtcE1NOkluc3RhbmNlSUQ9InhtcC5paWQ6QTdEMjFGNzg1MUFDMTFFN0I5QzQ4OTY4NzcwMDc5RUQiIHhtcE1NOkRvY3VtZW50SUQ9InhtcC5kaWQ6QTdEMjFGNzk1MUFDMTFFN0I5QzQ4OTY4NzcwMDc5RUQiPiA8eG1wTU06RGVyaXZlZEZyb20gc3RSZWY6aW5zdGFuY2VJRD0ieG1wLmlpZDpBN0QyMUY3NjUxQUMxMUU3QjlDNDg5Njg3NzAwNzlFRCIgc3RSZWY6ZG9jdW1lbnRJRD0ieG1wLmRpZDpBN0QyMUY3NzUxQUMxMUU3QjlDNDg5Njg3NzAwNzlFRCIvPiA8L3JkZjpEZXNjcmlwdGlvbj4gPC9yZGY6UkRGPiA8L3g6eG1wbWV0YT4gPD94cGFja2V0IGVuZD0iciI/PuPygQIAAATjSURBVHjazFrbVloxEIUxVSuIooD//2c+uxBdauVSK90lbUgnyWSSE9F5cFE4SXZm9lxP+7e3t72YbLfbfr/vf6iW1A7yEVkAlDrPPe3WQypA+1uljtAAiEBXAurvxKHxPzCg/geluVI2iZ7ifqVSMgjKYHcT7KA/SLAY9VqLs08UruZ6SiFBK00OEDSn3D/1GLmfnUtpmCoTsSMZlDcnhzjr0Wy7jkGzLmT5+qLq4/2FfmxhgFIMjLqEZq3jBQnBLsvIkFohIFk18mVkrRl3CbdMYAL7KZrttv+EPRlFJhOP7cBwmlAxbDv/n3K6/pMmiL7txBiDz/j+/f39507e3t7swymzpDJ/6nSj8TZBN/YnCLAOh8Ozs7Pj4+PwSVxgvV7/2Ak+swvIdk4Z36QU6W8XfsNKlNFodHFxcXR0lEwfRN93gicfHx9fX1+ZYWWTRm1i5LSX3RrcmEwmp6enyrgEm8xms+fn58VikdKFgIFzXV/Q+Y4Cu5+cnAAHqFIaWM/Pz7Hq7u4u6vdlhUCYmFJB0FkN+q7DbQXkwfJsCBZiNwn5RW4dwJNq3A79eDyG9VJsjLYN+6JX0xn4vmL/wuJ6fgsC58Y+0YooFdD2RW9RIWHjDJSNI1sVydGtNBUOlXoJdhwMBkIcrKANwo4N9nJRwC5DRbWyZTnyTtvWBLrQ21wLPbw62IKY2LaxAt0ZLA0eErrjaIRCTOw42wgFe1YwkIR4Ev2mY0BM9shEpe2fqq1OFQhtobtqOXpQMiVVNEQNxc/lmtr9b5fkB9FUhVk3SCkSm1OVU4l95Sg31CzcomlojvvXToSCMUzzvBDQVPqA7h/TRDabDfaUB09hb0VypRYuwBnL5bIt9OyGUdpQ6XwV36NJa8sWNE2hF2ZDGZXO07AjlLRarVpBR8eEjlueR7DSNQk9e12sf3h4aBJq4DlPT09+PpLnqX69QKVDSjuxgNbRHXcP5/f391Gn16iPNMMtP+rbewM9oMPWXaADN1TgqzyM6CE81+OT4I6pWY9bjLPr0APQfD7HWkZizRhiX011KTysxdfr9eXlpb4sw/OLxQL6ludq2omATHTBMnjy5eUF+lN6LRI+ngf6imKOHWGUHUY4i7KMR2eJFhsFt1ZVRNfX16PRyF7Y7+uyI0QW+40wyJVXos0bj8doK+t6C6wdDofwdZvg9BNqB8bIyg4vAz2ho5lMJlB29+ZoOp2irQb1bQ1ThN4I09DwMsANNQN3w/YUuseeCDhwgHD8KxCB9GNi4Eb/e3Nz07ytBnRsC/X7VXs28lCWZ92HoxoBCbE5Q58PjtmhgOUJtm44OYoGH1AfCrLoVW21TBUbBLsPR5W6B3r81bxrITbg899guZctV1dXzfkthB0Efk164lwP38UNBoPucbBI7ImgjdDm7xs81gg6O8B2yB29g4umKKJo/Hb1PvK8Psm3JT3Qy3N3kml3YKqwVAUHy6ekKKVQJIWjwIOJfacphMhk0QuqKcfeH+qvyCf5/w/DoKMw/NAEpFQ8YCShhw2opdenq9wpPkVactWZX+7ATAfLQdnKLMqZfUpi3wL3R8zR6wQ1WTRKRgYJAN3knWgrsW+aGKv/G5f6/+/hi7DFpRdXkPmsjkQYhMVPjy0sswJSSHfD/NeW5l+H6M5ZN5sNg/pbgAEAqY268+gGhSIAAAAASUVORK5CYII=";
            List<UserProcessViewDto> lstView = new List<UserProcessViewDto>();
            StringBuilder strBuilder = new StringBuilder();
            try
            {
                if (strPrimary != null && strPrimary.Length > 0)
                {
                    Guid[] values = strPrimary.Split(';').Select(s => Guid.Parse(s.ToString())).ToArray();
                    var primary = userDepartments.Where(x => values.Contains(x.UserID)).OrderBy(e=>e.JobTitleOrderNumber).ThenBy(e=>e.FullName).ToList();
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
        [Route("MoveDataByTask")]
        [HttpPost]
        public async Task<JsonResult> MoveDataByTask(MoveFilterModel filter)
        {
            bool bResult = false;
            string strMsg = "";
            string parentIdRefresh = "";
            var returnData = new MoveItemDto();
            try
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                foreach (PropertyInfo propertyInfo in filter.GetType().GetProperties())
                {
                    string name = propertyInfo.Name;
                    string value = propertyInfo.GetValue(filter, null).ToString();
                    if (!string.IsNullOrEmpty(value))
                    {
                        dic.Add(string.Concat("@", name), value);
                    }
                    // do stuff here

                }
                if (dic != null && dic.Count > 0)
                {
                    dic.Add("@CurrentUserId", CurrentUser.Id.ToString());
                    returnData = await _projectService.GetMoveDataByTask(dic);
                    if (returnData != null)
                    {
                        switch (returnData.Message)
                        {
                            case "Error":
                                strMsg = ResourceManagement.GetResourceText("Task.Home.MoveError", "Có lỗi trong quá trình xử lý hệ thống", "System processing error");
                                break;
                            case "Permission":
                                strMsg = ResourceManagement.GetResourceText("Task.Home.MoveNoPermission", "Không có quyền thực hiện thao tác này", "No permission");
                                break;
                            case "Success":
                                strMsg = ResourceManagement.GetResourceText("Task.Home.MoveSuccess", "Di chuyển thành công", "Move success");
                                bResult = true;
                                parentIdRefresh = returnData.ParentId;
                                break;
                            default:
                                strMsg = ResourceManagement.GetResourceText("Task.Home.MoveSuccess", "Di chuyển thành công", "Move success");
                                bResult = true;
                                parentIdRefresh = returnData.ParentId;
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.Message);
            }
            return Json(new { status = bResult, msg = strMsg, parentId = parentIdRefresh });
        }
        [Route("GetViewBreadCrumbWithParent")]
        [HttpPost]
        public async Task<JsonResult> GetViewBreadCrumbWithParent(Guid? parentId)
        {
            bool bResult = false;
            string strMsg = "";
            List<TrackingBreadCrumbViewParentDTO> breadCrumb = new List<TrackingBreadCrumbViewParentDTO>();
            try
            {
                try
                {
                    if (parentId.HasValue)
                    {
                        breadCrumb = await _projectService.GetViewBreadCrumbWithParent(parentId.Value);
                    }
                    bResult = true;
                }
                catch (Exception ex)
                {
                    _loggerServices.WriteError(ex.Message);
                    strMsg = ex.Message;
                }
                //  .OrderBy(x => x.STT).ToList();
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.Message);
            }
            return Json(new { status = bResult, msg = strMsg, data = breadCrumb.OrderBy(x => x.STT).ToList() });
        }
        [Route("GetDataByTitleTable")]
        [HttpPost]
        public async Task<JsonResult> GetDataByTitleTable(string view)
        {
            bool bResult = false;
            string strMsg = "";
            List<TitleTableViewModel> returnData = new List<TitleTableViewModel>();
            try
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("Name", ResourceManagement.GetResourceText("Task.Home.TitleTableName", "Nội dung", "Name"));
                dic.Add("StatusName", ResourceManagement.GetResourceText("Task.Home.TitleTableStatus", "Trạng thái", "Status"));
                dic.Add("FromDateFormat", ResourceManagement.GetResourceText("Task.Home.TitleTableFromDate", "Từ ngày", "From date"));
                dic.Add("ToDateFormat", ResourceManagement.GetResourceText("Task.Home.TitleTableToDate", "Đến ngày", "To date"));
                dic.Add("FullName", ResourceManagement.GetResourceText("Task.Home.TitleTableUserAssign", "Người xử lý", "Assign user"));
                dic.Add("ProcessHtml", ResourceManagement.GetResourceText("Task.Home.TitleTableProcessFinish", "Tiến độ", "Process finish"));
                foreach (KeyValuePair<string, string> item in dic)
                {
                    TitleTableViewModel add = new TitleTableViewModel();
                    add.field = item.Key;
                    add.displayName = item.Value;
                    if (!item.Key.Equals("Name"))
                    {
                        add.visible = true;
                    }
                    if (view != null && view.Equals("grantt") && item.Key.Equals("ProcessHtml"))
                    {
                        add.typeHtml = true;
                    }
                    returnData.Add(add);
                }
                bResult = true;
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.Message);
                strMsg = ex.Message;
            }
            return Json(new { status = bResult, msg = strMsg, data = returnData });
        }
        public async Task<List<ViewGranttModel>> ReportOnepageQuaterlyAsync(IEnumerable<FetchProjectsTasksResult> taskItem, AdvanceFilterModel filter)
        {
            var userDepartments = _userDepartmentServices.GetCachedUserDepartmentDtos();
            List<ViewGranttModel> userReportResults = new List<ViewGranttModel>();
            try
            {
                DateTime baseDate = filter.FromDate ?? DateTime.Now;
                var today = baseDate;
                //Tuan
                int dayOfWeek = (int)baseDate.DayOfWeek == 0 ? 6 : (int)baseDate.DayOfWeek - 1;
                var thisWeekStart = baseDate.AddDays(-dayOfWeek);
                var thisWeekEnd = thisWeekStart.AddDays(7).AddSeconds(-1);
                var nextWeekStart = thisWeekStart.AddDays(7);
                var nextWeekEnd = thisWeekEnd.AddDays(7);
                var fromDateW = thisWeekStart;
                var toDateW = thisWeekEnd;
                //Month
                var thisMonthStart = new DateTime(today.Year, today.Month, 1);
                var thisMonthEnd = thisMonthStart.AddDays(32).AddSeconds(-1);
                var fromDateM = thisMonthStart;
                var toDateM = thisMonthEnd;
                //Quy
                int quarterNumber = (today.Month - 1) / 3 + 1;
                DateTime firstDayOfQuarter = new DateTime(today.Year, (quarterNumber - 1) * 3 + 1, 1);
                DateTime lastDayOfQuarter = firstDayOfQuarter.AddMonths(3).AddDays(-1);
                var fromDateQ = firstDayOfQuarter;
                var toDateQ = lastDayOfQuarter;
                //Nam
                DateTime firstDayOfYear = new DateTime(today.Year, 1, 1);
                DateTime lastDayOfYear = new DateTime(today.Year, 12, 31);
                var fromDateY = firstDayOfYear;
                var toDateY = lastDayOfYear;

                foreach (var task in taskItem)
                {
                    task.FromDateFormat = task.FromDate?.ToString("dd/MM/yyyy");
                    task.ToDateFormat = task.ToDate?.ToString("dd/MM/yyyy");
                    if (task.Type == "project")
                    {
                        task.UserId = task.AssignBy;
                    }

                    if (string.IsNullOrEmpty(task.Type))
                    {
                        task.Type = task.HasChildren == true ? "tasks" : "task";
                    }
                    if (task.UserId != null)
                    {
                        var userDeparment = userDepartments.Result.Where(y => ((y.UserID == task.UserId && task.Type != "project") || (y.UserID == task.ApprovedBy && task.Type == "project"))).FirstOrDefault();
                        task.FullName = userDeparment?.FullName;
                        task.JobTitle = userDeparment?.JobTitleName;
                    }
                    if (task.Type != "project" && task.Type != "group")
                    {
                        #region Tuan
                        List<string> week = new List<string>();
                        for (DateTime i = fromDateW.Date.AddDays(-1); i < fromDateW.Date.AddDays(8); i = i.AddDays(1))
                        {
                            string checkInWeek = "";
                            if (task.FromDate.HasValue && task.ToDate.HasValue && task.FromDate.Value.Date <= i && task.ToDate.Value.Date >= i)
                            {
                                checkInWeek = task.Process;
                            }
                            week.Add(checkInWeek);
                        }
                        #endregion
                        #region Thang
                        List<string> month = new List<string>();
                        for (DateTime i = fromDateM.AddDays(-1); i <= toDateM.AddDays(1); i = i.AddDays(1))
                        {
                            string checkInWeek = "";
                            if (task.FromDate.HasValue && task.ToDate.HasValue && task.FromDate.Value.Date <= i && task.ToDate.Value.Date >= i)
                            {
                                //if (task.IsLate)
                                //{
                                //    checkInWeek = task.Process;
                                //}
                                //else
                                //{
                                //    checkInWeek = "#49a942";
                                //}
                                checkInWeek = task.Process;
                            }
                            month.Add(checkInWeek);
                        }

                        #endregion
                        #region Quy
                        List<string> quater = new List<string>();
                        var beforeQ = fromDateQ.Date.AddDays(-1);
                        string checkBeforeQ = "";
                        //string color = task.IsLate ? "#e7505a" : "#49a942";
                        if (task.ToDate >= beforeQ && task.FromDate < beforeQ)
                        {
                            checkBeforeQ = task.Process;
                        }
                        quater.Add(checkBeforeQ);
                        for (DateTime i = fromDateQ.Date; i < toDateQ.Date; i = i.AddMonths(1))
                        {
                            DateTime endI = i.AddMonths(1).AddDays(-1);
                            string checkInQuater = "";
                            if (!(task.ToDate < i || task.FromDate > endI))
                            {
                                checkInQuater = task.Process;
                            }
                            quater.Add(checkInQuater);
                        }
                        var afterQ = toDateQ.Date.AddDays(-1);
                        string checkAfterQ = "";
                        if (task.ToDate >= afterQ && task.FromDate < afterQ)
                        {
                            checkAfterQ = task.Process;
                        }
                        quater.Add(checkAfterQ);
                        #endregion
                        #region Nam
                        List<string> year = new List<string>();
                        var beforeY = fromDateY.Date.AddDays(-1);
                        string checkBeforeY = "";
                        //string colorY = task.IsLate ? "#e7505a" : "#49a942";
                        if (task.ToDate >= beforeY && task.FromDate < beforeY)
                        {
                            checkBeforeY = task.Process;
                        }
                        year.Add(checkBeforeY);
                        for (DateTime i = fromDateY.Date; i < toDateY.Date; i = i.AddMonths(1))
                        {
                            DateTime endI = i.AddMonths(1).AddDays(-1);
                            string checkInY = "";
                            if (!(task.ToDate < i || task.FromDate > endI))
                            {
                                checkInY = task.Process;
                            }
                            year.Add(checkInY);
                        }
                        var afterY = toDateY.Date.AddDays(-1);
                        string checkAfterY = "";
                        if (task.ToDate >= afterY && task.FromDate < afterY)
                        {
                            checkAfterY = task.Process;
                        }
                        year.Add(checkAfterY);
                        #endregion
                        ViewGranttModel report = new ViewGranttModel
                        {
                            Id = task.Id,
                            Name = task.Name,
                            ParentId = task.ParentId,
                            ProjectId = task.ProjectId,
                            StatusName = task.StatusName,
                            FromDate = task.FromDate,
                            ToDate = task.ToDate,
                            ToDateFormat = task.ToDateFormat,
                            FromDateFormat = task.FromDateFormat,
                            CurrentPage = task.CurrentPage,
                            TotalRecord = task.TotalRecord,
                            PageSize = task.PageSize,
                            HasChildren = task.HasChildren,
                            Type = task.Type,
                            //Process = task.Process,
                            FullName = task.FullName,
                            JobTitle = task.JobTitle,
                            #region Week
                            DayBeforeW = week[0],
                            T2 = week[1],
                            T3 = week[2],
                            T4 = week[3],
                            T5 = week[4],
                            T6 = week[5],
                            T7 = week[6],
                            CN = week[7],
                            DayAfterW = week[8],
                            #endregion
                            #region Month
                            DayBeforeM = month[0],
                            D1 = month[1],
                            D2 = month[2],
                            D3 = month[3],
                            D4 = month[4],
                            D5 = month[5],
                            D6 = month[6],
                            D7 = month[7],
                            D8 = month[8],
                            D9 = month[9],
                            D10 = month[10],
                            D11 = month[11],
                            D12 = month[12],
                            D13 = month[13],
                            D14 = month[14],
                            D15 = month[15],
                            D16 = month[16],
                            D17 = month[17],
                            D18 = month[18],
                            D19 = month[19],
                            D20 = month[20],
                            D21 = month[21],
                            D22 = month[22],
                            D23 = month[23],
                            D24 = month[24],
                            D25 = month[25],
                            D26 = month[26],
                            D27 = month[27],
                            D28 = month[28],
                            D29 = month[29],
                            D30 = month[30],
                            D31 = month[31],
                            DayAfterM = month[32],
                            #endregion
                            #region Quater
                            DayBeforeQ = quater[0],
                            Q1 = quater[1],
                            Q2 = quater[2],
                            Q3 = quater[3],
                            DayAfterQ = quater[4],
                            #endregion
                            #region Nam
                            DayBeforeY = year[0],
                            Th1 = year[1],
                            Th2 = year[2],
                            Th3 = year[3],
                            Th4 = year[4],
                            Th5 = year[5],
                            Th6 = year[6],
                            Th7 = year[7],
                            Th8 = year[8],
                            Th9 = year[9],
                            Th10 = year[10],
                            Th11 = year[11],
                            Th12 = year[12],
                            DayAfterY = year[13],
                            #endregion
                        };
                        userReportResults.Add(report);
                    }
                    else
                    {
                        ViewGranttModel report = new ViewGranttModel
                        {
                            Id = task.Id,
                            Name = task.Name,
                            ParentId = task.ParentId,
                            ProjectId = task.ProjectId,
                            StatusName = task.StatusName,
                            FromDate = task.FromDate,
                            ToDate = task.ToDate,
                            ToDateFormat = task.ToDateFormat,
                            FromDateFormat = task.FromDateFormat,
                            CurrentPage = task.CurrentPage,
                            TotalRecord = task.TotalRecord,
                            PageSize = task.PageSize,
                            HasChildren = task.HasChildren,
                            Type = task.Type,
                            //Process=task.Process,
                            FullName = task.FullName,
                            JobTitle = task.JobTitle
                        };
                        userReportResults.Add(report);
                    }

                }
            }
            catch (Exception ex)
            {

                _loggerServices.WriteError(ex.Message);
            }

            return userReportResults;
        }

        internal static DateTime FirstWeekDate(DateTime CurrentDate)
        {
            try
            {
                DateTime FirstDayOfWeek = CurrentDate;
                DayOfWeek WeekDay = FirstDayOfWeek.DayOfWeek;
                switch (WeekDay)
                {
                    case DayOfWeek.Sunday:
                        break;
                    case DayOfWeek.Monday:
                        FirstDayOfWeek = FirstDayOfWeek.AddDays(-1);
                        break;
                    case DayOfWeek.Tuesday:
                        FirstDayOfWeek = FirstDayOfWeek.AddDays(-2);
                        break;
                    case DayOfWeek.Wednesday:
                        FirstDayOfWeek = FirstDayOfWeek.AddDays(-3);
                        break;
                    case DayOfWeek.Thursday:
                        FirstDayOfWeek = FirstDayOfWeek.AddDays(-4);
                        break;
                    case DayOfWeek.Friday:
                        FirstDayOfWeek = FirstDayOfWeek.AddDays(-5);
                        break;
                    case DayOfWeek.Saturday:
                        FirstDayOfWeek = FirstDayOfWeek.AddDays(-6);
                        break;
                }
                return (FirstDayOfWeek);
            }
            catch
            {
                return DateTime.Now;
            }
        }
        //[Route("getHeaderView")]
        //[HttpPost]
        //public async Task<JsonResult> getHeaderView( string view)
        //{

        //    return userReportResults;
        //}

        /// <summary>
        /// Lấy cây danh mục filter
        /// </summary>
        /// <returns></returns>
        [Route("GetAdvanceFilterTree")]
        [HttpGet]
        public JsonResult GetAdvanceFilterTree(Guid parentID = default(Guid), string keySearch = null)
        {
            var dataModel = _projectFilterParamService.GetTreeFilterByParentId(CurrentUser.Id, parentID, keySearch);

            return Json(dataModel, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Lấy filter để chỉnh sửa
        /// </summary>
        /// <returns></returns>
        [Route("GetAdvanceFilter")]
        [HttpGet]
        public JsonResult GetAdvanceFilter(Guid Id)
        {
            return null;
        }
        /// <summary>
        /// Lấy quyển tạo filter của user
        /// </summary>
        /// <returns></returns>
        [Route("GetAdvanceFilterPermission")]
        [HttpGet]
        public JsonResult GetAdvanceFilterPermission()
        {
            return null;
        }
        /// <summary>
        /// Lấy danh mục theo tên bảng
        /// </summary>
        /// <returns></returns>
        [Route("GetCategory")]
        [HttpGet]
        public JsonResult GetCategory(string tableName)
        {
            return null;
        }
        /// <summary>
        /// Lưu filter advance
        /// </summary>
        /// <returns></returns>
        [Route("SaveAdvanceFilter")]
        [HttpPost]
        public JsonResult SaveAdvanceFilter(ProjectFilterParamModel model)
        {
            return null;
        }
        /// <summary>
        /// Xóa filter advance
        /// </summary>
        /// <returns></returns>
        [Route("DeleteAdvanceFilter")]
        [HttpPost]
        public JsonResult DeleteAdvanceFilter(ProjectFilterParamModel model)
        {
            return null;
        }
        [Route("UserInfoHeader")]
        public ActionResult UserInfoHeader()
        {
            var currentUser = CurrentUser;
            return PartialView(currentUser);
        }
        [Route("AccessDenied")]
        public ActionResult AccessDenied()
        {
            return View();
        }
        [Route("NotFound")]
        public ActionResult NotFound()
        {
            return View();
        }
        #region Contanst
        public const string ControllerName = "Home";
        public const string IndexAction = "Index";
        public const string ActionViewTaskInMain = "ViewTaskInMain";
        public const string PartialViewTaskInMain = "_ViewTaskInMain";
        //public const string ActionViewTaskInTable = "ViewTaskInTable";
        //public const string ActionViewTaskInGrantt = "ViewTaskInGrantt";
        //public const string ActionViewTaskInKaban = "ViewTaskInKaban";
        //public const string ActionViewTaskInKanban = "ViewTaskInKanban";
        //public const string ActionViewTaskInCalendar = "ViewTaskInCalendar";
        public const string TaskViewType = "TaskViewType";
        public const string NotFoundAction = "NotFound";
        public const string AccessDeniedAction = "AccessDenied";
        public const string ViewTable = "table";
        public const string ViewGrantt = "grantt";
        public const string ViewKanban = "kanban";
        public const string ViewCalendar = "calendar";
        #endregion
    }
}