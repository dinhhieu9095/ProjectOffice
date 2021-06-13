using System.Web.Mvc;
using System.Web.Routing;

namespace DaiPhatDat.WebHost
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            // for areas route
            routes.MapMvcAttributeRoutes();
            AreaRegistration.RegisterAllAreas();

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}/{id1}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional, id1 = UrlParameter.Optional },
                new string[] { "DaiPhatDat.WebHost.Controllers" }
            );

        }
    }
}
