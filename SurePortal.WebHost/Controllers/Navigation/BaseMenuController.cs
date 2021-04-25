using AutoMapper;
using SurePortal.Core.Kernel.Orgs.Application;
using SurePortal.WebHost.Modules.Navigation.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SurePortal.WebHost.Controllers
{
    public class BaseMenuController : Controller
    {
        protected readonly IMenuService _menuService;
        protected readonly IMapper _mapper;
        protected readonly IUserDepartmentServices _userDepartmentServices;
        protected readonly IUserServices _userServices;

        public BaseMenuController(IMenuService menuService, IMapper mapper)
        {
            MenuService = menuService;
            _mapper = mapper;
        }

        public BaseMenuController(IMenuService menuService, IMapper mapper, IUserDepartmentServices userDepartmentServices, IUserServices userServices)
        {
            _menuService = menuService;
            _mapper = mapper;
            _userDepartmentServices = userDepartmentServices;
            _userServices = userServices;
        }

        public IMenuService MenuService { get; }
        public IMapper Mapper { get; }
    }
}