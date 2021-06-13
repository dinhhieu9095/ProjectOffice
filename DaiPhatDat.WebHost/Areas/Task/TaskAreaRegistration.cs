using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaiPhatDat.Module.Task.Web
{
    public class TaskAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Task";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Task_default",
                "Task/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }, namespaces: new[] {
                "DaiPhatDat.Module.Task.Web"
            }
            );
        }
    }
}