using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurePortal.Module.Task.Web
{
    public class EnumModulePermission
    {
        public const string Task_FullControl = "TaskFullControl";
    }

    public class EnumReportType
    {
        public const string ReportDepartment = "ReportDepartment";
        public const string ReportUser = "ReportUser";
        public const string ReportWeekly = "ReportWeekly";
        public const string ReportOnePage = "ReportOnePage";
        public const string ReportIframe = "ReportIframe";
    }
}