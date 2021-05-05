using DaiPhatDat.Core.Kernel.Application.Helpers;
using DaiPhatDat.Core.Kernel.Application.Utilities;
using DaiPhatDat.Core.Kernel.Logger.Application;
using DaiPhatDat.Core.Kernel.Orgs.Application;
using DaiPhatDat.Core.Kernel.Orgs.Application.Dto;
using System.Linq;
using System.Web.Mvc;

namespace DaiPhatDat.Core.Kernel.Controllers
{
    [Authorize]
    [SessionState(System.Web.SessionState.SessionStateBehavior.ReadOnly)]
    public class CoreController : Controller
    {
        protected readonly ILoggerServices _loggerServices;
        protected readonly IUserServices _userService;
        protected readonly IUserDepartmentServices _userDepartmentServices;
        public CoreController(ILoggerServices loggerServices, IUserServices userService,
            IUserDepartmentServices userDepartmentServices)
        {
            _loggerServices = loggerServices;
            _userService = userService;
            _userDepartmentServices = userDepartmentServices;
        }
        protected override IActionInvoker CreateActionInvoker()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                ViewBag.UserModel = CurrentUser;
            }
            return base.CreateActionInvoker();
        }

        private UserDto _currentUser = null;
        public UserDto CurrentUser
        {
            get
            {
                if (_currentUser != null)
                {
                    return _currentUser;
                }

                var currentUserName = WebUtils.GetUserNameFromContext(User.Identity.Name);

                _loggerServices.WriteError("User from Identity: " + currentUserName);

                _currentUser = _userService.GetByUserName(currentUserName);

                _loggerServices.WriteError("User from Database: " + _currentUser?.UserName);

                if (_currentUser == null)
                {
                    RedirectToAction("Logout", "Account");
                }

                if (_currentUser != null)
                {
                    // get jobtitles
                    var allDepts = _userDepartmentServices.GetCachedUserDepartmentsByUser(_currentUser.Id);
                    if (allDepts != null)
                    {
                        //_currentUser.JobTitles = allDepts.Where(w => !string.IsNullOrEmpty(w.JobTitleName))
                        //    .Select(s => s.JobTitleName).ToList();

                        //_currentUser.DepartmentNames = allDepts.Select(s => s.DeptName).ToList();

                        _currentUser.Departments = allDepts.Select(s =>new DepartmentCompact{
                            Id = s.DeptID,
                            Name= s.DeptName,
                            JobTitle = s.JobTitleName,
                            OrderNumber = s.OrderNumber
                        }).OrderBy(x => x.OrderNumber).ToList();

                        _loggerServices.WriteError("Jobtitle of User: " + _currentUser.UserName);
                        _loggerServices.WriteError("Department of User: " + _currentUser.Departments?.FirstOrDefault()?.Name);
                    }
                    _currentUser.Permissions = _userService.GetUserPermission(_currentUser.UserName);
                }

                return _currentUser;
            }
        }
        protected ActionResult CamelCaseJson(object data)
        {
            return new CamelCaseJsonResult(data, JsonRequestBehavior.AllowGet);
        }
    }
}