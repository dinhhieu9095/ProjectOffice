using DaiPhatDat.Core.Kernel.Application.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace DaiPhatDat.WebHost.Common
{
    public sealed class CommonValues
    {
        private static readonly Lazy<CommonValues>
               lazy = new Lazy<CommonValues>
                   (() => new CommonValues());
        public static CommonValues Instance { get { return lazy.Value; } }
        public class Menu
        {
            public enum Layout : byte
            {
                /// <summary>
                /// Menu theo Modules
                /// </summary>
                [Display(Name = "Menu Modules")]
                Modules = 0,
                /// <summary>
                /// Menu top
                /// </summary>
                [Display(Name = "Menu Top")]
                Top = 1,
                /// <summary>
                /// Menu left
                /// </summary>
                [Display(Name = "Menu Left")]
                Left = 2
            }
            public enum MenuStatus : byte
            {
                /// <summary>
                /// Không cấu hình menu(action Ajax,hoặc)
                /// </summary>
                [Display(Name = "Tất cả")]
                Public = 0,
                /// <summary>
                /// Cấu hình theo quyền người dùng
                /// </summary>
                [Display(Name = "Theo quyền người dùng")]
                Private = 2
            }
        }
        public class NavNode
        {
            public enum NavNodeStatus : byte
            {
                /// <summary>
                /// Không cấu hình menu(action Ajax,hoặc)
                /// </summary>
                [Display(Name = "Bỏ qua")]
                Ignore = 0,
                /// <summary>
                /// Lựa chọn không theo quyền người dùng
                /// </summary>
                [Display(Name = "Không theo quyền")]
                Public = 1,
                /// <summary>
                /// Cấu hình theo quyền người dùng
                /// </summary>
                [Display(Name = "Theo quyền người dùng")]
                Private = 2
            }
        }

        public enum Action
        {
            Insert = 0,
            Update = 1,
            Delete = 2,
            ChangeActive = 3
        }

    }
}