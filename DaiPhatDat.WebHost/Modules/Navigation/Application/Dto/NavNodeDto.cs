using DaiPhatDat.Core.Kernel.Mapper;
using DaiPhatDat.WebHost.Modules.Navigation.Domain.Entities;
using DaiPhatDat.WebHost.Modules.Navigation.Domain.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DaiPhatDat.WebHost.Modules.Navigation.Application.Dto
{
    public class NavNodeDto : NavNodeEntity, IMapping<NavNode>
    {
    }

    public class NavNodePagingDto: NavNodeDto
    {

    }
}