using System.Web.Mvc;

namespace SurePortal.WebHost.Controllers
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