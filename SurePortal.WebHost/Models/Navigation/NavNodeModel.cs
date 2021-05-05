using DaiPhatDat.Core.Kernel.Mapper;
using DaiPhatDat.WebHost.Common;
using DaiPhatDat.WebHost.Modules.Navigation.Application.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;

namespace DaiPhatDat.WebHost.Models.Navigation
{
    public class NavNodeIndexModel
    {
        public string Controller
        {
            get
            {
                return Constant.Controller.NavNode;
            }
        }
        public string TemplateBreadcrumbURL => DirectTemplate.Breadcrumb("NavNode");
        public string TemplateSearchURL => DirectTemplate.Search("NavNode");
    }
    public class NavNodePagingModel : BaseGetPaging
    {
        public List<NavNodeModel> LstData { get; set; }
        public NavNodePagingModel()
        {
            LstData = new List<NavNodeModel>();
        }
    }
    public class NavNodeModel : NavNodeDto
    {
        public string DisplayStatus
        {
            get
            {
                string _display = string.Empty;
                switch (Status)
                {
                    case CommonValues.NavNode.NavNodeStatus.Ignore:
                        _display = "Ignore";
                        break;
                    case CommonValues.NavNode.NavNodeStatus.Public:
                        _display = "Public";
                        break;
                    case CommonValues.NavNode.NavNodeStatus.Private:
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
        }
    }

    public class NavNodeInfoModel
    {
        public string Name { get; set; }
        public string Areas { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Params { get; set; }
    }


    #region Insert,update
    public class NavNodeUpdateModel : IMapping<NavNodeDto>
    {
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "Tên hiển thị")]
        public string Name { get; set; }
        [Display(Name = "Areas")]
        public string Areas { get; set; }
        [Display(Name = "Controller")]
        public string Controller { get; set; }
        [Display(Name = "Action")]
        public string Action { get; set; }
        [Display(Name = "Params")]
        public string Params { get; set; }
        [Display(Name = "Link")]
        public string Link { get; set; }
        public string TemplateURL => DirectTemplate.InsertOrUpdate("NavNode", "NavNode");
        public string TemplateBreadcrumbURL => DirectTemplate.Breadcrumb("NavNode");
        public string navAutocomplete { get; set; }

        public CommonValues.Action ModeAction { get; set; }
        public bool IsLayoutNavNode { get; set; }
        public NavNodeUpdateModel()
        {
            ModeAction = CommonValues.Action.Insert;

        }
    }
    #endregion
}