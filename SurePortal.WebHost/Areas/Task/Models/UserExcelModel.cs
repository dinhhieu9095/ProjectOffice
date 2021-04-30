using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurePortal.Module.Task.Web
{
    public class UserExcelModel
    {
        public string Color { get; set; }
        public Guid? DeptID { get; set; }
        public Guid? ID { get; set; }
        public string Text { get; set; }
        public string Username { get; set; }
        public double? PercentFinish { get; set; }
        public int? Type { get; set; }
        public bool? CanProcess { get; set; }
        public DateTime? FinishedDate { get; set; }
    }
}