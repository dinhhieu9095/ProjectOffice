using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DaiPhatDat.Module.Task.Entities
{
    public enum FilterTaskItemStatus
    {
        [Display(Name = "Tất cả")]
        All = -1,
        [Display(Name = "Mới")]
        New = 0,
        [Display(Name = "Đang xử lý")]
        InProcess = 1,
        [Display(Name = "Báo cáo")]
        Report = 2,
        [Display(Name = "Gia hạn")]
        Extend = 5,
        [Display(Name = "Kết thúc")]
        Finished = 4
    }
}