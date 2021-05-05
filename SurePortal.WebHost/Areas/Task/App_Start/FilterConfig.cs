using System.Web;
using System.Web.Mvc;

namespace DaiPhatDat.Module.Task.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
