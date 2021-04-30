using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurePortal.Module.Task.Web
{
    public class AdvanceFilterModel : BaseFilterModel
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string CurrentDate { get; set; }
        public string GranttView { get; set; }
    }
    public class MoveFilterModel 
    {
        public string IdSource { get; set; }
        public string IdDestination { get; set; }
        public string TaskType { get; set; }
        public string MoveType { get; set; }
    }
}