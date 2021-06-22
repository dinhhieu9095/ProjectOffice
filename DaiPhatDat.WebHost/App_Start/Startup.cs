using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System.Web;
using System.Web.Mvc;

[assembly: OwinStartup(typeof(DaiPhatDat.WebHost.Startup))]

namespace DaiPhatDat.WebHost
{
    public partial class Startup
    {
        UrlHelper url = new UrlHelper(HttpContext.Current.Request.RequestContext);
        public void ConfigureAuth(IAppBuilder app)
        {
            // Enable the application to use a cookie to store information for the signed in user
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                //AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                //LoginPath = new PathString(url.Action(MVC.Controllers.HomeController.IndexAction, MVC.Controllers.HomeController.ControllerName))
            });
            // Use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");

            //app.UseGoogleAuthentication();
        }
    }
}
