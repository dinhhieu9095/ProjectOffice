using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static DaiPhatDat.WebHost.Constant;

namespace DaiPhatDat.WebHost.Models
{
    public class BaseGetPaging
    {
        public bool IsSuperAdmin { get; set; }
        public int PageIndex { get; set; }
        public int TotalPage { get; set; }
        public int PageSize { get; set; }
        public BaseGetPaging()
        {
            PageSize = App.PageSize;
        }
    }

    public sealed class DirectTemplate
    {
        public static string Breadcrumb(string ctrl)
        {
            return string.Concat("~/Views/", ctrl, "/Template/Breadcrumb.cshtml");
        }
        public static string Search(string ctrl)
        {
            return string.Concat("~/Views/", ctrl, "/Template/Search.cshtml");
        }

        public static string InsertOrUpdate(string ctrl, string url)
        {
            return string.Concat("~/Views/", ctrl, "/Template/", url, ".cshtml");
        }
    }
}