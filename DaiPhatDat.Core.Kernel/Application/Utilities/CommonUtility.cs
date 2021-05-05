using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DaiPhatDat.Core.Kernel
{
    public sealed class CommonUtility
    {
        private static readonly Lazy<CommonUtility>
              lazy = new Lazy<CommonUtility>
                  (() => new CommonUtility());
        public static CommonUtility Instance { get { return lazy.Value; } }

        public const string ConnectionSQLString = "SurePortalDbContext";

        private const string Pattern = @"\p{IsCombiningDiacriticalMarks}+";
        public static string ToUnsignString(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }
            input = input.ToLower().Trim();
            for (int i = 0x20; i < 0x30; i++)
            {
                input = input.Replace(((char)i).ToString(), " ");
            }
            input = input.Replace(".", "-");
            input = input.Replace(".", "-");
            input = input.Replace(" ", "-");
            input = input.Replace(";", "-");
            input = input.Replace(":", "-");
            input = input.Replace("  ", "-");
            Regex regex = new Regex(Pattern);
            string str = input.Normalize(System.Text.NormalizationForm.FormD);
            string str2 = regex.Replace(str, string.Empty).Replace('đ', 'd').Replace('Đ', 'D');
            while (str2.IndexOf("?") >= 0)
            {
                str2 = str2.Remove(str2.IndexOf("?"), 1);
            }
            while (str2.Contains("--"))
            {
                str2 = str2.Replace("--", "-").ToLower();
            }
            return str2;
        }

        public enum ActiveFag : byte
        {
            /// <summary>
            /// Kích hoạt
            /// </summary>
            Active = 0,
            /// <summary>
            /// Tạm khóa
            /// </summary>
            Deactive = 1,
            /// <summary>
            /// Xóa
            /// </summary>
            Delete = 2
        }
    }
}
