using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DaiPhatDat.Module.Task.Entities
{
    public enum FilterTaskItemType
    {
        [Display(Name = "Tất cả")]
        All = 0,
        [Display(Name = "Phòng ban")]
        Department = 1,
        [Display(Name = "Cá nhân")]
        User = 2,
    }
}