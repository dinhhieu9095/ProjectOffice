using AutoMapper;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using DaiPhatDat.Core.Kernel.Controllers;
using DaiPhatDat.Core.Kernel.Helper;
using DaiPhatDat.Core.Kernel.Logger.Application;
using DaiPhatDat.Core.Kernel.Models;
using DaiPhatDat.Core.Kernel.Orgs.Application;
using DaiPhatDat.Module.Task.Entities;
using DaiPhatDat.Module.Task.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DaiPhatDat.Module.Task.Web
{

    [Authorize]
    public class ReportController : CoreController
    {
        public ReportController(ILoggerServices loggerServices, IUserServices userService, IUserDepartmentServices userDepartmentServices, IReportService ReportService, IMapper mapper, ICategoryService categoryService, IDepartmentServices departmentServices, IOrgService orgService, IProjectService projectService) : base(loggerServices, userService, userDepartmentServices)
        {
            _ReportService = ReportService;
            _mapper = mapper;
            _categoryService = categoryService;
            _orgService = orgService;
            _departmentServices = departmentServices;
            _projectService = projectService;
        }

        #region Properties
        private IReportService _ReportService;
        private ICategoryService _categoryService;
        private IDepartmentServices _departmentServices;
        private IOrgService _orgService;
        private IProjectService _projectService;
        private readonly IMapper _mapper;
        private static readonly IDictionary<string, byte[]> TemporaryData = new Dictionary<string, byte[]>();
        #endregion

        public ActionResult Index()
        {
            ViewBag.IsAdmin = CurrentUser.HavePermission((EnumModulePermission.Task_FullControl));
            return View();
        }
        public ActionResult UserReviewReport()
        {
            return View();
        }

        public ActionResult Edit(ReportModel filter)
        {
            return View();
        }

        public ActionResult ViewDetail(ReportModel filter)
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> GetPaging(string keyword, int pageIndex = 1, int pageSize = 20)
        {
            var response = new Pagination<ReportModel>(0, new List<ReportModel>());
            try
            {
                bool isAdmin = CurrentUser.HavePermission((EnumModulePermission.Task_FullControl));
                var data = await _ReportService.GetPaginAsync(keyword, pageIndex, pageSize, CurrentUser.Id, isAdmin);

                var result = _mapper.Map<List<ReportModel>>(data.Result);
                response.Result = result;
                response.Count = data.Count;
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> PostEdit(ReportModel model)
        {
            var response = new ReportModel();
            try
            {
                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];
                    model.FileName = file.FileName;
                    byte[] fileBytes = new byte[file.ContentLength];
                    System.IO.Stream myStream = file.InputStream;
                    myStream.Read(fileBytes, 0, file.ContentLength);
                    model.FileContent = fileBytes;
                }
                bool isSuccess = await _ReportService.UpdateAsync(_mapper.Map<ReportDto>(model));
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> GetById(ReportModel model)
        {
            var response = new ReportModel();
            try
            {
                if (model.Id > 0)
                {
                    var data = await _ReportService.GetAsync(model.Id);
                    response = _mapper.Map<ReportModel>(data);
                }

            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public async Task<System.Web.Mvc.FileResult> Download(int Id)
        {
            var data = await _ReportService.GetFileAsync(Id);
            return File(data.FileContent, data.FileName.Split('.').LastOrDefault(), data.FileName);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(ReportModel model)
        {
            try
            {
                bool isSuccess = await _ReportService.DeleteAsync(_mapper.Map<ReportDto>(model));
                if (!isSuccess)
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.Message);
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }



        [HttpPost]
        public async Task<ActionResult> PostReportWeekly(ReportFilterModel reportFilterModel)
        {
            var result = new ReportResultModel();
            try
            {
                if (reportFilterModel.FromDate == null ||
                reportFilterModel.ToDate == null)
                    throw new ArgumentNullException(
                        "Invalid arguments: " +
                        $"{nameof(reportFilterModel.FromDate)}, " +
                        $"{nameof(reportFilterModel.ToDate)}");

                var reportFilterDto = _mapper.Map<ReportFilterDto>(reportFilterModel);

                var reportResultDtos = await _ReportService.ProjectReportWeeklyAsync(reportFilterDto);
                var reportResultModel = _mapper.Map<List<ReportResultModel>>(reportResultDtos);

                if (!string.IsNullOrEmpty(reportFilterModel.Action) &&
                    reportFilterModel.Action.ToLower() == "exportexcel")
                {
                    var report = await _ReportService.GetAsyncByCode("ReportWeekly");
                    var stream = new MemoryStream(report.FileContent);
                    using (var package = new ExcelPackage(stream))
                    {

                        EplusExtension eplusExtension = new EplusExtension();
                        if (reportResultModel.Any())
                        {
                            var worksheet = package.Workbook.Worksheets.First();

                            var fromDateCell =
                                (from cell
                                        in worksheet.Cells
                                 where !string.IsNullOrEmpty(cell.Text) && cell.Text.Contains("{{ fromDate }}")
                                 select cell).FirstOrDefault();

                            if (fromDateCell != null)
                                worksheet.Cells[fromDateCell.Address].Value =
                                    worksheet.Cells[fromDateCell.Address].Text.Replace(
                                        "{{ fromDate }}",
                                        reportFilterModel.FromDate.Value.ToString("dd/MM/yyyy"));

                            var toDateCell =
                                (from cell
                                        in worksheet.Cells
                                 where !string.IsNullOrEmpty(cell.Text) && cell.Text.Contains("{{ toDate }}")
                                 select cell).FirstOrDefault();

                            if (toDateCell != null)
                                worksheet.Cells[toDateCell.Address].Value =
                                    worksheet.Cells[toDateCell.Address].Text.Replace(
                                        "{{ toDate }}",
                                        reportFilterModel.ToDate.Value.ToString("dd/MM/yyyy"));

                            var userDepartmentCell =
                                (from cell
                                        in worksheet.Cells
                                 where !string.IsNullOrEmpty(cell.Text) && cell.Text.Contains("{{ userName }}")
                                 select cell).FirstOrDefault();

                            if (userDepartmentCell != null)
                                worksheet.Cells[userDepartmentCell.Address].Value =
                                    worksheet.Cells[userDepartmentCell.Address].Text.Replace(
                                        "{{ userName }}",
                                        reportFilterModel.UserDepartmentText);

                            var trackingProgressCell =
                                (from cell
                                        in worksheet.Cells
                                 where !string.IsNullOrEmpty(cell.Text) && cell.Text.Contains("{{ trackingProgress }}")
                                 select cell).FirstOrDefault();

                            if (trackingProgressCell != null)
                                worksheet.Cells[trackingProgressCell.Address].Value =
                                    worksheet.Cells[trackingProgressCell.Address].Text.Replace(
                                        "{{ trackingProgress }}",
                                        reportFilterModel.TrackingProgress == -1
                                            ? "Tất cả"
                                            : reportFilterModel.TrackingProgress == 0
                                                ? "Trong hạn"
                                                : "Quá hạn");

                            var trackingStatusCell =
                                (from cell
                                        in worksheet.Cells
                                 where !string.IsNullOrEmpty(cell.Text) && cell.Text.Contains("{{ trackingStatus }}")
                                 select cell).FirstOrDefault();

                            if (trackingStatusCell != null)
                                worksheet.Cells[trackingStatusCell.Address].Value =
                                    worksheet.Cells[trackingStatusCell.Address].Text.Replace(
                                        "{{ trackingStatus }}",
                                        reportFilterModel.TrackingStatus == -1
                                            ? "Tất cả"
                                            : reportFilterModel.TrackingStatus == 0
                                                ? "Mới"
                                                : reportFilterModel.TrackingStatus == 1
                                                ? "Đang xử lý"
                                                : reportFilterModel.TrackingStatus == 2
                                                ? "Báo cáo"
                                                : reportFilterModel.TrackingStatus == 3
                                                ? "Trả lại"
                                                : reportFilterModel.TrackingStatus == 4
                                                ? "Kết thúc"
                                                : reportFilterModel.TrackingStatus == 5
                                                ? "Gia hạn"
                                                : reportFilterModel.TrackingStatus == 6
                                                ? "Báo cáo trả lại"
                                                : "Đã xem");

                            var trackingCrucialCell =
                                (from cell
                                        in worksheet.Cells
                                 where !string.IsNullOrEmpty(cell.Text) && cell.Text.Contains("{{ trackingCrucial }}")
                                 select cell).FirstOrDefault();

                            if (trackingCrucialCell != null)
                                worksheet.Cells[trackingCrucialCell.Address].Value =
                                    worksheet.Cells[trackingCrucialCell.Address].Text.Replace(
                                        "{{ trackingCrucial }}",
                                        reportFilterModel.TrackingCrucial == -1
                                            ? "Tất cả"
                                            : reportFilterModel.TrackingCrucial == 0
                                                ? "Bình thường"
                                                : reportFilterModel.TrackingCrucial == 1
                                                ? "Quan trọng"
                                                : "Rất quan trọng");
                            var resultThisWeek = reportResultModel[0].Result;
                            if (resultThisWeek.Any())
                            {
                                eplusExtension.FillData(reportResultModel[0].Result, worksheet);
                            }
                        }

                        if (reportResultModel.Count() > 1)
                        {
                            var worksheetNextWeek = package.Workbook.Worksheets[1];

                            var fromDateCell =
                                (from cell
                                        in worksheetNextWeek.Cells
                                 where !string.IsNullOrEmpty(cell.Text) && cell.Text.Contains("{{ fromDate }}")
                                 select cell).FirstOrDefault();

                            if (fromDateCell != null)
                                worksheetNextWeek.Cells[fromDateCell.Address].Value =
                                    worksheetNextWeek.Cells[fromDateCell.Address].Text.Replace(
                                        "{{ fromDate }}",
                                        reportFilterModel.FromDate.Value.ToString("dd/MM/yyyy"));

                            var toDateCell =
                                (from cell
                                        in worksheetNextWeek.Cells
                                 where !string.IsNullOrEmpty(cell.Text) && cell.Text.Contains("{{ toDate }}")
                                 select cell).FirstOrDefault();

                            if (toDateCell != null)
                                worksheetNextWeek.Cells[toDateCell.Address].Value =
                                    worksheetNextWeek.Cells[toDateCell.Address].Text.Replace(
                                        "{{ toDate }}",
                                        reportFilterModel.ToDate.Value.ToString("dd/MM/yyyy"));

                            var userDepartmentCell =
                                (from cell
                                        in worksheetNextWeek.Cells
                                 where !string.IsNullOrEmpty(cell.Text) && cell.Text.Contains("{{ userName }}")
                                 select cell).FirstOrDefault();

                            if (userDepartmentCell != null)
                                worksheetNextWeek.Cells[userDepartmentCell.Address].Value =
                                    worksheetNextWeek.Cells[userDepartmentCell.Address].Text.Replace(
                                        "{{ userName }}",
                                        reportFilterModel.UserDepartmentText);

                            var trackingProgressCell =
                                (from cell
                                        in worksheetNextWeek.Cells
                                 where !string.IsNullOrEmpty(cell.Text) && cell.Text.Contains("{{ trackingProgress }}")
                                 select cell).FirstOrDefault();

                            if (trackingProgressCell != null)
                                worksheetNextWeek.Cells[trackingProgressCell.Address].Value =
                                    worksheetNextWeek.Cells[trackingProgressCell.Address].Text.Replace(
                                        "{{ trackingProgress }}",
                                        reportFilterModel.TrackingProgress == -1
                                            ? "Tất cả"
                                            : reportFilterModel.TrackingProgress == 0
                                                ? "Trong hạn"
                                                : "Quá hạn");

                            var trackingStatusCell =
                                (from cell
                                        in worksheetNextWeek.Cells
                                 where !string.IsNullOrEmpty(cell.Text) && cell.Text.Contains("{{ trackingStatus }}")
                                 select cell).FirstOrDefault();

                            if (trackingStatusCell != null)
                                worksheetNextWeek.Cells[trackingStatusCell.Address].Value =
                                    worksheetNextWeek.Cells[trackingStatusCell.Address].Text.Replace(
                                        "{{ trackingStatus }}",
                                        reportFilterModel.TrackingStatus == -1
                                            ? "Tất cả"
                                            : reportFilterModel.TrackingStatus == 0
                                                ? "Mới"
                                                : reportFilterModel.TrackingStatus == 1
                                                ? "Đang xử lý"
                                                : reportFilterModel.TrackingStatus == 2
                                                ? "Báo cáo"
                                                : reportFilterModel.TrackingStatus == 3
                                                ? "Trả lại"
                                                : reportFilterModel.TrackingStatus == 4
                                                ? "Kết thúc"
                                                : reportFilterModel.TrackingStatus == 5
                                                ? "Gia hạn"
                                                : reportFilterModel.TrackingStatus == 6
                                                ? "Báo cáo trả lại"
                                                : "Đã xem");

                            var trackingCrucialCell =
                                (from cell
                                        in worksheetNextWeek.Cells
                                 where !string.IsNullOrEmpty(cell.Text) && cell.Text.Contains("{{ trackingCrucial }}")
                                 select cell).FirstOrDefault();

                            if (trackingCrucialCell != null)
                                worksheetNextWeek.Cells[trackingCrucialCell.Address].Value =
                                    worksheetNextWeek.Cells[trackingCrucialCell.Address].Text.Replace(
                                        "{{ trackingCrucial }}",
                                        reportFilterModel.TrackingCrucial == -1
                                            ? "Tất cả"
                                            : reportFilterModel.TrackingCrucial == 0
                                                ? "Bình thường"
                                                : reportFilterModel.TrackingCrucial == 1
                                                ? "Quan trọng"
                                                : "Rất quan trọng");
                            if (reportResultModel.Any())
                            {
                                var resultNextWeek = reportResultModel[1].Result;
                                if (resultNextWeek.Any())
                                {
                                    eplusExtension.FillData(resultNextWeek, worksheetNextWeek);
                                }
                            }
                        }


                        var file = Guid.NewGuid().ToString();
                        var fileName = report.FileName;
                        TemporaryData[file] = package.GetAsByteArray();

                        var endpoint = Url.Action("ExportDepartmentReport", "Report", new
                        {
                            file,
                            fileName,
                            contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                        });

                        return Content(endpoint);
                    }
                }

                foreach (var item in reportResultModel[0].Result)
                {
                    if (item.ParentId == Guid.Empty.ToString())
                    {
                        item.ParentId = "tuan-hien-tai";
                    }
                }

                reportResultModel[0].Result.Add(new ReportItemModel() { Id = "tuan-hien-tai", Name = "Tuần hiện tại", ParentId = "#" });

                foreach (var item in reportResultModel[1].Result)
                {
                    if (item.ParentId == Guid.Empty.ToString())
                    {
                        item.ParentId = "tuan-tiep-theo";
                    }
                }
                reportResultModel[1].Result.Add(new ReportItemModel() { Id = "tuan-tiep-theo", Name = "Tuần tiếp theo", ParentId = "#" });
                reportResultModel[0].Result = GetHierarchyReportItems(reportResultModel[0].Result, "#");
                reportResultModel[1].Result = GetHierarchyReportItems(reportResultModel[1].Result, "#");

                result.Result = reportResultModel[0].Result;
                result.Result.AddRange(reportResultModel[1].Result);

            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.Message);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> ReportCommon(int Id, Guid? projectId, string type, string device = "")
        {
            var model = new ReportFilterModel();
            try
            {
                var report = await _ReportService.GetAsync(Id);
                if (report.Type == EnumReportType.ReportUser)
                {
                    type = "user";
                }
                ViewBag.Device = device;
                ViewBag.Type = type;
                ViewBag.Title = report.Name;
                ViewBag.Url = report.LinkDesktop;
                //var filter = await GetFilter(projectId, type, device);
                if (report.Type == EnumReportType.ReportDepartment || report.Type == EnumReportType.ReportUser)
                {
                    return View("ProjectReport");
                }
                else if (report.Type == EnumReportType.ReportWeekly)
                {
                    return View("ReportWeekly");
                }
                else if (report.Type == EnumReportType.ReportOnePage)
                {
                    return View("ReportOnePage");
                }
                else if ((report.Type == EnumReportType.ReportIframe))
                {
                    return View("ReportIframe");
                }    
            }
            catch (Exception ex)
            {
                _loggerServices.WriteDebug(ex.Message);
            }
            return null;
        }

        public async Task<ReportFilterModel> GetFilter(Guid? projectId, string type, string device = "")
        {
            var model = new ReportFilterModel();
            try
            {
                ViewBag.Device = device;
                ViewBag.Type = type;
                var user = _userService.GetById(CurrentUser.Id);

                if (type == "user")
                {
                    //var username = SharePointUtils.GetUserNameFromSharePoint(User.Identity.Name);
                    //var role = WFUserRoleService.GetCodeByUserID(user.Id);
                    //ViewBag.WFUserRolesCode = role;
                    //var userDepartments = _userDepartmentServices.GetUserDepartmentsByUser(CurrentUser.Id);
                    var firstNode = user.Departments.FirstOrDefault();
                    if (firstNode != null)
                    {
                        model.DepartmentId = firstNode.Id;

                        model.UserOfDepartmentId = user.Id;

                        model.UserDepartmentText = $"{user.FullName} {(string.IsNullOrEmpty(firstNode.JobTitle) ? "" : " (" + firstNode.JobTitle + ")")}";
                    }
                }
                else
                {
                    //var username = SharePointUtils.GetUserNameFromSharePoint(User.Identity.Name);
                    //var role = WFUserRoleService.GetCodeByUserID(user.Id);
                    //ViewBag.WFUserRolesCode = role;
                    var lstTreeUserDept = await _orgService.GetOrgUserTree();
                    var userDepartments = _userDepartmentServices.GetUserDepartmentsByUser(CurrentUser.Id);
                    var firstNode = user.Departments.FirstOrDefault();
                    if (firstNode != null)
                    {
                        model.DepartmentId = firstNode.Id;

                        //if (Guid.TryParse(firstNode.UserID, out var userOfDepartmentId))
                        //    model.UserOfDepartmentId = userOfDepartmentId;

                        model.UserDepartmentText = $"{firstNode.Name}";
                    }
                }

                var lstParams = new List<string>();
                var project = new FetchProjectsTasksResult
                {
                    ProjectId = null,
                    Name = "Tất cả"
                };
                model.Projects = new List<FetchProjectsTasksResult>() { project };
                var page = _projectService.GetTaskWithFilterPaging(
                                        null,
                                        lstParams,
                                        1,
                                        50,
                                        " CreatedDate DESC ",
                                        CurrentUser,
                                        true);
                model.Projects.AddRange(page.Result);
            }
            catch (Exception ex)
            {
                _loggerServices.WriteDebug(ex.Message);
            }
            return model;
        }

        [HttpGet]
        public async Task<ActionResult> ReportWeekly(Guid? projectId, string type, string device = "")
        {
            //var model = new ReportFilterModel();
            try
            {
                ViewBag.Device = device;
                ViewBag.Type = type;
                //model = await GetFilter(projectId, type, device);
            }
            catch (Exception ex)
            {
                _loggerServices.WriteDebug(ex.Message);
            }
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> ProjectReport(Guid? projectId, string type, string device = "")
        {
            //var model = new ReportFilterModel();
            try
            {
                ViewBag.Device = device;
                ViewBag.Type = type;
                var user = _userService.GetById(CurrentUser.Id);

                //model = await GetFilter(projectId, type, device);
            }
            catch (Exception ex)
            {
                _loggerServices.WriteDebug(ex.Message);
            }
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> FormSearch(int Id, Guid? projectId, string type, string device = "")
        {
            var model = new ReportFilterModel();
            try
            {
                model.FromDate = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Sunday);
                model.ToDate = model.FromDate.Value.AddDays(7);
                ViewBag.Device = device;
                var report = await _ReportService.GetAsync(Id);
                ViewBag.ReportCode = report.Type;
                ViewBag.Type = report.IsUser == true ? "user" : "";
                DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
                Calendar cal = dfi.Calendar;
                ViewBag.CurrentWeek = cal.GetWeekOfYear(DateTime.Now, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
                var user = _userService.GetById(CurrentUser.Id);
                if (report.IsUser)
                {
                    //var username = SharePointUtils.GetUserNameFromSharePoint(User.Identity.Name);
                    //var role = WFUserRoleService.GetCodeByUserID(user.Id);
                    //ViewBag.WFUserRolesCode = role;
                    //var userDepartments = _userDepartmentServices.GetUserDepartmentsByUser(CurrentUser.Id);
                    var firstNode = user.Departments.FirstOrDefault();
                    if (firstNode != null)
                    {
                        model.DepartmentId = firstNode.Id;

                        model.UserOfDepartmentId = user.Id;

                        model.UserDepartmentText = $"{user.FullName} {(string.IsNullOrEmpty(firstNode.JobTitle) ? "" : " (" + firstNode.JobTitle + ")")}";
                    }
                }
                else
                {
                    //var username = SharePointUtils.GetUserNameFromSharePoint(User.Identity.Name);
                    //var role = WFUserRoleService.GetCodeByUserID(user.Id);
                    //ViewBag.WFUserRolesCode = role;
                    var lstTreeUserDept = await _orgService.GetOrgUserTree();
                    var userDepartments = _userDepartmentServices.GetUserDepartmentsByUser(CurrentUser.Id);
                    var firstNode = user.Departments.FirstOrDefault();
                    if (firstNode != null)
                    {
                        model.DepartmentId = firstNode.Id;

                        //if (Guid.TryParse(firstNode.UserID, out var userOfDepartmentId))
                        //    model.UserOfDepartmentId = userOfDepartmentId;

                        model.UserDepartmentText = $"{firstNode.Name}";
                    }
                }

                var lstParams = new List<string>();
                lstParams.Add($"@ParentId:{null}");
                var project = new FetchProjectsTasksResult
                {
                    ProjectId = null,
                    Name = "Tất cả"
                };
                model.Projects = new List<FetchProjectsTasksResult>() { project };
                var page = _projectService.GetTaskWithFilterPaging(
                                        null,
                                        lstParams,
                                        1,
                                        15,
                                        " CreatedDate DESC ",
                                        user,
                                        true);
                model.Projects.AddRange(page.Result);
            }
            catch (Exception ex)
            {
                _loggerServices.WriteDebug(ex.Message);
            }
            return PartialView("PartialView/_FormSearch", model);
        }

        [HttpPost]
        public async Task<ActionResult> PostProjectReport(ReportFilterModel reportFilterModel)
        {
            var reportResultModel = new ReportResultModel();

            try
            {
                if (reportFilterModel.FromDate == null ||
                reportFilterModel.ToDate == null)
                    throw new ArgumentNullException(
                        "Invalid arguments: " +
                        $"{nameof(reportFilterModel.FromDate)}, " +
                        $"{nameof(reportFilterModel.ToDate)}");
                var report = new ReportDto();

                var reportFilterDto = _mapper.Map<ReportFilterDto>(reportFilterModel);

                var reportResultDto = await _ReportService.ProjectReportAsync(reportFilterDto);

                reportResultModel = _mapper.Map<ReportResultModel>(reportResultDto);
                foreach (var item in reportResultModel.Result)
                {
                    if (item.ParentId == Guid.Empty.ToString())
                    {
                        item.ParentId = null;

                    }
                }
                //#region Sumary Project
                //int countProject = reportResultModel.Result.Count(e => e.ParentId == null);
                //reportResultModel.SumaryProjects[0].Count = countProject;
                //// đếm danh mục có trạng thái là Mới
                //int countProjectNew = reportResultModel.Result.Count(e => e.ParentId == null && e.ProjectStatusId == 0);
                //reportResultModel.SumaryProjects.Add(new SumaryModel() { Name = "Mới", Count = countProjectNew, Percent = Math.Round(((double)countProjectNew / countProject) * 100, 2) });

                //// đếm danh mục có trạng thái là Đang xử lý
                //int countProjectInProcess = reportResultModel.Result.Count(e => e.ParentId == null && e.ProjectStatusId == 3);
                //reportResultModel.SumaryProjects.Add(new SumaryModel() { Name = "Đang xử lý", Count = countProjectInProcess, Percent = Math.Round(((double)countProjectInProcess / countProject) * 100, 2) });

                //// đếm danh mục có trạng thái là Kết thúc
                //int countProjectFinished = reportResultModel.Result.Count(e => e.ParentId == null && e.ProjectStatusId == 4);
                //reportResultModel.SumaryProjects.Add(new SumaryModel() { Name = "Kết thúc", Count = countProjectFinished, Percent = Math.Round(((double)countProjectFinished / countProject) * 100, 2) });


                //#endregion

                #region Sumary Task

                var tasks = reportResultModel.Result.Where(e => e.ShowType == ShowType.TaskItem).ToList();
                int countTask = tasks.Count();

                reportResultModel.SumaryTasks[0].Count = countTask;

                var taskItemStatuses = await _categoryService.GetAllTaskItemStatuses();
                taskItemStatuses = taskItemStatuses.Where(e => e.Id >= 0).ToList();
                int i = 1;
                foreach (var item in taskItemStatuses)
                {
                    int countTaskByStatuses = tasks.Count(e => e.TaskStatusId == (int)item.Id);
                    int countTaskByStatusAndInDues = tasks.Count(e => e.TaskStatusId == (int)item.Id && e.Progress == "Trong hạn");
                    int countTaskByStatusAndOutDues = tasks.Count(e => e.TaskStatusId == (int)item.Id && e.Progress == "Quá hạn");
                    double percent = Math.Round(((double)countTaskByStatuses / countTask) * 100, 2);
                    double? percentInDue = null;
                    double? percentOutDue = null;
                    if (countTaskByStatuses > 0)
                    {
                        percentInDue = countTaskByStatusAndInDues;
                        percentOutDue = countTaskByStatusAndOutDues;
                    }
                    reportResultModel.SumaryTasks.Add(new SumaryModel() { Index = i, Name = item.Name, Count = countTaskByStatuses, Percent = percent, PercentOutDue = percentOutDue, PercentInDue = percentInDue });
                    i++;
                }

                #endregion

                if (!string.IsNullOrEmpty(reportFilterModel.Action) &&
                    reportFilterModel.Action.ToLower() == "exportexcel")
                {
                    if (reportFilterModel.UserOfDepartmentId != null)
                    {
                        report = await _ReportService.GetAsyncByCode("ReportUser");
                    }
                    else
                    {
                        report = await _ReportService.GetAsyncByCode("ReportDepartment");
                    }
                    //var template = new FileInfo(Server.MapPath("~/Areas/Task/Content/Templates/TemplateProjectReport.xlsx"));
                    var stream = new MemoryStream(report.FileContent);
                    using (var package = new ExcelPackage(stream))
                    {
                        #region Sheet Detail
                        var worksheetDetail = package.Workbook.Worksheets.LastOrDefault();

                        var fromDateCellDetail =
                            (from cell
                                    in worksheetDetail.Cells
                             where !string.IsNullOrEmpty(cell.Text) && cell.Text.Contains("{{ fromDate }}")
                             select cell).FirstOrDefault();

                        if (fromDateCellDetail != null)
                            worksheetDetail.Cells[fromDateCellDetail.Address].Value =
                                worksheetDetail.Cells[fromDateCellDetail.Address].Text.Replace(
                                    "{{ fromDate }}",
                                    reportFilterModel.FromDate.Value.ToString("dd/MM/yyyy"));

                        var toDateCellDetail =
                            (from cell
                                    in worksheetDetail.Cells
                             where !string.IsNullOrEmpty(cell.Text) && cell.Text.Contains("{{ toDate }}")
                             select cell).FirstOrDefault();

                        if (toDateCellDetail != null)
                            worksheetDetail.Cells[toDateCellDetail.Address].Value =
                                worksheetDetail.Cells[toDateCellDetail.Address].Text.Replace(
                                    "{{ toDate }}",
                                    reportFilterModel.ToDate.Value.ToString("dd/MM/yyyy"));

                        var userDepartmentCellDetail =
                            (from cell
                                    in worksheetDetail.Cells
                             where !string.IsNullOrEmpty(cell.Text) && cell.Text.Contains("{{ userName }}")
                             select cell).FirstOrDefault();

                        if (userDepartmentCellDetail != null)
                            worksheetDetail.Cells[userDepartmentCellDetail.Address].Value =
                                worksheetDetail.Cells[userDepartmentCellDetail.Address].Text.Replace(
                                    "{{ userName }}",
                                    reportFilterModel.UserDepartmentText);

                        var trackingProgressCellDetail =
                            (from cell
                                    in worksheetDetail.Cells
                             where !string.IsNullOrEmpty(cell.Text) && cell.Text.Contains("{{ trackingProgress }}")
                             select cell).FirstOrDefault();

                        if (trackingProgressCellDetail != null)
                            worksheetDetail.Cells[trackingProgressCellDetail.Address].Value =
                                worksheetDetail.Cells[trackingProgressCellDetail.Address].Text.Replace(
                                    "{{ trackingProgress }}",
                                    reportFilterModel.TrackingProgress == -1
                                        ? "Tất cả"
                                        : reportFilterModel.TrackingProgress == 0
                                            ? "Trong hạn"
                                            : "Quá hạn");

                        var trackingStatusCellDetail =
                            (from cell
                                    in worksheetDetail.Cells
                             where !string.IsNullOrEmpty(cell.Text) && cell.Text.Contains("{{ trackingStatus }}")
                             select cell).FirstOrDefault();

                        if (trackingStatusCellDetail != null)
                            worksheetDetail.Cells[trackingStatusCellDetail.Address].Value =
                                worksheetDetail.Cells[trackingStatusCellDetail.Address].Text.Replace(
                                    "{{ trackingStatus }}",
                                    reportFilterModel.TrackingStatus == -1
                                        ? "Tất cả"
                                        : reportFilterModel.TrackingStatus == 0
                                            ? "Mới"
                                            : reportFilterModel.TrackingStatus == 1
                                            ? "Đang xử lý"
                                            : reportFilterModel.TrackingStatus == 2
                                            ? "Báo cáo"
                                            : reportFilterModel.TrackingStatus == 3
                                            ? "Trả lại"
                                            : reportFilterModel.TrackingStatus == 4
                                            ? "Kết thúc"
                                            : reportFilterModel.TrackingStatus == 5
                                            ? "Gia hạn"
                                            : reportFilterModel.TrackingStatus == 6
                                            ? "Báo cáo trả lại"
                                            : "Đã xem");

                        var trackingCrucialCellDetail =
                            (from cell
                                    in worksheetDetail.Cells
                             where !string.IsNullOrEmpty(cell.Text) && cell.Text.Contains("{{ trackingCrucial }}")
                             select cell).FirstOrDefault();

                        if (trackingCrucialCellDetail != null)
                            worksheetDetail.Cells[trackingCrucialCellDetail.Address].Value =
                                worksheetDetail.Cells[trackingCrucialCellDetail.Address].Text.Replace(
                                    "{{ trackingCrucial }}",
                                    reportFilterModel.TrackingCrucial == -1
                                        ? "Tất cả"
                                        : reportFilterModel.TrackingCrucial == 0
                                            ? "Bình thường"
                                            : reportFilterModel.TrackingCrucial == 1
                                            ? "Quan trọng"
                                            : "Rất quan trọng");

                        EplusExtension eplusExtension = new EplusExtension();
                        if (reportResultDto.Result.Any())
                        {
                            eplusExtension.FillData(reportResultDto.Result, worksheetDetail);
                        }

                        #endregion

                        #region Sheet Summary
                        var worksheetSummary = package.Workbook.Worksheets.FirstOrDefault();

                        var fromDateCellSummary =
                            (from cell
                                    in worksheetSummary.Cells
                             where !string.IsNullOrEmpty(cell.Text) && cell.Text.Contains("{{ fromDate }}")
                             select cell).FirstOrDefault();

                        if (fromDateCellSummary != null)
                            worksheetSummary.Cells[fromDateCellSummary.Address].Value =
                                worksheetSummary.Cells[fromDateCellSummary.Address].Text.Replace(
                                    "{{ fromDate }}",
                                    reportFilterModel.FromDate.Value.ToString("dd/MM/yyyy"));

                        var toDateCellSummary =
                            (from cell
                                    in worksheetSummary.Cells
                             where !string.IsNullOrEmpty(cell.Text) && cell.Text.Contains("{{ toDate }}")
                             select cell).FirstOrDefault();

                        if (toDateCellSummary != null)
                            worksheetSummary.Cells[toDateCellSummary.Address].Value =
                                worksheetSummary.Cells[toDateCellSummary.Address].Text.Replace(
                                    "{{ toDate }}",
                                    reportFilterModel.ToDate.Value.ToString("dd/MM/yyyy"));

                        var userDepartmentCellSummary =
                            (from cell
                                    in worksheetSummary.Cells
                             where !string.IsNullOrEmpty(cell.Text) && cell.Text.Contains("{{ userName }}")
                             select cell).FirstOrDefault();

                        if (userDepartmentCellSummary != null)
                            worksheetSummary.Cells[userDepartmentCellSummary.Address].Value =
                                worksheetSummary.Cells[userDepartmentCellSummary.Address].Text.Replace(
                                    "{{ userName }}",
                                    reportFilterModel.UserDepartmentText);

                        var trackingProgressCellSummary =
                            (from cell
                                    in worksheetSummary.Cells
                             where !string.IsNullOrEmpty(cell.Text) && cell.Text.Contains("{{ trackingProgress }}")
                             select cell).FirstOrDefault();

                        if (trackingProgressCellSummary != null)
                            worksheetSummary.Cells[trackingProgressCellSummary.Address].Value =
                                worksheetSummary.Cells[trackingProgressCellSummary.Address].Text.Replace(
                                    "{{ trackingProgress }}",
                                    reportFilterModel.TrackingProgress == -1
                                        ? "Tất cả"
                                        : reportFilterModel.TrackingProgress == 0
                                            ? "Trong hạn"
                                            : "Quá hạn");

                        var trackingStatusCellSummary =
                            (from cell
                                    in worksheetSummary.Cells
                             where !string.IsNullOrEmpty(cell.Text) && cell.Text.Contains("{{ trackingStatus }}")
                             select cell).FirstOrDefault();

                        if (trackingStatusCellSummary != null)
                            worksheetSummary.Cells[trackingStatusCellSummary.Address].Value =
                                worksheetSummary.Cells[trackingStatusCellSummary.Address].Text.Replace(
                                    "{{ trackingStatus }}",
                                    reportFilterModel.TrackingStatus == -1
                                        ? "Tất cả"
                                        : reportFilterModel.TrackingStatus == 0
                                            ? "Mới"
                                            : reportFilterModel.TrackingStatus == 1
                                            ? "Đang xử lý"
                                            : reportFilterModel.TrackingStatus == 2
                                            ? "Báo cáo"
                                            : reportFilterModel.TrackingStatus == 3
                                            ? "Trả lại"
                                            : reportFilterModel.TrackingStatus == 4
                                            ? "Kết thúc"
                                            : reportFilterModel.TrackingStatus == 5
                                            ? "Gia hạn"
                                            : reportFilterModel.TrackingStatus == 6
                                            ? "Báo cáo trả lại"
                                            : "Đã xem");

                        var trackingCrucialCellSummary =
                            (from cell
                                    in worksheetSummary.Cells
                             where !string.IsNullOrEmpty(cell.Text) && cell.Text.Contains("{{ trackingCrucial }}")
                             select cell).FirstOrDefault();

                        if (trackingCrucialCellSummary != null)
                            worksheetSummary.Cells[trackingCrucialCellSummary.Address].Value =
                                worksheetSummary.Cells[trackingCrucialCellSummary.Address].Text.Replace(
                                    "{{ trackingCrucial }}",
                                    reportFilterModel.TrackingCrucial == -1
                                        ? "Tất cả"
                                        : reportFilterModel.TrackingCrucial == 0
                                            ? "Bình thường"
                                            : reportFilterModel.TrackingCrucial == 1
                                            ? "Quan trọng"
                                            : "Rất quan trọng");

                        if (reportResultDto.SumaryTasks.Any())
                        {
                            eplusExtension.FillData(reportResultModel.SumaryTasks, worksheetSummary);
                        }

                        #endregion

                        var fileName = report.FileName;
                        var file = Guid.NewGuid().ToString();
                        TemporaryData[file] = package.GetAsByteArray();
                        var endpoint = Url.Action("ExportDepartmentReport", "Report", new
                        {
                            file,
                            fileName,
                            contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                        });
                        return Content(endpoint);
                    }
                }
                reportResultModel.Result = GetHierarchyReportItems(reportResultModel.Result, null);
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.Message);
            }



            return Json(reportResultModel, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ExportDepartmentReport(string file, string fileName, string contentType)
        {
            if (TemporaryData[file] != null &&
                !string.IsNullOrEmpty(file) &&
                !string.IsNullOrEmpty(contentType))
            {
                var fileContents = TemporaryData[file];
                TemporaryData.Remove(file);
                return File(fileContents, contentType, fileName);
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpGet]
        public async Task<ActionResult> ReportOnePage(Guid? projectId, string type, string device = "")
        {
            var model = new ReportFilterModel();
            try
            {
                ViewBag.Device = device;
                ViewBag.Type = type;
                var user = _userService.GetById(CurrentUser.Id);

                //model = await GetFilter(projectId, type, device);
            }
            catch (Exception ex)
            {
                _loggerServices.WriteDebug(ex.Message);
            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> PostReportOnePage(ReportFilterModel reportFilterModel)
        {
            if (reportFilterModel.FromDate == null ||
                reportFilterModel.ToDate == null)
                throw new ArgumentNullException(
                    "Invalid arguments: " +
                    $"{nameof(reportFilterModel.FromDate)}, " +
                    $"{nameof(reportFilterModel.ToDate)}");
            string appPath = string.Format("{0}", ConfigurationManager.AppSettings["RootApplicationPath"]);
            string TempFolderDir = Path.Combine(appPath, "ReportTempFile");
            var reportFilterDto = _mapper.Map<ReportFilterDto>(reportFilterModel);
            string templateDir = string.Empty;

            var report = await _ReportService.GetAsyncByCode("ReportOnePage");
            //templateDir = Server.MapPath("~/Areas/Task/Content/Templates/TemplateReportOnePage.xlsx");
            //var template = new FileInfo(templateDir);
            string fileName = report.FileName;
            byte[] userReportResult = await _ReportService.ExportReportOnepage(report.FileContent, reportFilterDto);
            if (!string.IsNullOrEmpty(reportFilterModel.Action) &&
               reportFilterModel.Action.ToLower() == "exportexcel")
            {
                var file = Guid.NewGuid().ToString();
                TemporaryData[file] = userReportResult;

                var endpoint = Url.Action("ExportReportProject", "Report", new
                {
                    file,
                    fileName,
                    contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                });

                return Content(endpoint);
            }
            else
            {
                try
                {
                    if (!Directory.Exists(TempFolderDir))
                    {
                        Directory.CreateDirectory(TempFolderDir);
                    }
                    fileName = Path.GetFileNameWithoutExtension(fileName);
                    fileName = fileName + "_" + Guid.NewGuid();
                    fileName = StringUtils.RemoveUnicode(fileName);
                    fileName = StringUtils.RemoveSpecialCharacters(fileName, "_") + ".xlsx";
                    string path = Path.Combine(TempFolderDir, fileName);
                    System.IO.File.WriteAllBytes(path, userReportResult);
                }
                catch (Exception ex)
                {
                    _loggerServices.WriteError(ex.Message);
                }

                return Json(new { fileName = fileName, result = true }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult DownloadFile(string fileName)
        {
            ActionResult actionResult = null;
            try
            {
                string appPath = string.Format("{0}", ConfigurationManager.AppSettings["RootApplicationPath"]);
                string TempFolderDir = Path.Combine(appPath, "ReportTempFile");
                string path = Path.Combine(TempFolderDir, fileName);
                actionResult = File(System.IO.File.ReadAllBytes(path), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", Path.GetFileName(path));
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.Message);
            }
            return actionResult;
        }

        [HttpGet]
        public ActionResult ReportIframe(string url, string title)
        {
            ViewBag.Url = url;
            ViewBag.Title = title;
            return View();
        }

        public List<ReportItemModel> GetHierarchyReportItems(List<ReportItemModel> data, string parentId)
        {
            List<ReportItemModel> roots;
            if (parentId == Guid.Empty.ToString())
            {
                roots = data
                    .Where(w => w.ParentId == null || w.ParentId == Guid.Empty.ToString() || w.ParentId == "#")
                    .OrderBy(o => o.NumberOf).ToList();
            }
            else
            {
                roots = data
                   .Where(w => w.ParentId == parentId)
                   .OrderBy(o => o.NumberOf).ToList();
            }
            foreach (var item in roots)
            {
                item.children = GetHierarchyReportItems(data, item.Id);
            }

            return roots;
        }
    }

}