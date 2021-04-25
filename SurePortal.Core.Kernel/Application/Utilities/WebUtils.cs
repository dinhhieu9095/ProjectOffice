using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SurePortal.Core.Kernel.Application.Utilities
{
    public static class WebUtils
    {
        public static byte[] DefaultAvatar => Convert.FromBase64String("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IUGVGRHNJZdcotsUa+TqDKdp7QS8RCGsvRW9dlGlBrpXWdDQCqnelisrKwsLC4PB4FnPetZFF110+umnr4VA213K/759+6677rrvf//7q6urs7Ozw+HQ+lT8dnbSia1XduMiV2thyfIfiVKVXsWmLNlbU9Yb0U33rCMVwRrvd5VOsOPEOsnirl0rWf+M7Vt1Yru3aZqlpaWFhYXzzjvv4osvftKTnqRulUzCnBUMPezZs+c//uM/fvCDH8zOzk5OTjZNY+kixurTx3a7PtWUx+7NJl2l3GOn1VrsQypuzst0R67H8pgFtpRVRmZlMGezsN4kWm/t5HBHVjnaW9aXl5ePHj06Ozt7+eWXn3vuuWhAsR7vXYXSLMcPf/jDq6++enFxcf369d3vcCo10mQzxSuesZOOWpRcVcRX74TfbYSTfcVndG8A1goF/JRTZknkh0Yte8mgcqK8dAemate7R4ql7JRdBL6+Uryhn2PHjh0+fPiCCy741Kc+RVrvDMpTJIQopUVaL06Kw3PPPfdTn/rUS1/60oMHDy4tLZF99AUaUspydazWrR8rAFsIopFUpAaEnGSKIAsVeAmzoxw1UHPJJZfYPtDM7bRTYwqAyLKNlFglSogWbfHULaWmyNUGLcvV1dXVxcXF4XD41re+9UUvelHduDvm5+f37t17991333///Xv37t23b18n1unp6dNPP33Lli1nnXXWM5/5zC1btszNzalDlfI3v/nNT3ziE6urq9PT03XacWOFRmLJKsEyY+egVqTi2W6Mfq/WQWrRrSoee+zkntXbIs7aIEsmGycWWRa90jDhJGsFXX/UuLr30Ucf3bBhw7vf/e6tW7diIHUyPz//zW9+89Zbb923b98jjzwyMTExMTHRvfKemJiIiNFo1LZt97en0Wh06qmnnn766S95yUte9KIXoe6prl2IX/ziFx/+8IePHDmybt06SrOe7xg1rI2E7OawFv82ipa77if644zSt0V/zCHJXS3s/ugf2pGkaUVTcTi2eTDPSpJ2e+Wop9a27fz8/IYNG6666qoNGzYowgJsz549n/3sZ++8886VlZXp6empqanupXYIIXi+vLy8vLy8tLQ0HA6f97znvf71r8effbXMR44c+eu//uujR4/OzMw0TaNDLvJqqjeNok8ztjPaSVH1dZWKDWpluRbwjz3i792VrCwGZmuvVi4RU/9Xl/5P7LOjbdvut42f/OQnu98Ghrv1HThw4F//9V+/+93vzs7Odn8kerzR27YdjUaLi4sLCws7dux4+9vfvnHjxnBTMCJWVlbe8pa3rK6urlu37ndOk8Rk1Tx2I638f9Ketcfvth2PiR07dpBfnUClqIVxtMQVjVp2kUFz/FAPuCULQQYEzCJU+8yYMm3bdmlpaXJy8mMf+9jk5KR1GBE33HDDBz7wgcOHD5988skzMzNlopP/OodN0wwGg6mpqbm5ub179372s58djUbnnXee5WEwGLz61a/+8pe/vLy8PDExQZTiU0upItGRr2xTLhrUppwlq34amCAWSQZD+VHaJ84//3wLxT4W+SovWUXptRraV7KyNmSvFOOl3/kljfpcXl5umubKK6887bTTwr20ePDBB9/3vvd95zvfOemkk2ZmZiyYtYSjRLo3z/z4xz/euXPns571rO5lOjE/MTFxzjnn3HbbbW3bdj8SKF3hKtL0h3qsQX/2RWPA3YaioHO0IVoy/1aE2cubbAsdE895znPwpoyNQkApEwqJTuxQ157TDIlolQL5pJcT2XmdRAWAj90fTd/61reef/75ijMivv/973/wgx985JFH5ubmyusclVfGAHFLPHc/3R44cOC22247++yzt2zZgvrrLDdu3HjKKad861vf6t5cSSnYF+5EiH0djx4sdbSxyRtMqc52VSBleqhoQJFP7NixowIoI0u3aOBwraIJ66HbVS56STuhrFcwEH501Z0fOXLkxS9+8Rve8IZiiZW4/fbbr7nmmrZt5+bmBoMBveQjeJUfSCgo5tK9tllcXPzGN76xefPms88+W7N4ylOe8uCDD957770zMzNaBZUUAsNhrxj0XPmv/1CX6U97Q6uMANQhhkb50XY06H03UzlIstlPJPaoaGvsS0M1rmxv4S8LFga1DQlRnReDEmhxcXFubu4d73gHhevMdu7cedVVV01OTk5NTRWW2uMHurU4sXi6i7DNzMxMT09fddVVO3futBS94x3vmJubW1xcRKg2KKpHA2kKNlyWizrPuogQUkTcjo+ZrK0YEFW3MtB4VCo61Ky408rZVqEtdazqQXkhPVnpEGwr/bKxS2R1dfXw4cOXX345El2A/eQnP/nQhz60adMm/D0jQcIslPq2P4aRXsy9PB0Oh5s2bfrQhz70k5/8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YvvBD37w9a9//Y477ui+v3d6enpiYgK/sk9h4OPy8aNt28FgsH379pe85CUvetGL6H3FKqaI2Lt37/vf//6HH364exuwKinjWZlUs0qzWY1mICujKhveGCLDQHgq6Zx47R7Cix26kaiqvlfTyCaQIqbM7RioJ0nbC2YCgyvdr19OPfXU9773vd1/D1WEfujQoZtvvvkrX/nKI488MhwOu793Ymhbe60Q+l9eXu7eRXPyySeff/75r3nNa8p7xbRPiqsPf/jD3/ve9/CFDZGQKcxyku21/VNvKisPe5IBIG+RiLZiExGP/WZG28jWoPhS3WRP8VCtWAosXOsqREwIr9KENoty3r359hnPeMaVV15ZhGtF9sgjj1x//fU7d+7s3qzbvWIZ28kKNTtv27b8K9PTn/70N7/5zeWj3JWr7rj++uu/9KUvTU1NTU1NKTmEX69GIoD6gBs7v6Kvy4rGbKUqRBHy7BbR2Uw85znPKUtN05QwZUUJJbhkGblSMQpipbjFoKxoadGMXOkj+bFQUeuHDh162cte9q53vQtfRajn66677qqrrvrtb387PT3daV3B0/aMH2QSzyNiYmJiampqcnLyoYce+uIXv7h79+5nPvOZ69atUzzdcd5555100knf+c536G2YVEorX8JQDEg9KlPLJK1oUWw1UQxko3is0iwzjyVYfu+u0MM1MWorm9aRHJVRjYuV2Y/AMvYtVC2htVxeXj548OCb3vSm17/+9RXkX/3qVz/xiU90n75U3qYbruSV8Vk/LKWrq6vz8/MLCwt/8Ad/8Bd/8Rd45ylsdCc//elPr7zyyvXr15d/+8imJm6PfumJXss2bhw7j+3oVWUrNpQ1Hdab3ds0zcRznvMcaqyoNopVEtpXFI9Dqz4/KLoFQI+2pzOQ1nJlZeXw4cNvfOMb/+RP/iTbfuDAgfe+973dP4Z2E11Ty7ROZvXDehsMBt0/c9x777033XTT6aef3r3zXnPZtGnTOeecc/PNN3d/edUBGa4E0ScZMXQHSZPIt9vRv5ZA7wYEDONafZKESEiIoW3bx94zo2lnk56CFTREtyZT51czqYwKpKDClB3k2dXl5eXFxcVXvepVb37zmzVo9/id73zn7//+748cOdL9LIhmWdYUSDVhp7iOW1zpRL+ysnL77bcvLy93f/NStk877bSnPOUp3/zmNyOi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");

        public static string BuildActionLink(string actionName, string controllerName, object routeValues)
        {
            var httpContextWrapper = new HttpContextWrapper(HttpContext.Current);
            var urlHelper = new UrlHelper(new RequestContext(httpContextWrapper, RouteTable.Routes.GetRouteData(httpContextWrapper)));
            return urlHelper.Action(actionName, controllerName, routeValues);
        }

        public static string FormatIsShow(bool isShow) => isShow ? "Có" : "Không";

        public static bool FormatIsShow(string show) => show == "Có";

        public static string FormatGender(int gender) => gender == 0 ? "Nữ" : gender == 1 ? "Nam" : "Không xác định";

        public static int FormatGender(string gender) => gender == "Nữ" ? 0 : gender == "Nam" ? 1 : 2;

        public static string FormatMobile(string mobile)
        {
            var plain = Regex.Replace(mobile.Trim(), @"[^\d]", "");
            return $"({plain.Substring(0, plain.Length - 7)}) {plain.Substring(plain.Length - 7, 3)}-{plain.Substring(plain.Length - 4)}";
        }

        public static string RemoveDiacritics(this string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var character in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(character);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    if (character.ToString() == "đ")
                    {
                        stringBuilder.Append(Encoding.UTF8
                            .GetString(Encoding.GetEncoding("ISO-8859-8").GetBytes(character.ToString())));
                    }
                    else
                    {
                        stringBuilder.Append(character);
                    }
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        public static string GetContentType(string FileExt)
        {
            FileExt = FileExt.TrimStart(new char[] { '.' });
            FileExt = "." + FileExt;
            FileExt = FileExt.ToLower();
            string contentType = "";
            switch (FileExt)
            {
                case ".doc":
                    contentType = "application/msword";
                    break;

                case ".docx":
                    contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    break;

                case ".xls":
                    contentType = "application/vnd.ms-excel";
                    break;

                case ".xlsx":
                    contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    break;

                case ".ppt":
                    contentType = "application/vnd.ms-powerpoint";
                    break;

                case ".pptx":
                    contentType = "application/vnd.openxmlformats-officedocument.presentationml.presentation";
                    break;

                case ".pdf":
                    contentType = "application/pdf";
                    break;

                case ".tif":
                    contentType = "image/tiff";
                    break;

                case ".tiff":
                    contentType = "image/tiff";
                    break;

                case ".css":
                    contentType = "text/css";
                    break;

                case ".htm":
                    contentType = "text/html";
                    break;

                case ".html":
                    contentType = "text/html";
                    break;

                case ".bmp":
                    contentType = "image/x-ms-bmp";
                    break;

                case ".jpeg":
                    contentType = "image/jpeg";
                    break;

                case ".jpg":
                    contentType = "image/jpeg";
                    break;

                case ".jpe":
                    contentType = "image/jpeg";
                    break;

                case ".gif":
                    contentType = "image/gif";
                    break;

                case ".png":
                    contentType = "image/png";
                    break;

                default:
                    contentType = "text/plain";
                    break;
            }
            return contentType;
        }

        private static string[] SizeSuffixes = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

        public static string SizeSuffix(long value)
        {
            if (value < 0)
            {
                return "-" + SizeSuffix(-value);
            }

            int i = 0;
            decimal dValue = value;

            while (Math.Round(dValue, 2) >= 1000)
            {
                dValue /= 1024;
                i++;
            }

            return $"{dValue.ToString("0.##", new CultureInfo("en-US"))} {SizeSuffixes[i]}";
        }

        public static string ToCapitalizeCase(this string text)
        {
            if (text == null)
                return null;

            if (text.Length > 1)
                return char.ToUpper(text[0]) + text.Substring(1);

            return text.ToUpper();
        }

        public static string DisplayName(this Enum enumValue)
        {
            return enumValue
                .GetType()
                .GetMember(enumValue.ToString())
                .First()
                .GetCustomAttribute<DisplayAttribute>()
                .GetName();
        }

        public static string ToSubEqual(this string text, int maxLength)
        {
            if (text.Length <= maxLength)
                return text;

            const string separator = " ";
            var currentLength = 0;

            var words = text
                .Split(' ')
                .TakeWhile(t => (currentLength += t.Length + separator.Length) < maxLength);

            return string.Join(separator, words);
        }

        public static string GetUserNameFromContext(string userNameInContext)
        {
            string realLoginName = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(userNameInContext) && System.Web.HttpContext.Current != null &&
                        System.Web.HttpContext.Current.Request != null)
                {
                    //fetch FedAuth
                    var cookie = System.Web.HttpContext.Current.Request.Cookies["FedAuth"];
                    if (cookie != null && !string.IsNullOrEmpty(cookie.Value))
                    {
                        var cookieValue = cookie.Value;
                        var base64EncodedBytes = Convert.FromBase64String(cookieValue);
                        cookieValue = Encoding.UTF8.GetString(base64EncodedBytes);
                        cookieValue = new Regex("\\<\\?xml.*\\?>").Replace(cookieValue, "");
                        cookieValue = cookieValue.Replace("<SP>", "").Replace("</SP>", "");
                        userNameInContext = cookieValue.Split(',')[0];
                    }
                }
            }
            catch
            {
            }
            if (!string.IsNullOrEmpty(userNameInContext))
            {
                realLoginName = GetStandardUserName(userNameInContext);
            }

            return realLoginName;
        }

        public static string GetStandardUserName(string loginName)
        {
            if (!string.IsNullOrEmpty(loginName))
            {
                if (loginName.Contains('|'))
                {
                    var arr = loginName.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                    if (arr.Length > 2)
                    {
                        loginName = arr[1] + "\\" + arr[2];
                    }
                    else
                    {
                        loginName = loginName.Split('|')[1];
                    }
                }
                if (loginName.Contains("\\"))
                {
                    loginName = loginName.Split('\\')[1];
                }
                if (loginName.Contains("@"))
                {
                    loginName = loginName.Split('@')[0];
                }
                if (!loginName.Contains("\\"))
                {
                    loginName = AppSettings.DomainName + "\\" + loginName;
                }
                return loginName.ToLower();
            }
            else
            {
                return string.Empty;
            }
        }
    }
}