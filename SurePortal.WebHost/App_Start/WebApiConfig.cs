﻿using SurePortal.Core.Kernel.Application;
using SurePortal.Core.Kernel.Ioc;
using SurePortal.Core.Kernel.Logger.Application;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Unity;

namespace SurePortal.WebHost
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration configuration)
        {
            configuration.MapHttpAttributeRoutes();
            configuration.Services.Add(typeof(IExceptionLogger), new UnhandledExceptionLogger());
            configuration.Routes.MapHttpRoute("API Default", "_apis/{controller}/{id}",
                new { id = RouteParameter.Optional });

            var container = new UnityContainer();
            UnityHelper.BuildUnityContainer(container);
            configuration.DependencyResolver = new UnityResolver(container);
        }
    }
    public class UnhandledExceptionLogger : ExceptionLogger
    {
        private ILoggerServices _loggingService => ServiceFactory.Get<ILoggerServices>();
        public override void Log(ExceptionLoggerContext context)
        {
            _loggingService.WriteError(context.Exception.ToString());
        }
    }
}
