using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurePortal.WebHost.Modules.Navigation.Application.Dto
{
    public class BaseSearchDto
    {
        public string KeyWord { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public BaseSearchDto()
        {
            PageIndex = 1;
            PageSize = 20;

        }
    }
}