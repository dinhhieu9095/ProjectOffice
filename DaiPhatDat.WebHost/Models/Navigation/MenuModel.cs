using DaiPhatDat.Core.Kernel.Domain.ValueObjects;
using DaiPhatDat.Core.Kernel.Mapper;
using DaiPhatDat.WebHost.Common;
using DaiPhatDat.WebHost.Modules.Navigation.Application.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace DaiPhatDat.WebHost.Models.Navigation
{
    #region List

    public class MenuIndexModel
    {
        public string Controller
        {
            get
            {
                return Constant.Controller.Menu;
            }
        }
        public string TemplateBreadcrumbURL => DirectTemplate.Breadcrumb("Menu");
        public string TemplateSearchURL => DirectTemplate.Search("Menu");
        public IEnumerable<SelectListItem> LstProvider { get; set; }
        #region ctor
        public MenuIndexModel()
        {

            LstProvider = new List<SelectListItem>();

        }
        #endregion
    }
    public class MenuPagingModel : BaseGetPaging
    {
        public List<MenuModel> LstData { get; set; }
        public MenuPagingModel()
        {
            LstData = new List<MenuModel>();
        }
    }
    public class MenuModel : MenuPagingDto
    {
        public string DisplayTypeModule
        {
            get
            {
                string _display = string.Empty;
                if (TypeModule.HasValue)
                {
                    switch (TypeModule)
                    {
                        case SurePortalModules.Org:
                            _display = "Sơ đồ tổ chức";
                            break;
                        case SurePortalModules.BW:
                            _display = "Trình ký điện tử";
                            break;
                        case SurePortalModules.Doc:
                            _display = "Quản lý văn bản";
                            break;
                        case SurePortalModules.Contract:
                            _display = "Quản lý hợp đồng";
                            break;
                        case SurePortalModules.Workspace:
                            _display = "Môi trường làm việc";
                            break;
                        default:
                            break;
                    }
                }
                return _display;
            }
        }
        public string DisplayStatus
        {
            get
            {
                string _display = string.Empty;
                switch (Status)
                {
                    case CommonValues.Menu.MenuStatus.Public:
                        _display = "Public";
                        break;
                    case CommonValues.Menu.MenuStatus.Private:
                        _display = "Private";
                        break;
                    default:
                        break;
                }
                return _display;
            }
        }
        public string DisplayActiveFag
        {
            get
            {
                string _display = string.Empty;
                switch (ActiveFag)
                {
                    case Core.Kernel.CommonUtility.ActiveFag.Active:
                        _display = "Kích hoạt";
                        break;
                    case Core.Kernel.CommonUtility.ActiveFag.Deactive:
                        _display = "Tạm khóa";
                        break;
                    case Core.Kernel.CommonUtility.ActiveFag.Delete:
                        _display = "Xóa/Hủy";
                        break;
                    default:
                        break;
                }
                return _display;
            }
        }
        public string DisplayLink
        {
            get
            {
                if (NavNodeId.HasValue)
                {
                    StringBuilder _display = new StringBuilder();
                    if (!string.IsNullOrEmpty(Areas))
                    {
                        _display.Append(string.Concat("/", Areas));
                    }
                    if (!string.IsNullOrEmpty(Controller))
                    {
                        _display.Append(string.Concat("/", Controller));
                    }
                    if (!string.IsNullOrEmpty(Action))
                    {
                        _display.Append(string.Concat("/", Action));
                    }
                    if (!string.IsNullOrEmpty(Params))
                    {
                        _display.Append(string.Concat("?", Params));
                    }
                    return _display.ToString();
                }
                return string.Empty;
            }
        }
    }
    #endregion

    #region Insert,update
    public class MenuUpdateModel : IMapping<MenuDto>
    {
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "Tên hiển thị")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        [Display(Name = "Menu cấp")]
        public Guid? ParentId { get; set; }
        public Guid? NavNodeId { get; set; }
        [Display(Name = "Nhóm Module")]
        public SurePortalModules? TypeModule { get; set; }
        [Display(Name = "Giao diện")]
        public string Layout { get; set; }
        [Display(Name = "Quyền hiển thị")]
        public CommonValues.Menu.MenuStatus Status { get; set; }
        [Display(Name = "Phân quyền theo chức năng")]
        public string Roles { get; set; }
        [Display(Name = "Phân quyền theo nhóm và người dùng")]
        public string GroupOrUsers { get; set; }
        public List<SelectListItem> ListGroupOrUser { get; set; }
        [Display(Name = "Target")]
        public string Target { get; set; }
        [Display(Name = "Icon")]
        public string Icon { get; set; }
        [Display(Name = "Số thứ tự")]
        public int Order { get; set; }
        [Display(Name = "Link")]
        public string Link { get; set; }
        public string TemplateURL => DirectTemplate.InsertOrUpdate("Menu", "Menu");
        public string TemplateBreadcrumbURL => DirectTemplate.Breadcrumb("Menu");
        public string navAutocomplete { get; set; }
        public CommonValues.Action Action { get; set; }
        public IEnumerable<SelectListItem> LstTarget { get; set; }
        public IEnumerable<SelectListItem> LstMenu { get; set; }
        public IEnumerable<SelectListItem> ListRole { get; set; }
        public MenuUpdateModel()
        {
            Action = CommonValues.Action.Insert;
            LstTarget = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "_blank",
                    Value = "_blank"
                },
                    new SelectListItem
                {
                    Text = "_self",
                    Value = "_self"
                    } ,
                    new SelectListItem
                {
                    Text = "_parent",
                    Value = "_parent"
                },
                new SelectListItem
                {
                    Text = "_top",
                    Value = "_top"
                },
                    new SelectListItem
                {
                    Text = "framename",
                    Value = "framename"
                    }
            };
            LstMenu =  new List<SelectListItem>();  
            ListRole =  new List<SelectListItem>();
            ListGroupOrUser =  new List<SelectListItem>();
        }
    }

    #endregion
}