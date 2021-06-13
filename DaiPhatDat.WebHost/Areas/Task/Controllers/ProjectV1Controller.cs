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
using System.Text.RegularExpressions;
using IKVM.Runtime;
using net.sf.mpxj.ikvm;
using net.sf.mpxj.MpxjUtilities;
using net.sf.mpxj.mspdi;
using net.sf.mpxj.writer;
using net.sf.mpxj.common;
using java.util;
using Microsoft.Ajax.Utilities;
using System.Configuration;

namespace DaiPhatDat.Module.Task.Web
{
    [Authorize]
    public partial class ProjectController : CoreController
    {
        public async Task<ActionResult> ExportExcel(Guid? projectId, Guid? taskId)
        {
            if (projectId == null || projectId == Guid.Empty)
            {
                return null;
            }
            try
            {
                var users = _userService.GetUsers();
                var userDepartments = await _userDepartmentServices.GetCachedUserDepartmentDtos();
                bool isAll = false;
                var projectDTO = await _projectService.GetById(projectId.Value);
                if (CurrentUser.HavePermission(EnumModulePermission.Task_FullControl))
                {
                    isAll = true;
                }
                var lstTrackingDocumentExcel = await _projectService.GetTaskForTableExcelV2(projectId.Value, CurrentUser.Id, taskId, string.Empty, 0, -1, isAll, null, users, true);

                string templateExcel = Server.MapPath("~/Areas/Task/Content/Templates/TemplateExportTaskV2Excel.xlsx");
                byte[] fileBytesArray = null;

                using (var stream = System.IO.File.OpenRead(templateExcel))
                {
                    using (var package = new ExcelPackage(stream))
                    {
                        var row = 3;
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                        var requestData = package.Workbook.Worksheets[1];
                        requestData.Hidden = eWorkSheetHidden.Hidden;
                        var userWorksheet = package.Workbook.Worksheets.Add("User");
                        userWorksheet.Cells[1, 1].Value = "UserName";
                        userWorksheet.Cells[1, 2].Value = "FullName";
                        userWorksheet.Cells[1, 3].Value = "Role";
                        int recordIndex = 2;
                        foreach (var user in users)
                        {
                            userWorksheet.Cells[recordIndex, 1].Value = user.UserName;
                            userWorksheet.Cells[recordIndex, 2].Value = user.FullName;
                            var jobTitle = string.Join("\n", userDepartments.Where(e => e.UserName == user.UserName).Select(e => e.JobTitleName + " - " + e.DeptName));
                            userWorksheet.Cells[recordIndex, 3].Value = jobTitle;
                            recordIndex++;
                        }
                        foreach (var item in lstTrackingDocumentExcel)
                        {
                            if (item.Visible)
                            {
                                List<TaskItemAssignDto> lstTaskItemAssigns = await _taskItemService.GetTaskItemAssignChildrens(item.Id);
                                StringBuilder lstTaskItemAssignId = new StringBuilder();

                                foreach (var taskItemAssign in lstTaskItemAssigns)
                                {
                                    lstTaskItemAssignId.AppendFormat("\n{0}:{1};", taskItemAssign.Id, taskItemAssign.AssignTo);
                                }

                                // Set value for each cell
                                worksheet.Cells[$"A{row}"].Value = item.No;
                                if (item.IsGroupLabel != null && item.IsGroupLabel.Value)
                                {
                                    worksheet.Cells[$"B{row}"].Value = "X";
                                }
                                else
                                {
                                    worksheet.Cells[$"B{row}"].Value = "";
                                }
                                worksheet.Cells[$"C{row}"].Value = item.Name;
                                worksheet.Cells[$"D{row}"].Value = item.Content;
                                worksheet.Cells[$"E{row}"].Value = item.AssignByUsername;
                                worksheet.Cells[$"F{row}"].Value = string.Join(";\n", item.MainWorkers.Select(p => p.Username).ToList());
                                worksheet.Cells[$"G{row}"].Value = string.Join(";\n", item.Supporters.Select(p => p.Username).ToList());
                              
                                worksheet.Cells[$"H{row}"].Value = item.StartTime.HasValue ? item.StartTime.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : string.Empty;
                                worksheet.Cells[$"I{row}"].Value = item.EndTime.HasValue ? item.EndTime.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : string.Empty;

                                if (item.TaskItemPriorityId.HasValue)
                                {
                                    if (item.TaskItemPriorityId == (int?)TaskItemPriorityId.Normal)
                                    {
                                        worksheet.Cells[$"J{row}"].Value = "Bình thường";
                                    }
                                    else if (item.TaskItemPriorityId == (int?)TaskItemPriorityId.Important)
                                    {
                                        worksheet.Cells[$"J{row}"].Value = "Quan trọng";
                                    }
                                    else if (item.TaskItemPriorityId == (int?)TaskItemPriorityId.Critical)
                                    {
                                        worksheet.Cells[$"J{row}"].Value = "Rất quan trọng";
                                    }
                                    else if (item.TaskItemPriorityId == (int?)TaskItemPriorityId.NotImportant)
                                    {
                                        worksheet.Cells[$"J{row}"].Value = "Không quan trọng";
                                    }
                                    else if (item.TaskItemPriorityId == (int?)TaskItemPriorityId.Necessary)
                                    {
                                        worksheet.Cells[$"J{row}"].Value = "Thiết yếu";
                                    }
                                }

                               
                                worksheet.Cells[$"K{row}"].Value = item.HasReport.HasValue && item.HasReport.Value ? "" : "X";

                                worksheet.Cells[$"AC{row}"].Value = item.Id;
                                worksheet.Cells[$"AD{row}"].Value = item.DocId;
                                // Aligning value in cell is center
                                worksheet.Cells[$"A{row}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                worksheet.Cells[$"B{row}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                worksheet.Cells[$"E{row}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                worksheet.Cells[$"F{row}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                worksheet.Cells[$"G{row}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                worksheet.Cells[$"H{row}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                worksheet.Cells[$"I{row}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                worksheet.Cells[$"J{row}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                worksheet.Cells[$"I{row}"].Style.Numberformat.Format = "@";
                                worksheet.Cells[$"J{row}"].Style.Numberformat.Format = "@";


                                worksheet.Cells[$"K{row}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                worksheet.Cells[$"L{row}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                worksheet.Cells[$"M{row}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                // Border for each cell
                                #region Set border for each cell
                                worksheet.Cells[$"A{row}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                worksheet.Cells[$"A{row}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                worksheet.Cells[$"A{row}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                worksheet.Cells[$"A{row}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                worksheet.Cells[$"B{row}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                worksheet.Cells[$"B{row}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                worksheet.Cells[$"B{row}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                worksheet.Cells[$"B{row}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                worksheet.Cells[$"C{row}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                worksheet.Cells[$"C{row}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                worksheet.Cells[$"C{row}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                worksheet.Cells[$"C{row}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                worksheet.Cells[$"D{row}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                worksheet.Cells[$"D{row}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                worksheet.Cells[$"D{row}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                worksheet.Cells[$"D{row}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                worksheet.Cells[$"E{row}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                worksheet.Cells[$"E{row}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                worksheet.Cells[$"E{row}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                worksheet.Cells[$"E{row}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                worksheet.Cells[$"F{row}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                worksheet.Cells[$"F{row}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                worksheet.Cells[$"F{row}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                worksheet.Cells[$"F{row}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                worksheet.Cells[$"G{row}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                worksheet.Cells[$"G{row}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                worksheet.Cells[$"G{row}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                worksheet.Cells[$"G{row}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                worksheet.Cells[$"H{row}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                worksheet.Cells[$"H{row}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                worksheet.Cells[$"H{row}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                worksheet.Cells[$"H{row}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                worksheet.Cells[$"I{row}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                worksheet.Cells[$"I{row}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                worksheet.Cells[$"I{row}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                worksheet.Cells[$"I{row}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                worksheet.Cells[$"J{row}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                worksheet.Cells[$"J{row}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                worksheet.Cells[$"J{row}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                worksheet.Cells[$"J{row}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                worksheet.Cells[$"K{row}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                worksheet.Cells[$"K{row}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                worksheet.Cells[$"K{row}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                worksheet.Cells[$"K{row}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                #endregion



                                // Hide value in Z row
                                worksheet.Cells[$"AC{row}"].Style.Font.Color.SetColor(System.Drawing.Color.White);
                                worksheet.Cells[$"AD{row}"].Style.Font.Color.SetColor(System.Drawing.Color.White);
                                worksheet.Cells[$"AE{row}"].Style.Font.Color.SetColor(System.Drawing.Color.White);

                                row++;
                            }
                        }

                        fileBytesArray = package.GetAsByteArray();
                    }
                }
                return File(fileBytesArray, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", projectDTO.Summary+".xlsx");
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
            }
            return null;
        }

        public async Task<JsonResult> ImportExcel(Guid projectId, Guid? taskId)
        {
            try
            {
                List<string> errors = new List<string>();
                List<TaskItemForMSProjectDto> lstTrackingDocument = new List<TaskItemForMSProjectDto>();
                DateTime? fromDate = null;
                DateTime? toDate = null;
                var projectDto = await _projectService.GetById(projectId);
                fromDate = projectDto.FromDate;
                toDate = projectDto.ToDate;
                if (taskId.HasValue)
                {
                    var taskDto = await _taskItemService.GetById(taskId.Value);
                    fromDate = taskDto.FromDate;
                    toDate = taskDto.ToDate;
                }
                var userDtos = _userService.GetUsers();
                if (!_projectService.CheckImportExcelPermission(CurrentUser.Id, projectId, taskId))
                {
                    return Json("AccessDenied", JsonRequestBehavior.AllowGet);
                }
                // Danh sach index trong template excel
                int colStt = 1;
                int colNhom = 2;
                int colTieuDeCongViec = 3;
                int colNoiDungCongViec = 4;
                int colNguoiGiao = 5;
                int colNguoiXuLyChinh = 6;
                int colNguoiXuLyPhu = 7;
                int colNgayBatDau = 8;
                int colHanXuLy = 9;
                int colMucDoQuanTrong = 10;
                int colBaoCao = 11;
                int colKetQua = 14;
                int colTaskItemId = 29;
                int colPojectId = 30;
                HttpPostedFileBase file = Request.Files[0];
                var listTaskItemCategories = new List<string>();
                if (file != null && file.ContentLength > 0 && !string.IsNullOrEmpty(file.FileName))
                {
                    string fileName = file.FileName;
                    string fileContent = file.ContentType;
                    string fileExtension = Path.GetExtension(file.FileName);
                    byte[] fileBytes = new byte[file.ContentLength];

                    using (var package = new ExcelPackage(file.InputStream))
                    {
                        var currentSheet = package.Workbook.Worksheets;
                        var workSheet = currentSheet.First();
                        var noOfCol = workSheet.Dimension.End.Column;
                        var noOfRow = workSheet.Dimension.End.Row;
                        for (int rowIterator = 3; rowIterator <= noOfRow; rowIterator++)
                        {
                            string error = string.Empty;
                            TaskItemForMSProjectDto taskDto = new TaskItemForMSProjectDto();
                            taskDto.MainWorkers = new List<UserExcelDto>();
                            taskDto.Supporters = new List<UserExcelDto>();
                            taskDto.WhoOnlyKnow = new List<UserExcelDto>();
                            taskDto.RowIndex = rowIterator;
                            if (workSheet.Cells[rowIterator, colStt].Value != null)
                            {
                                // Cap nhap TaskItem
                                taskDto.Id = Guid.NewGuid();
                                if (workSheet.Cells[rowIterator, colTaskItemId].Value != null)
                                {
                                    if (workSheet.Cells[rowIterator, colPojectId].Value != null)
                                    {
                                        Guid prjectId = Guid.Empty;
                                        if (Guid.TryParse(workSheet.Cells[rowIterator, colPojectId].Value.ToString(), out prjectId))
                                        {
                                            if (prjectId == projectId)
                                            {
                                                taskDto.Id = Guid.Parse(workSheet.Cells[rowIterator, colTaskItemId].Value.ToString());
                                            }
                                        }
                                    }
                                }
                                if (workSheet.Cells[rowIterator, colTieuDeCongViec].Value == null)
                                {
                                    error += "Tiêu đề không hợp lệ; ";
                                }
                                bool isGroup = workSheet.Cells[rowIterator, colNhom].Value != null && workSheet.Cells[rowIterator, colNhom].Value.ToString().ToLower().Equals("x") ? true : false;
                                if (isGroup)
                                {
                                    taskDto.IsGroupLabel = true;
                                }
                                else
                                {
                                    taskDto.IsGroupLabel = false;
                                }
                                List<string> mainWorkers = new List<string>();
                                string mainWorker = workSheet.Cells[rowIterator, colNguoiXuLyChinh].Value != null ? workSheet.Cells[rowIterator, colNguoiXuLyChinh].Value.ToString() : string.Empty;
                                if (string.IsNullOrEmpty(mainWorker))
                                {
                                    error += "Người xử lý chính không hợp lệ; ";
                                }
                                else 
                                {
                                    mainWorkers = mainWorker.Split(';').ToList();
                                    if (!userDtos.Any(p => mainWorkers.Any(w => w.Trim().ToLower() == p.UserName.ToLower()))) {
                                        error += "Người xử lý chính không hợp lệ; ";
                                    }
                                    else
                                    {
                                        foreach (var item in mainWorkers)
                                        {
                                            UserExcelDto worker = new UserExcelDto();
                                            worker.Username = Regex.Replace(item, @"\r\n?|\n", "");
                                            worker.Username = worker.Username.Replace(" ", "");
                                            taskDto.MainWorkers.Add(worker);
                                        }
                                    }
                                }
                                string supWorker = workSheet.Cells[rowIterator, colNguoiXuLyPhu].Value != null ? workSheet.Cells[rowIterator, colNguoiXuLyPhu].Value.ToString() : string.Empty;
                                if (!string.IsNullOrEmpty(supWorker))
                                {
                                    List<string> supWorkers = new List<string>();
                                    supWorkers = supWorker.Split(';').ToList();
                                    foreach (var item in supWorkers)
                                    {
                                        UserExcelDto worker = new UserExcelDto();
                                        worker.Username = Regex.Replace(item, @"\r\n?|\n", "");
                                        worker.Username = worker.Username.Replace(" ", "");
                                        taskDto.Supporters.Add(worker);
                                    }
                                }

                                DateTime date = DateTime.Now;

                                string startDate = workSheet.Cells[rowIterator, colNgayBatDau].Value != null ? workSheet.Cells[rowIterator, colNgayBatDau].Value.ToString() : string.Empty;
                                if (DateTime.TryParseExact(startDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                                {
                                    TimeSpan ts = new TimeSpan(08, 0, 0);
                                    date = date.Date + ts;
                                    taskDto.StartTime = date;
                                }
                                else
                                {
                                    error += "Ngày bắt đầu không hợp lệ; ";
                                }

                                string endDate = workSheet.Cells[rowIterator, colHanXuLy].Value != null ? workSheet.Cells[rowIterator, colHanXuLy].Value.ToString() : string.Empty;
                                if (DateTime.TryParseExact(endDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                                {
                                    TimeSpan ts = new TimeSpan(17, 0, 0);
                                    date = date.Date + ts;
                                    taskDto.EndTime = date;
                                }
                                else
                                {
                                    error += "Ngày kết thúc không hợp lệ; ";
                                }
                                string priority = workSheet.Cells[rowIterator, colMucDoQuanTrong].Value != null ? workSheet.Cells[rowIterator, colMucDoQuanTrong].Value.ToString() : string.Empty;
                                if (priority == "Bình thường")
                                {
                                    taskDto.TaskItemPriorityId = (int?)TaskItemPriorityId.Normal;
                                }
                                else if (priority == "Quan trọng")
                                {
                                    taskDto.TaskItemPriorityId = (int?)TaskItemPriorityId.Important;
                                }
                                else if (priority == "Rất quan trọng")
                                {
                                    taskDto.TaskItemPriorityId = (int?)TaskItemPriorityId.Critical;
                                }
                                else if (priority == "Không quan trọng")
                                {
                                    taskDto.TaskItemPriorityId = (int?)TaskItemPriorityId.NotImportant;
                                }
                                else if (priority == "Thiết yếu")
                                {
                                    taskDto.TaskItemPriorityId = (int?)TaskItemPriorityId.Necessary;
                                }
                                else
                                {
                                    taskDto.TaskItemPriorityId = (int?)TaskItemPriorityId.Normal;
                                }
                                taskDto.HasReport = workSheet.Cells[rowIterator, colBaoCao].Value != null && workSheet.Cells[rowIterator, colBaoCao].Value.ToString().ToLower().Equals("x") ? false : true;
                                if (!string.IsNullOrEmpty(error))
                                {
                                    errors.Add(error);
                                    workSheet.Cells[rowIterator, colKetQua].Value = error;
                                    workSheet.Cells[rowIterator, colKetQua].Style.Font.Color.SetColor(System.Drawing.Color.Red);
                                    continue;
                                }
                                if (!string.IsNullOrEmpty(taskDto.TaskItemCategory))
                                {
                                    List<string> categories = taskDto.TaskItemCategory.Split(';').ToList();
                                    categories.ForEach(e => e = e.Replace(" ", ""));
                                    listTaskItemCategories.AddRange(categories);
                                }
                                taskDto.No = workSheet.Cells[rowIterator, colStt].Value != null ? workSheet.Cells[rowIterator, colStt].Value.ToString() : string.Empty;
                                taskDto.No = taskDto.No.Replace(",", ".");
                                taskDto.Name = workSheet.Cells[rowIterator, colTieuDeCongViec].Value != null ? workSheet.Cells[rowIterator, colTieuDeCongViec].Value.ToString() : string.Empty;
                                if (workSheet.Cells[rowIterator, colNoiDungCongViec].Value != null)
                                {
                                    taskDto.Content = workSheet.Cells[rowIterator, colNoiDungCongViec].Value.ToString();
                                }
                                if (workSheet.Cells[rowIterator, colNguoiGiao].Value != null)
                                {
                                    taskDto.AssignByUsername = workSheet.Cells[rowIterator, colNguoiGiao].Value.ToString();
                                }
                                else
                                {
                                    taskDto.AssignByUsername = CurrentUser.AccountName;
                                }
                                lstTrackingDocument.Add(taskDto);
                            }
                        }
                        foreach (TaskItemForMSProjectDto task in lstTrackingDocument)
                        {
                            // Nếu import từ task và = task thì ko đổi parent Id
                            task.ParentId = Guid.Empty;
                            if (taskId.HasValue && task.Id != taskId)
                            {
                                task.ParentId = taskId;
                            }
                            var number = task.No;
                            if (number.IndexOf(".") > 0)
                            {
                                var parentNumber = number.Substring(0, number.LastIndexOf("."));
                                var parentTaskItem = lstTrackingDocument.Where(p => p.No == parentNumber).FirstOrDefault();
                                if (parentTaskItem == null)
                                {
                                    continue;
                                }
                                else
                                {
                                    task.ParentId = parentTaskItem.Id;
                                }
                            }
                        }
                        foreach (TaskItemForMSProjectDto task in lstTrackingDocument)
                        {
                            string error = "";
                            // Nếu import từ task và = task thì lấy range trên
                            if (taskId.HasValue && task.Id == taskId)
                            {
                                var taskParent = await _taskItemService.GetById(taskId.Value);
                                var checkTask = await _taskItemService.GetDateRangeForTask(projectId, taskParent.ParentId);
                                if (checkTask != null)
                                {
                                    if (task.StartTime.HasValue && checkTask.FromDate.HasValue)
                                    {
                                        if (task.StartTime.Value.Date < checkTask.FromDate.Value.Date)
                                        {
                                            error = "Ngày bắt đầu nhỏ hơn ngày bắt đầu của công việc cha;";
                                        }
                                    }
                                    if (task.EndTime.HasValue && checkTask.ToDate.HasValue)
                                    {
                                        if (task.EndTime.Value.Date > checkTask.ToDate.Value.Date)
                                        {
                                            error += " Ngày kết thúc lớn hơn ngày kết thúc của công việc cha";
                                        }
                                    }
                                }
                            }
                            else
                            {
                                TaskItemForMSProjectDto checkTask = new TaskItemForMSProjectDto
                                {
                                    ParentId = task.ParentId,
                                    StartTime = task.StartTime,
                                    EndTime = task.EndTime
                                };
                                error = ValidateRangeTimeTask(fromDate, toDate, checkTask, lstTrackingDocument);
                            }
                            if (!string.IsNullOrEmpty(error))
                            {
                                errors.Add(error);
                                workSheet.Cells[task.RowIndex, colKetQua].Value = error;
                                workSheet.Cells[task.RowIndex, colKetQua].Style.Font.Color.SetColor(System.Drawing.Color.Red);
                            }
                        }
                        if (!errors.Any())
                        {
                            int order = 0;
                            foreach (var task in lstTrackingDocument)
                            {
                                if (task.ParentId == Guid.Empty)
                                {
                                    task.Order = order;
                                    order++;
                                }
                                else
                                {
                                    var index = lstTrackingDocument.Where(e => e.ParentId == task.ParentId).ToList().FindIndex(e => e.Id == task.Id);
                                    task.Order = index;
                                }
                            }
                            bool rs = await _taskItemService.ImportExcelV2Action(lstTrackingDocument, projectId, CurrentUser.Id, taskId);
                            //if (listTaskItemCategories.Any())
                            //    await _taskItemService.CreateTaskItemCategories(CurrentUser.ID, listTaskItemCategories);
                            if (rs)
                            {
                                return Json(new { Message = "Success", Error = "Lỗi hệ thống" });
                            }
                            else
                            {
                                return Json(new { Message = "Failure" });
                            }
                        }
                        else
                        {
                            fileBytes = package.GetAsByteArray();
                            if (!Directory.Exists(Server.MapPath("~/ErrorFile")))
                            {
                                Directory.CreateDirectory(Server.MapPath("~/ErrorFile"));
                            }
                            var path = Server.MapPath("~/ErrorFile/" + fileName);
                            System.IO.File.WriteAllBytes(path, fileBytes);
                            return Json(new { Message = "Failure", Url = Convert.ToBase64String(Encoding.UTF8.GetBytes(path)), FileName = fileName });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
                return Json(new { Message = "Failure" });
            }
            return Json(new { Message = "Failure" });
        }
        private string ValidateRangeTimeTask(DateTime? fromdate, DateTime? toDate, TaskItemForMSProjectDto taskItem, List<TaskItemForMSProjectDto> lstTrackingDocument)
        {
            string error = string.Empty;
            var parentTaskItem = lstTrackingDocument.Where(e => e.Id == taskItem.ParentId).FirstOrDefault();
            if (parentTaskItem != null)
            {
                if (!parentTaskItem.IsGroupLabel.HasValue || !parentTaskItem.IsGroupLabel.Value)
                {
                    if (taskItem.StartTime.HasValue && parentTaskItem.StartTime.HasValue)
                    {
                        if (taskItem.StartTime.Value.Date < parentTaskItem.StartTime.Value.Date)
                        {
                            error = "Ngày bắt đầu nhỏ hơn ngày bắt đầu của công việc cha;";
                        }
                    }
                    if (taskItem.EndTime.HasValue && parentTaskItem.EndTime.HasValue)
                    {
                        if (taskItem.EndTime.Value.Date > parentTaskItem.EndTime.Value.Date)
                        {
                            error += " Ngày kết thúc nhỏ hơn ngày kết thúc của công việc cha";
                        }
                    }
                }
                else
                {
                    taskItem.ParentId = parentTaskItem.ParentId;
                    error = ValidateRangeTimeTask(fromdate, toDate, taskItem, lstTrackingDocument);
                }
            }
            else
            {
                if (taskItem.StartTime.HasValue && fromdate.HasValue)
                {
                    if (taskItem.StartTime.Value.Date < fromdate.Value.Date)
                    {
                        error = "Ngày bắt đầu nhỏ hơn ngày bắt đầu của công việc cha;";
                    }
                }
                if (taskItem.EndTime.HasValue && toDate.HasValue)
                {
                    if (taskItem.EndTime.Value.Date > toDate.Value.Date)
                    {
                        error += " Ngày kết thúc nhỏ hơn ngày kết thúc của công việc cha";
                    }
                }
            }
            return error;
        }
        public ActionResult DownloadFile(string path)
        {
            try
            {
                string path2 = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(path));
                byte[] fileBytesArray = null;

                using (var stream = System.IO.File.OpenRead(path2))
                using (var package = new ExcelPackage(stream))
                {
                    fileBytesArray = package.GetAsByteArray();
                }

                return File(fileBytesArray, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public async Task<ActionResult> ExportMSProject(Guid projectId)
        {
            byte[] fileBytesArray = null;
            var projectDTO = await _projectService.GetById(projectId);
            try
            {
                bool isAll = false;
                var permission = CurrentUser.Permissions;
                if (permission.Any(e => e == EnumModulePermission.Task_FullControl))
                {
                    isAll = true;
                }
                IReadOnlyList<TaskItemForMSProjectDto> lstTrackingDocumentExcel = await _projectService.GetTaskForTableMSPAsync(projectId, CurrentUser.Id, Guid.Empty, string.Empty, 0, -1, isAll);
                net.sf.mpxj.ProjectFile file = new net.sf.mpxj.ProjectFile();
                net.sf.mpxj.ProjectProperties properties = file.getProjectProperties();
                net.sf.mpxj.ProjectCalendar calendar = file.addDefaultBaseCalendar();
                calendar.setName("Task format 24 hours");
                java.text.SimpleDateFormat df = new java.text.SimpleDateFormat("dd/MM/yyyy HH:mm");
                net.sf.mpxj.DateRange range = new net.sf.mpxj.DateRange(df.parse(projectDTO.FromDate.Value.ToString("dd/MM/yyyy 00:00")), df.parse(projectDTO.ToDate.Value.AddDays(1).ToString("dd/MM/yyyy 00:00")));
                //
                // Mark each net.sf.mpxj.Day as working
                //
                calendar.setWorkingDay(net.sf.mpxj.Day.SUNDAY, net.sf.mpxj.DayType.WORKING);
                calendar.setWorkingDay(net.sf.mpxj.Day.MONDAY, net.sf.mpxj.DayType.WORKING);
                calendar.setWorkingDay(net.sf.mpxj.Day.TUESDAY, net.sf.mpxj.DayType.WORKING);
                calendar.setWorkingDay(net.sf.mpxj.Day.WEDNESDAY, net.sf.mpxj.DayType.WORKING);
                calendar.setWorkingDay(net.sf.mpxj.Day.THURSDAY, net.sf.mpxj.DayType.WORKING);
                calendar.setWorkingDay(net.sf.mpxj.Day.FRIDAY, net.sf.mpxj.DayType.WORKING);
                calendar.setWorkingDay(net.sf.mpxj.Day.SATURDAY, net.sf.mpxj.DayType.WORKING);

                //
                // Add a working hours range to each net.sf.mpxj.Day
                ////
                net.sf.mpxj.ProjectCalendarHours hours;
                hours = calendar.addCalendarHours(net.sf.mpxj.Day.SUNDAY);
                hours.addRange(net.sf.mpxj.ProjectCalendarWeek.DEFAULT_WORKING_MORNING);
                hours.addRange(net.sf.mpxj.ProjectCalendarWeek.DEFAULT_WORKING_AFTERNOON);
                hours = calendar.addCalendarHours(net.sf.mpxj.Day.MONDAY);
                hours.addRange(net.sf.mpxj.ProjectCalendarWeek.DEFAULT_WORKING_MORNING);
                hours.addRange(net.sf.mpxj.ProjectCalendarWeek.DEFAULT_WORKING_AFTERNOON);
                hours = calendar.addCalendarHours(net.sf.mpxj.Day.TUESDAY);
                hours.addRange(net.sf.mpxj.ProjectCalendarWeek.DEFAULT_WORKING_MORNING);
                hours.addRange(net.sf.mpxj.ProjectCalendarWeek.DEFAULT_WORKING_AFTERNOON);
                hours = calendar.addCalendarHours(net.sf.mpxj.Day.WEDNESDAY);
                hours.addRange(net.sf.mpxj.ProjectCalendarWeek.DEFAULT_WORKING_MORNING);
                hours.addRange(net.sf.mpxj.ProjectCalendarWeek.DEFAULT_WORKING_AFTERNOON);
                hours = calendar.addCalendarHours(net.sf.mpxj.Day.THURSDAY);
                hours.addRange(net.sf.mpxj.ProjectCalendarWeek.DEFAULT_WORKING_MORNING);
                hours.addRange(net.sf.mpxj.ProjectCalendarWeek.DEFAULT_WORKING_AFTERNOON);
                hours = calendar.addCalendarHours(net.sf.mpxj.Day.FRIDAY);
                hours.addRange(net.sf.mpxj.ProjectCalendarWeek.DEFAULT_WORKING_MORNING);
                hours.addRange(net.sf.mpxj.ProjectCalendarWeek.DEFAULT_WORKING_AFTERNOON);
                hours = calendar.addCalendarHours(net.sf.mpxj.Day.SATURDAY);
                hours.addRange(net.sf.mpxj.ProjectCalendarWeek.DEFAULT_WORKING_MORNING);
                hours.addRange(net.sf.mpxj.ProjectCalendarWeek.DEFAULT_WORKING_AFTERNOON);

                net.sf.mpxj.CustomFieldContainer customFields = file.getCustomFields();
                net.sf.mpxj.CustomField field = customFields.getCustomField(net.sf.mpxj.TaskField.TEXT29);
                field.setAlias("TaskGuid");
                net.sf.mpxj.CustomField fieldNguoiGiao = customFields.getCustomField(net.sf.mpxj.TaskField.TEXT27);
                fieldNguoiGiao.setAlias("Người giao");
                var mainWorkers = GetMainWorkersFromTask(lstTrackingDocumentExcel.ToList());
                mainWorkers = mainWorkers.DistinctBy(e => e.Username).ToList();
                List<net.sf.mpxj.Resource> resources = new List<net.sf.mpxj.Resource>();
                foreach (var worker in mainWorkers)
                {
                    net.sf.mpxj.Resource resource = file.addResource();
                    resource.setName(worker.Text);
                    resource.setNtAccount(worker.Username);
                    var calendarRs = resource.addResourceCalendar();
                    calendarRs.setWorkingDay(net.sf.mpxj.Day.SUNDAY, net.sf.mpxj.DayType.WORKING);
                    calendarRs.setWorkingDay(net.sf.mpxj.Day.MONDAY, net.sf.mpxj.DayType.WORKING);
                    calendarRs.setWorkingDay(net.sf.mpxj.Day.TUESDAY, net.sf.mpxj.DayType.WORKING);
                    calendarRs.setWorkingDay(net.sf.mpxj.Day.WEDNESDAY, net.sf.mpxj.DayType.WORKING);
                    calendarRs.setWorkingDay(net.sf.mpxj.Day.THURSDAY, net.sf.mpxj.DayType.WORKING);
                    calendarRs.setWorkingDay(net.sf.mpxj.Day.FRIDAY, net.sf.mpxj.DayType.WORKING);
                    calendarRs.setWorkingDay(net.sf.mpxj.Day.SATURDAY, net.sf.mpxj.DayType.WORKING);
                    hours = calendarRs.addCalendarHours(net.sf.mpxj.Day.SUNDAY);
                    hours.addRange(net.sf.mpxj.ProjectCalendarWeek.DEFAULT_WORKING_MORNING);
                    hours.addRange(net.sf.mpxj.ProjectCalendarWeek.DEFAULT_WORKING_AFTERNOON);
                    hours = calendarRs.addCalendarHours(net.sf.mpxj.Day.MONDAY);
                    hours.addRange(net.sf.mpxj.ProjectCalendarWeek.DEFAULT_WORKING_MORNING);
                    hours.addRange(net.sf.mpxj.ProjectCalendarWeek.DEFAULT_WORKING_AFTERNOON);
                    hours = calendarRs.addCalendarHours(net.sf.mpxj.Day.TUESDAY);
                    hours.addRange(net.sf.mpxj.ProjectCalendarWeek.DEFAULT_WORKING_MORNING);
                    hours.addRange(net.sf.mpxj.ProjectCalendarWeek.DEFAULT_WORKING_AFTERNOON);
                    hours = calendarRs.addCalendarHours(net.sf.mpxj.Day.WEDNESDAY);
                    hours.addRange(net.sf.mpxj.ProjectCalendarWeek.DEFAULT_WORKING_MORNING);
                    hours.addRange(net.sf.mpxj.ProjectCalendarWeek.DEFAULT_WORKING_AFTERNOON);
                    hours = calendarRs.addCalendarHours(net.sf.mpxj.Day.THURSDAY);
                    hours.addRange(net.sf.mpxj.ProjectCalendarWeek.DEFAULT_WORKING_MORNING);
                    hours.addRange(net.sf.mpxj.ProjectCalendarWeek.DEFAULT_WORKING_AFTERNOON);
                    hours = calendarRs.addCalendarHours(net.sf.mpxj.Day.FRIDAY);
                    hours.addRange(net.sf.mpxj.ProjectCalendarWeek.DEFAULT_WORKING_MORNING);
                    hours.addRange(net.sf.mpxj.ProjectCalendarWeek.DEFAULT_WORKING_AFTERNOON);
                    hours = calendarRs.addCalendarHours(net.sf.mpxj.Day.SATURDAY);
                    hours.addRange(net.sf.mpxj.ProjectCalendarWeek.DEFAULT_WORKING_MORNING);
                    hours.addRange(net.sf.mpxj.ProjectCalendarWeek.DEFAULT_WORKING_AFTERNOON);
                    resources.Add(resource);
                }
                foreach (var task in lstTrackingDocumentExcel)
                {
                    net.sf.mpxj.Task taskMSP = file.addTask();
                    taskMSP.setName(task.Name);
                    taskMSP.setCalendarUniqueID(calendar.getUniqueID());
                    taskMSP.setGUID(UUID.fromString(task.Id.ToString()));
                    taskMSP.setText(29, task.Id.ToString());
                    taskMSP.setText(27, task.AssignByUsername);
                    if (!task.IsHaveChild)
                    {
                        if (task.StartTime.HasValue)
                        {
                            taskMSP.setStart(df.parse(task.StartTime.Value.ToString("dd/MM/yyyy HH:mm")));
                            taskMSP.setActualStart(df.parse(task.StartTime.Value.ToString("dd/MM/yyyy HH:mm")));
                        }
                        if (task.EndTime.HasValue)
                        {
                            taskMSP.setFinish(df.parse(task.EndTime.Value.ToString("dd/MM/yyyy HH:mm")));

                        }
                        if (task.FinishiDate.HasValue)
                        {
                            taskMSP.setActualFinish(df.parse(task.FinishiDate.Value.ToString("dd/MM/yyyy HH:mm")));
                        }
                        if (task.EndTime.HasValue && task.StartTime.HasValue)
                        {
                            net.sf.mpxj.Duration duration = calendar.getWork(taskMSP.getStart(), taskMSP.getFinish(), net.sf.mpxj.TimeUnit.DAYS);
                            taskMSP.setDuration(duration);

                        }
                        if (task.FinishiDate.HasValue)
                        {
                            net.sf.mpxj.Duration duration = calendar.getWork(taskMSP.getActualStart(), taskMSP.getActualFinish(), net.sf.mpxj.TimeUnit.DAYS);
                            taskMSP.setActualDuration(duration);
                        }

                        if (task.PercentFinish.HasValue)
                        {
                            taskMSP.setPercentageComplete(NumberHelper.getDouble(task.PercentFinish.Value));
                        }
                        List<UserExcelDto> listWorkers = new List<UserExcelDto>();
                        listWorkers.AddRange(task.MainWorkers);
                        listWorkers.AddRange(task.Supporters);
                        listWorkers.AddRange(task.WhoOnlyKnow);
                        listWorkers = listWorkers.Distinct().ToList();
                        if (task.StartTime.HasValue)
                        {
                            foreach (var woker in listWorkers)
                            {
                                var rs = resources.Where(e => e.getNtAccount() == woker.Username).FirstOrDefault();
                                net.sf.mpxj.ResourceAssignment assignment1 = taskMSP.addResourceAssignment(rs);
                                //assignment1.setUnits(NumberHelper.getDouble((8 / 24) * 100.0));
                                assignment1.setStart(taskMSP.getStart());
                                assignment1.setFinish(taskMSP.getFinish());
                                assignment1.setGUID(UUID.fromString(woker.ID.ToString()));
                                assignment1.setText(28, woker.ID.ToString());
                                if (woker.FinishedDate.HasValue)
                                {
                                    assignment1.setActualFinish(df.parse(woker.FinishedDate.Value.ToString("dd/MM/yyyy HH:mm")));
                                }

                                assignment1.setWork(taskMSP.getDuration());
                                var percent = woker.PercentFinish.HasValue ? woker.PercentFinish.Value : 0;
                                assignment1.setPercentageWorkComplete(NumberHelper.getDouble(percent));
                                var dayt = taskMSP.getDuration().getDuration();
                                var pert = assignment1.getPercentageWorkComplete();
                                var durt = dayt * NumberHelper.getDouble(pert) / 100;
                                assignment1.setActualWork(net.sf.mpxj.Duration.getInstance(durt, net.sf.mpxj.TimeUnit.DAYS));
                                assignment1.setRemainingWork(net.sf.mpxj.Duration.getInstance(dayt - durt, net.sf.mpxj.TimeUnit.DAYS));
                            }
                        }
                    }

                    if (task.IsHaveChild)
                    {
                        AddTaskForMSProject(taskMSP, task.Childrens, resources, calendar);
                    }
                }
                string domain = ConfigurationManager.AppSettings["TempFileDocuments"].ToString();
                java.io.File file1 = java.io.File.createTempFile("demo", ".xml", new java.io.File(domain));
                MSPDIWriter writer = new MSPDIWriter();
                writer.write(file, file1);
                fileBytesArray = java.nio.file.Files.readAllBytes(file1.toPath());
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
            return File(fileBytesArray, "application/xml", projectDTO.Summary+ ".xml");
        }
        [HttpPost]
        public async Task<JsonResult> ImportMSProject(Guid projectId)
        {
            HttpPostedFileBase file = Request.Files[0];
            List<TaskItemForMSProjectDto> lstTrackingDocument = new List<TaskItemForMSProjectDto>();
            try
            {
                bool isAll = false;
                List<string> errors = new List<string>();
                var projectDto = await _projectService.GetById(projectId);
                if (!_projectService.CheckImportExcelPermission(CurrentUser.Id, projectId, null))
                {
                    return Json("AccessDenied", JsonRequestBehavior.AllowGet);
                }
                IReadOnlyList<Guid> listIdOfUser = new List<Guid>();
                if (projectDto.CreatedBy == CurrentUser.Id)
                {
                    isAll = true;
                }
                else
                {
                    listIdOfUser = _taskItemService.GetTaskOfUserAssign(projectId, CurrentUser.Id);
                }
                var buffer = new byte[file.InputStream.Length];
                file.InputStream.Read(buffer, 0, (int)file.InputStream.Length);
                net.sf.mpxj.ProjectFile project = null;
                if (file.ContentType == "text/xml")
                {
                    var reader = new net.sf.mpxj.mspdi.MSPDIReader();
                    project = reader.read(new java.io.ByteArrayInputStream(buffer));
                }
                else if (file.ContentType == "application/vnd.ms-project")
                {
                    var reader = new net.sf.mpxj.mpp.MPPReader();
                    project = reader.read(new java.io.ByteArrayInputStream(buffer));
                }
                else
                {
                    return Json("AccessDenied", JsonRequestBehavior.AllowGet);
                }
                var tasks = project.getTasks();
                int order = 0;
                foreach (net.sf.mpxj.Task task in tasks)
                {
                    TaskItemForMSProjectDto taskDto = new TaskItemForMSProjectDto();
                    try
                    {
                        var isAc = task.getID();
                        var level = NumberHelper.getInt(task.getOutlineLevel());
                        if (NumberHelper.getInt(isAc) != 0 && level == 1)
                        {
                            taskDto.Id = string.IsNullOrEmpty(task.getText(29)) ? Guid.NewGuid() : new Guid(task.getText(29));
                            taskDto.AssignByUsername = string.IsNullOrEmpty(task.getText(27)) ? null : task.getText(27);
                            taskDto.IsOwner = isAll || listIdOfUser.Contains(taskDto.Id);
                            taskDto.Name = task.getName();
                            taskDto.DocId = projectId;
                            taskDto.Order = order;
                            order++;
                            if (task.getStart() != null)
                            {
                                taskDto.StartTime = task.getStart().ToDateTime();
                            }
                            if (task.getFinish() != null)
                            {
                                taskDto.EndTime = task.getFinish().ToDateTime();
                            }
                            if (task.getActualFinish() != null)
                            {
                                taskDto.FinishiDate = task.getActualFinish().ToDateTime();
                            }
                            taskDto.PercentFinish = NumberHelper.getDouble(task.getPercentageComplete());
                            var assignments = task.getResourceAssignments().toArray();
                            List<object> assignmentLists = assignments.ToList();
                            List<UserExcelDto> mainWorkers = new List<UserExcelDto>();
                            foreach (net.sf.mpxj.ResourceAssignment item in assignments)
                            {
                                UserExcelDto worker = new UserExcelDto();
                                var resource = item.getResource();
                                worker.Username = resource.getNtAccount();
                                worker.Text = resource.getName();
                                worker.ID = !string.IsNullOrEmpty(item.getText(28)) ? new Guid(item.getText(28)) : new Guid?();
                                worker.PercentFinish = NumberHelper.getDouble(item.getPercentageWorkComplete());
                                if (item.getActualFinish() != null)
                                {
                                    worker.FinishedDate = item.getActualFinish().ToDateTime();
                                }
                                mainWorkers.Add(worker);
                            }
                            taskDto.MainWorkers = mainWorkers;
                            if (task.hasChildTasks())
                            {
                                taskDto.IsGroupLabel = true;
                                GetMSProjectChildTasks(task.getChildTasks(), taskDto.Id, projectId, ref lstTrackingDocument, ref errors, taskDto.IsOwner, listIdOfUser);
                            }
                            lstTrackingDocument.Add(taskDto);
                        }
                    }
                    catch (Exception)
                    {
                        errors.Add("Công việc: " + taskDto.Name + " có lỗi trong quá trình xử lý");
                        continue;
                    }


                }
                lstTrackingDocument = lstTrackingDocument.Where(e => e.IsOwner).ToList();
                await _taskItemService.ImportMSProject(lstTrackingDocument, projectId, isAll, CurrentUser.Id);
                if (!errors.Any())
                {
                    return Json("Success", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Message = "Error", Data = errors }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
                return Json("Error", JsonRequestBehavior.AllowGet);
            }

        }
        private void GetMSProjectChildTasks(java.util.List tasks, Guid parentId, Guid? documentId, ref List<TaskItemForMSProjectDto> lstTrackingDocument, ref List<string> errors, bool isOwner, IReadOnlyList<Guid> listIdOfUser)
        {
            var listTasks = tasks.toArray().ToList();
            int order = 0;
            foreach (net.sf.mpxj.Task task in listTasks)
            {
                TaskItemForMSProjectDto taskDto = new TaskItemForMSProjectDto();
                try
                {
                    taskDto.Id = string.IsNullOrEmpty(task.getText(29)) ? Guid.NewGuid() : new Guid(task.getText(29));
                    taskDto.IsOwner = isOwner || listIdOfUser.Contains(taskDto.Id);
                    taskDto.Name = task.getName();
                    taskDto.DocId = documentId;
                    taskDto.ParentId = parentId;
                    taskDto.Order = order;
                    order++;
                    taskDto.AssignByUsername = string.IsNullOrEmpty(task.getText(27)) ? null : task.getText(27);
                    if (task.getStart() != null)
                    {
                        taskDto.StartTime = task.getStart().ToDateTime();
                    }
                    if (task.getFinish() != null)
                    {
                        taskDto.EndTime = task.getFinish().ToDateTime();
                    }
                    if (task.getActualFinish() != null)
                    {
                        taskDto.FinishiDate = task.getActualFinish().ToDateTime();
                    }
                    taskDto.PercentFinish = NumberHelper.getDouble(task.getPercentageComplete());

                    var assignments = task.getResourceAssignments().toArray();
                    List<object> assignmentLists = assignments.ToList();
                    List<UserExcelDto> mainWorkers = new List<UserExcelDto>();
                    foreach (net.sf.mpxj.ResourceAssignment item in assignments)
                    {
                        var resource = item.getResource();
                        if (resource != null)
                        {
                            UserExcelDto worker = new UserExcelDto();
                            worker.Username = resource.getNtAccount();
                            worker.Text = resource.getName();
                            worker.ID = !string.IsNullOrEmpty(item.getText(28)) ? new Guid(item.getText(28)) : new Guid?();
                            var nam = item.getText(28);
                            worker.PercentFinish = NumberHelper.getDouble(item.getPercentageWorkComplete());
                            if (item.getActualFinish() != null)
                            {
                                worker.FinishedDate = item.getActualFinish().ToDateTime();
                            }
                            mainWorkers.Add(worker);
                        }

                    }
                    taskDto.MainWorkers = mainWorkers;
                    if (task.hasChildTasks())
                    {
                        taskDto.IsGroupLabel = true;
                        var list = task.getChildTasks();
                        GetMSProjectChildTasks(list, taskDto.Id, documentId, ref lstTrackingDocument, ref errors, taskDto.IsOwner, listIdOfUser);
                    }
                    lstTrackingDocument.Add(taskDto);
                }
                catch (Exception)
                {
                    errors.Add("Công việc: " + taskDto.Name + " có lỗi trong quá trình xử lý");
                    continue;
                }

            }
        }
        private List<UserExcelDto> GetMainWorkersFromTask(List<TaskItemForMSProjectDto> tasks)
        {
            List<UserExcelDto> workers = new List<UserExcelDto>();
            workers = tasks.SelectMany(e => e.MainWorkers).ToList();
            workers.AddRange(tasks.SelectMany(e => e.Supporters).ToList());
            workers.AddRange(tasks.SelectMany(e => e.WhoOnlyKnow).ToList());
            foreach (var task in tasks)
            {
                if (task.IsHaveChild)
                {
                    workers.AddRange(GetMainWorkersFromTask(task.Childrens));
                }
            }
            return workers;
        }
        private void AddTaskForMSProject(net.sf.mpxj.Task taskParent, List<TaskItemForMSProjectDto> tasks, List<net.sf.mpxj.Resource> resources, net.sf.mpxj.ProjectCalendar calendar)
        {
            java.text.SimpleDateFormat df = new java.text.SimpleDateFormat("dd/MM/yyyy HH:mm");
            foreach (var task in tasks)
            {
                net.sf.mpxj.Task taskChild = taskParent.addTask();
                taskChild.setName(task.Name);
                taskChild.setCalendarUniqueID(calendar.getUniqueID());
                taskChild.setGUID(UUID.fromString(task.Id.ToString()));
                taskChild.setText(29, task.Id.ToString());
                taskChild.setText(27, task.AssignByUsername);
                if (!task.IsHaveChild)
                {
                    if (task.StartTime.HasValue)
                    {
                        taskChild.setStart(df.parse(task.StartTime.Value.ToString("dd/MM/yyyy HH:mm")));
                        taskChild.setActualStart(df.parse(task.StartTime.Value.ToString("dd/MM/yyyy HH:mm")));
                    }
                    if (task.EndTime.HasValue)
                    {
                        taskChild.setFinish(df.parse(task.EndTime.Value.ToString("dd/MM/yyyy HH:mm")));
                    }
                    if (task.FinishiDate.HasValue)
                    {
                        taskChild.setActualFinish(df.parse(task.FinishiDate.Value.ToString("dd/MM/yyyy HH:mm")));
                    }

                    if (task.EndTime.HasValue && task.StartTime.HasValue)
                    {
                        net.sf.mpxj.Duration duration = calendar.getWork(taskChild.getStart(), taskChild.getFinish(), net.sf.mpxj.TimeUnit.DAYS);
                        taskChild.setDuration(duration);
                    }
                    if (task.FinishiDate.HasValue)
                    {
                        net.sf.mpxj.Duration duration = calendar.getWork(taskChild.getActualStart(), taskChild.getActualFinish(), net.sf.mpxj.TimeUnit.DAYS);
                        taskChild.setActualDuration(duration);
                    }
                    if (task.PercentFinish.HasValue)
                    {
                        taskChild.setPercentageComplete(NumberHelper.getDouble(task.PercentFinish.Value));
                    }
                    List<UserExcelDto> listWorkers = new List<UserExcelDto>();
                    listWorkers.AddRange(task.MainWorkers);
                    listWorkers.AddRange(task.Supporters);
                    listWorkers = listWorkers.Distinct().ToList();
                    if (task.StartTime.HasValue)
                    {
                        foreach (var woker in listWorkers)
                        {
                            var rs = resources.Where(e => e.getNtAccount() == woker.Username).FirstOrDefault();
                            net.sf.mpxj.ResourceAssignment assignment1 = taskChild.addResourceAssignment(rs);
                            //assignment1.setUnits(NumberHelper.getDouble((8 / 24) * 100.0));
                            assignment1.setStart(taskChild.getStart());
                            assignment1.setFinish(taskChild.getFinish());
                            assignment1.setGUID(UUID.fromString(woker.ID.ToString()));
                            assignment1.setText(28, woker.ID.ToString());
                            if (woker.FinishedDate.HasValue)
                            {
                                assignment1.setActualFinish(df.parse(woker.FinishedDate.Value.ToString("dd/MM/yyyy HH:mm")));
                            }

                            assignment1.setWork(taskChild.getDuration());
                            var percent = woker.PercentFinish.HasValue ? woker.PercentFinish.Value : 0;
                            assignment1.setPercentageWorkComplete(NumberHelper.getDouble(percent));
                            var dayt = taskChild.getDuration().getDuration();
                            var pert = assignment1.getPercentageWorkComplete();
                            var durt = dayt * NumberHelper.getDouble(pert) / 100;
                            assignment1.setActualWork(net.sf.mpxj.Duration.getInstance(durt, net.sf.mpxj.TimeUnit.DAYS));
                            assignment1.setRemainingWork(net.sf.mpxj.Duration.getInstance(dayt - durt, net.sf.mpxj.TimeUnit.DAYS));
                        }
                    }
                }
                if (task.IsHaveChild)
                {
                    AddTaskForMSProject(taskChild, task.Childrens, resources, calendar);
                }
            }
        }
    }
}