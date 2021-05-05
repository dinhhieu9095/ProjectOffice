using DaiPhatDat.Core.Kernel.Application;
using DaiPhatDat.Core.Kernel.Application.ViewEngines;
using DaiPhatDat.Core.Kernel.Logger.Application;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web;
using DaiPhatDat.Core.Kernel.Resources.Application.Dto;
using System.Collections.Generic;

namespace DaiPhatDat.WebHost
{
    public class App : HttpApplication
    {
        /// <summary>
        /// Resource
        /// </summary>
        public static Dictionary<string, ResourceDto> DicResources { get; set; }
        /// <summary>
        /// PageSize
        /// </summary>
        public const int PageSize = 20;
        ///// <summary>
        ///// CacheService
        ///// </summary>
        //private static ResourceCache cacheProvider;
        //public static ResourceCache CacheProvider
        //{
        //    get
        //    {
        //        if (cacheProvider == null)
        //        {
        //            cacheProvider = new ResourceCache();
        //        }
        //        return cacheProvider;
        //    }
        //    set
        //    {
        //        cacheProvider = value;
        //    }
        //}
        private ILoggerServices _loggingService => ServiceFactory.Get<ILoggerServices>();
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            GlobalConfiguration.Configure(WebApiConfig.Register);

            RouteConfig.RegisterRoutes(RouteTable.Routes);

            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new DynamicViewEngine(""));
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = DateTimeZoneHandling.Local,
                DateParseHandling = DateParseHandling.DateTimeOffset,
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            JsonConvert.DefaultSettings = () => jsonSerializerSettings;
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling =
                ReferenceLoopHandling.Ignore;
            GlobalConfiguration.Configuration.EnsureInitialized();

            log4net.Config.XmlConfigurator.Configure();
        }
        protected void Session_Start(Object sender, EventArgs e)
        {
        }
        protected void Application_Error(object sender, EventArgs e)
        {
            var lastError = Server.GetLastError();
            // Don't flag missing pages or changed urls, as just clogs up the log
            if (!lastError.Message.Contains("was not found or does not implement IController") &&
                !lastError.Message.Contains("did not return a controller for the name"))
            {
                _loggingService.WriteError(lastError.ToString());
            }
        }
    }

}
