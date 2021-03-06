using DaiPhatDat.Core.Kernel.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DaiPhatDat.WebHost.Modules.Navigation.Domain.Entities
{
    public class MenuRoleEntity : BaseMoreEntity
    {
        public Guid RoleId { get; set; }
        public Guid MenuId { get; set; }
    }
}