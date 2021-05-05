using DaiPhatDat.Core.Kernel.Domain.Entities;
using DaiPhatDat.Core.Kernel.Domain.ValueObjects;
using DaiPhatDat.WebHost.Common;
using DaiPhatDat.WebHost.Modules.Navigation.Application.Dto;
using DaiPhatDat.WebHost.Modules.Navigation.Domain.POCO;
using System;

namespace DaiPhatDat.WebHost.Modules.Navigation.Domain.Entities
{
    public class MenuEntity : BaseMoreEntity
    {
        public string Code { get; set; }
        public Guid? NavNodeId { get; set; }
        public Guid? ParentId { get; set; }
        public Guid? ModuleId { get; set; }
        public SurePortalModules? TypeModule { get; set; }
        public string Layout { get; set; }
        public CommonValues.Menu.MenuStatus Status { get; set; }
        public string Target { get; set; }
        public string Icon { get; set; }
        public int Order { get; set; }
        public string Roles { get; set; }
        public string GroupOrUsers { get; set; }
    }
}