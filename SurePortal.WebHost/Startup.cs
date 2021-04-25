using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using System.Web;

[assembly: OwinStartup(typeof(SurePortal.WebHost.Startup))]
namespace SurePortal.WebHost
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
            var idProvider = new UserIdProvider();
            GlobalHost.DependencyResolver.Register(typeof(IUserIdProvider), () => idProvider);
        }
        public class UserIdProvider : IUserIdProvider
        {
            public string GetUserId(IRequest request)
            {
                var userId = HttpContext.Current.User.Identity.Name;
                return userId.ToString();
            }
        }
    }
}