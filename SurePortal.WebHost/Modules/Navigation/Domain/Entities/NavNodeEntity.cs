using SurePortal.Core.Kernel.Domain.Entities;
using SurePortal.WebHost.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurePortal.WebHost.Modules.Navigation.Domain.Entities
{
    public class NavNodeEntity : BaseMoreEntity
    {
        public CommonValues.NavNode.NavNodeStatus Status { get; set; }
        public string Areas { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Params { get; set; }
        public string ResourceId { get; set; }
        public string NameEN { get; set; }
        public string Description { get; set; }
        public string Method { get; set; }
    }
}