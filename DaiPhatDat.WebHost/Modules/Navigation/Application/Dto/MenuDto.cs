using DaiPhatDat.Core.Kernel.Domain.ValueObjects;
using DaiPhatDat.Core.Kernel.Mapper;
using DaiPhatDat.WebHost.Common;
using DaiPhatDat.WebHost.Modules.Navigation.Domain.Entities;
using DaiPhatDat.WebHost.Modules.Navigation.Domain.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DaiPhatDat.WebHost.Modules.Navigation.Application.Dto
{
    public class MenuDto : MenuEntity, IMapping<Menu>
    {
    }

    public class MenuPagingDto : MenuDto
    {
        public string Areas { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Params { get; set; }
        public string Link
        {
            get
            {
                string area = !string.IsNullOrEmpty(this.Areas) ? "/" + this.Areas : "";
                string controller = !string.IsNullOrEmpty(this.Controller) ? "/" + this.Controller : "";
                string action = !string.IsNullOrEmpty(this.Action) ? "/" + this.Action : "";
                string param = !string.IsNullOrEmpty(this.Params) ? "?" + this.Params : "";
                string link = area + controller + action + param;
                return link;
            }
        }

    }
    public class SearchMenuDto : BaseSearchDto
    {
        public string UserName { get; set; }
    }

    public class MenuTreeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
    }
}