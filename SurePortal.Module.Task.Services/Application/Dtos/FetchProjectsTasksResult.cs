using SurePortal.Core.Kernel.Helper;
using SurePortal.Module.Task.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurePortal.Module.Task.Services
{
    public class FetchProjectsTasksResult
    {
        public Guid Id { get; set; }

        public Guid? ParentId { get; set; }

        public Guid? ProjectId { get; set; }

        public string Type { get; set; }

        public string Status { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
         
        public string Content { get; set; }

        public string Name { get; set; }
        public string ProjectName { get; set; }

        public string TaskName
        {
            get
            {
                return Name;
            }
        }

        public bool HasChildren { get; set; }

        public DateTime? FromDate { get; set; }

        public string FromDateFormat { get; set; }
        public string DateFormat
        {
            get
            {
                string strReturn = "";
                if (this.FromDate.HasValue && this.ToDate.HasValue)
                {
                    strReturn =string.Concat( (this.FromDate.Value.Year == DateTime.Today.Year) ? ConvertToStringExtensions.DateToStringLocal(this.FromDate, "dd MMM") : ConvertToStringExtensions.DateToStringLocal(this.FromDate, "dd MMM, yyyy")," - ",(this.ToDate.Value.Year == DateTime.Today.Year) ? ConvertToStringExtensions.DateToStringLocal(this.ToDate, "dd MMM") : ConvertToStringExtensions.DateToStringLocal(this.ToDate, "dd MMM, yyyy")); //+ " - "+ this.ToDate.Value.Year == DateTime.Today.Year ? ConvertToStringExtensions.DateToStringLocal(this.ToDate, "dd MMM") : ConvertToStringExtensions.DateToStringLocal(this.ToDate, "dd MMM, yyyy");
                }
                return strReturn;
            }
        }

        public string FromDateText
        {
            get
            {
                string strReturn = "";
                if (this.FromDate.HasValue)
                {
                    strReturn = FromDate.Value.ToString("dd/MM/yyyy");
                }
                return strReturn;
            }
        }

        public string ToDateText
        {
            get
            {
                string strReturn = "";
                if (this.ToDate.HasValue)
                {
                    strReturn = ToDate.Value.ToString("dd/MM/yyyy");
                }
                return strReturn;
            }
        }

        public DateTime? ToDate { get; set; }

        public DateTime? FinishedDate { get; set; }
        public string Process
        {
            get
            {
                string result = string.Empty;
                if (TaskItemStatusId == Entities.TaskItemStatusId.Finished && FinishedDate.HasValue && ToDate.HasValue && FinishedDate <= ToDate)
                {
                    result = "in-due-date";
                }
                else if (TaskItemStatusId == Entities.TaskItemStatusId.Finished && FinishedDate.HasValue && ToDate.HasValue && FinishedDate > ToDate)
                {
                    result = "out-of-date";
                }
                else if (TaskItemStatusId != Entities.TaskItemStatusId.Finished && ToDate.HasValue && ToDate.Value >= DateTime.Now && ToDate.Value <= DateTime.Now.AddDays(2))
                {
                    result = "near-of-date";
                }
                else if (TaskItemStatusId != Entities.TaskItemStatusId.Finished && ToDate.HasValue && ToDate >= DateTime.Now)
                {
                    result = "in-due-date";
                }
                else
                {
                    result = "out-of-date";
                }
                return result;
            }
        }

        public TaskItemStatusId? TaskItemStatusId { get; set; }

        public string ToDateFormat { get; set; }

        public Guid? ApprovedBy { get; set; }

        public Guid? DepartmentId { get; set; }

        public Guid? UserId { get; set; }
        public List<Guid> AssignToIds { get; set; }

        public Guid? AssignBy { get; set; }
        public Guid? AssignTo { get; set; }
        public string AssignToFullName { get; set; }
        public string AssignToJobTitle { get; set; }
        public Guid? AssignToDeparmentId { get; set; }

        public string FullName { get; set; }
        public string UserFullName
        {
            get
            {
                return FullName;
            }
        }

        public string JobTitle { get; set; }
        public string JobTitleName
        {
            get
            {
                return JobTitle;
            }
        }
        public string UserViews { get; set; }
        public List<UserModel> Users { get; set; }

        public int CurrentPage { get; set; }
        public bool? IsGroupLabel { get; set; }

        public int CountTask { get; set; }
        public int PageSize { get; set; }

        public int TotalRecord { get; set; }

        public string ColorStatus =>
                 TaskInDueDate.Main(ToDate, FinishedDate, TaskItemStatusId) ? "blue" : "red";

        public double? PercentFinish { get; set; }
        public bool HasPagination { get; set; } // dùng trên view table
        public bool HasLoading { get; set; } // dùng trên view table
        public bool DragDrop { get; set; } // dùng trên view table // Neu User hien tai == voi UserAssignBy
        public Guid? CreatedBy { get; set; }
        public string ProcessClass { get; set; } //dung view grantt
        public string ProcessHtml { get; set; }//dung view grantt
        public string UsersPrimary { get; set; }//  xu ly chinh or nguoi quan ly
        public string UsersSecond { get; set; }//  nguoi theo doi or nguoi xu ly phu
        public string UsersThird { get; set; }// // nguoi de biet
        public string MemberHtml { get; set; }//
        public string StatusHtml { get; set; }//
        public string DateFormatHtml { get; set; }//
        
    }
    public class UserModel
    {
        public Guid ID { get; set; }
        public string LoginName { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Gender { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string HomePhone { get; set; }
        public string Ext { get; set; }
        public DateTime? BirthDate { get; set; }
        public string UserCode { get; set; }
        public string LangID { get; set; }
        public bool IsActive { get; set; }
        public string UserName { get; set; }
        public string DepartmentName { get; set; }
        public Guid DepartmentId { get; set; }

        public string JobTitle { get; set; }
    }
    public class TaskInDueDate
    {
        public static bool Main(DateTime? toDate, DateTime? finishDate, TaskItemStatusId? statusId)
        {
            var isFinished = statusId == TaskItemStatusId.Finished || statusId == TaskItemStatusId.Cancel;
            if ((!isFinished && toDate < DateTime.Now) || (isFinished && toDate < finishDate))
                return false;

            return true;
        }
        public static bool Main(DateTime? toDate, DateTime? finishDate, ProjectStatusId? statusId)
        {
            var isFinished = statusId != ProjectStatusId.Finished;
            if ((!isFinished && toDate < DateTime.Now) || (isFinished && toDate < finishDate))
                return false;

            return true;
        }
        public static string HtmlProcess(string process, string processClass, string strHtmlProcess, double? percentProcess)
        {
             string strReturn = "";
            string strPercentProcess = percentProcess.HasValue ? (percentProcess.Value.ToString()) : "0" ;
            string strPercentProcess3 = percentProcess.HasValue ? (percentProcess.Value.ToString()+"%") : "";
            strReturn = string.Format(strHtmlProcess, (processClass!=string.Empty) ? (" "+ processClass) :"" , (process!=string.Empty) ? (" "+ process) :"", strPercentProcess, strPercentProcess3);
            return strReturn;
        }
        public static string HtmlStatus(string htmlStatus, string statusName,string processClass, double? percentProcess)
        {
            string strReturn = "";
            string str1 = percentProcess.HasValue ? "<div class='process-number'>"+ (percentProcess.Value.ToString() + "%" + "</div>") : "";
            string str0 = percentProcess.HasValue ? (percentProcess.Value.ToString()) : "0";
            strReturn = string.Format(htmlStatus, str0, str1, statusName, processClass);
            return strReturn;
        }
    }
    public class FetchProjectsTasksQuery
    {
        public FetchProjectsTasksQuery()
        {
            CurrentPage = 1;
            PageSize = 20;
        }

        public Guid? ParentId { get; set; }

        public string Search { get; set; }

        public int CurrentPage { get; set; }

        public int PageSize { get; set; }

        public bool IsSearching { get; set; }

        public string OtherParams { get; set; }
        public string CurrentDate { get; set; }
        public string CurrentView { get; set; }
    }

    public class MoveItemDto
    {
        public string Result { get; set; }
        public string Message { get; set; }
        public string ParentId { get; set; }
    }
    public class TrackingBreadCrumbViewParentDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ProjectId { get; set; }
        public int STT { get; set; }

    }
    public class UserProcessViewDto
    {
        public int ViewType { get; set; }
        public string ViewTypeName { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public string SrcImage { get; set; }
        public string JobTitle { get; set; }
        public string DeptName { get; set; }
        public int STT { get; set; }
    }
}
