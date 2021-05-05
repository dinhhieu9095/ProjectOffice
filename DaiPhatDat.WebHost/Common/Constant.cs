using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DaiPhatDat.WebHost
{
    public sealed class Constant
    {
        #region Controller

     
        public class Controller
        {
            public const string Menu = "menu";
            public const string NavNode = "navnode";
        }
        #endregion

        #region Action

      
        public class Action
        {
            public const string Index = "index";
            public const string SearchUser = "SearchUser";
            public const string GetPaging = "getpaging";
            public const string Insert = "insert";
            public const string Update = "update";
            public const string Delete = "delete";
            public const string ChangeActive = "changeactive";
            public const string DownloadFile = "downloadfile";
            public const string NavNodeInfo = "navnodeinfo";
            public const string ConvertNameToURL = "convertnametourl";
        }
        #endregion

        #region Parameter
        public class Parameter
        {
            public const string Id = "id=";
            public const string PageIndex = "pageindex=";
        }
        #endregion      
    }
}