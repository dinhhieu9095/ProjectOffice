using DaiPhatDat.Core.Kernel.Mapper;
using DaiPhatDat.Module.Task.Entities;
using DaiPhatDat.Module.Task.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;

namespace DaiPhatDat.Module.Task.Web
{
    public class TaskDetailModel
    {
        //Mã công việc
        public Guid Id { get; set; }
        // là nhóm hay là Task
        public bool? IsGroupLabel { get; set; }
        //Tên công việc
        public string TaskName { get; set; }
        //Nội dung
        public string Content { get; set; }
        //Ngày bắt đầu
        public string FromDate { get; set; }
        //Ngày kết t húc
        public DateTime? ToDate { get; set; }
        public string ToDateFormat { get; set; }
        //Ngày hoàn thành
        public string FinishedDate { get; set; }
        //Mã người Người giao
        public Guid? AssignByID { get; set; }
        //Tên đăng nhập Người giao
        public string AssignByJobTitle { get; set; }
        //Họ và tên Người giao
        public string AssignByFullName { get; set; }
        //Mã người Người Nhận
        public Guid? AssignToID { get; set; }
        //Tên đăng nhập Người Nhận
        public string AssignToJobTitle { get; set; }
        //Họ và tên Người Nhận
        public string AssignToFullName { get; set; }
        // Ngày cập nhật
        public Double? PercentFinish { get; set; }
        //Trạng thái
        public string StatusName { get; set; }
        //string 
        public string DateFormat { get; set; }
        //Trạng thái của Task
        public TaskItemStatusId? TaskItemStatusId { get; set; }
        public string AvatarUrl { get; set; }
        public string Color
        {
            get
            {
                if (TaskItemStatusId != Entities.TaskItemStatusId.Finished
                       && ToDate != null
                       && ToDate.GetValueOrDefault() < DateTime.Now)
                    return "out-of-date";
                else if (ToDate != null
                       && (ToDate.GetValueOrDefault() - DateTime.Now).Days < 3)
                {
                    return "near-of-date";
                }
                else
                    return "in-due-date";
            }
        }
        public ICollection<AttachmentModel> Attachments { get; set; }
    }
}