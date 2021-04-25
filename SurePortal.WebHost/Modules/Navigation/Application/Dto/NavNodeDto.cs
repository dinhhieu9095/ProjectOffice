using SurePortal.Core.Kernel.Mapper;
using SurePortal.WebHost.Modules.Navigation.Domain.Entities;
using SurePortal.WebHost.Modules.Navigation.Domain.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurePortal.WebHost.Modules.Navigation.Application.Dto
{
    public class NavNodeDto : NavNodeEntity, IMapping<NavNode>
    {
    }

    public class NavNodePagingDto: NavNodeDto
    {

    }
}