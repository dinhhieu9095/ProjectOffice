using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DaiPhatDat.Module.Task.Web
{
    public class BaseFilterModel
    {
        public string KeyWord { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}