using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace DaiPhatDat.Core.Kernel.Helper
{
    public static class ConvertToStringExtensions
    {
        /// <summary>
        /// Chuyển từ DateTime sang String
        /// </summary>
        /// <param name="date"></param>
        /// <returns>Smart</returns>
        public static string DateToString(DateTime? date)
        {
            var rs = string.Empty;
            if (date != null)
            {
                var dateTime = date.GetValueOrDefault();
                var dateNow = DateTime.Now;
                if (dateTime.Year == dateNow.Year)
                {
                    rs = dateTime.ToString("dd/MM");
                    if (dateTime.Month == dateNow.Month)
                        rs = "Ngày " + dateTime.ToString("dd");
                }
                else
                {
                    rs = dateTime.ToString("dd/MM/yy");
                }
            }
            return rs;
        }

        public static string DateTimeToString(DateTime? date)
        {
            var rs = string.Empty;
            if (date != null)
            {
                var dateTime = date.GetValueOrDefault();
                var dateNow = DateTime.Now;
                if (dateTime.Year == dateNow.Year)
                {
                    rs = dateTime.ToString("dd/MM HH:mm");
                    if (dateTime.Month == dateNow.Month)
                        rs = "Ngày " + dateTime.ToString("dd HH:mm");
                }
                else
                {
                    rs = dateTime.ToString("dd/MM/yy HH:mm");
                }
            }
            return rs;
        }

        /// <summary>
        /// Chuyển từ DateTime sang String
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns>Smart</returns>
        public static string DateToString(DateTime? fromDate, DateTime? toDate)
        {
            var rs = string.Empty;
            if (fromDate != null && toDate != null)
            {
                var fromDateTime = fromDate.GetValueOrDefault();
                var toDateTime = toDate.GetValueOrDefault();

                if (fromDateTime.Year == toDateTime.Year)
                {
                    if (fromDateTime.Year == DateTime.Now.Year)
                    {
                        rs = fromDateTime.ToString("dd/MM") + " - " + toDateTime.ToString("dd/MM");
                        if (fromDateTime.Month == toDateTime.Month)
                        {
                            if (fromDateTime.Day == toDateTime.Day)
                            {
                                rs = fromDateTime.ToString("dd/MM");
                            }
                            else
                            {
                                rs = fromDateTime.ToString("dd") + " - " + toDateTime.ToString("dd/MM");
                            }

                        }


                    }
                    else
                    {
                        rs = fromDateTime.ToString("dd/MM") + " - "
                            + toDateTime.ToString("dd/MM/yy");
                        if (fromDateTime.Month == toDateTime.Month)
                        {
                            if (fromDateTime.Day == toDateTime.Day)
                            {
                                rs = fromDateTime.ToString("dd/MM/yy");
                            }
                            else
                            {
                                rs = fromDateTime.ToString("dd") + " - "
                                + toDateTime.ToString("dd/MM/yy");
                            }
                        }
                    }
                }
                else
                {
                    rs = fromDateTime.ToString("dd/MM/yy") + " - " + toDateTime.ToString("dd/MM/yy");
                }
            }
            return rs;
        }

        /// <summary>
        /// Chuyển từ DateTime sang String
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns>dd/MM/yy HH:mm</returns>
        public static string DateToStringNewLine(DateTime? fromDate, DateTime? toDate)
        {
            return $"{fromDate?.ToString("dd/MM/yy HH:mm")}"
                    + $"{Environment.NewLine}"
                    + $"{toDate?.ToString("dd/MM/yy HH:mm")}";
        }

        /// <summary>
        /// DateToStringLocal
        /// </summary>
        /// <param name="date"></param>
        /// <param name="specifier"></param>
        /// <param name="local"></param>
        /// <returns></returns>
        public static string DateToStringLocal(DateTime? date, string specifier = "d", string local = null)
        {
            if (string.IsNullOrEmpty(local))
                local = Thread.CurrentThread.CurrentUICulture.Name;

            DateTimeFormatInfo fmt = (new CultureInfo(local)).DateTimeFormat;

            return date?.ToString(specifier, fmt);
        }

        public static string DateToStringLocal(DateTime? fromDate, DateTime? toDate, string specifier = "d", string local = null)
        {
            if (string.IsNullOrEmpty(local))
                local = Thread.CurrentThread.CurrentUICulture.Name;

            DateTimeFormatInfo fmt = (new CultureInfo(local)).DateTimeFormat;

            return fromDate?.ToString(specifier, fmt)
                + ((fromDate != null && toDate != null) ? " - " : "")
                + toDate?.ToString(specifier, fmt);
        }

        public static string DateToStringLocalNewLine(DateTime? fromDate, DateTime? toDate, string specifier = "d", string local = null)
        {
            if (string.IsNullOrEmpty(local))
                local = Thread.CurrentThread.CurrentUICulture.Name;
            if (string.IsNullOrEmpty(local))
                local = "vi-VN";

            DateTimeFormatInfo fmt = (new CultureInfo(local)).DateTimeFormat;

            return fromDate?.ToString(specifier, fmt)
                + ((fromDate != null && toDate != null) ? $"{Environment.NewLine}" : "")
                + toDate?.ToString(specifier, fmt);
        }

        public static string RemoveVietnameseTone(string text)
        {
            string result = text.ToLower();
            result = Regex.Replace(result, "à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ|/g", "a");
            result = Regex.Replace(result, "è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ|/g", "e");
            result = Regex.Replace(result, "ì|í|ị|ỉ|ĩ|/g", "i");
            result = Regex.Replace(result, "ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ|/g", "o");
            result = Regex.Replace(result, "ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ|/g", "u");
            result = Regex.Replace(result, "ỳ|ý|ỵ|ỷ|ỹ|/g", "y");
            result = Regex.Replace(result, "đ", "d");
            return result;
        }
        public static string VietnameseToneToCode(string text)
        {
            return Regex.Replace(RemoveVietnameseTone(text), " |\n|\t", "_").ToUpper();
        }
    }
}
