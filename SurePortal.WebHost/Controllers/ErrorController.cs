using System.Web.Mvc;

namespace DaiPhatDat.WebHost.Controllers
{
    public class ErrorController : Controller
    {
        // GET: 404-NotFound
        public ActionResult NotFound()
        {
            return View();
        }

        // GET: 500-InternalServerError
        public ActionResult InternalServerError()
        {
            return View();
        }
    }
}