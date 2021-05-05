using System;
using System.Text;
using System.Text.RegularExpressions;

namespace DaiPhatDat.Core.Kernel.Application.Helpers
{
    public class StringHelper
    {
        public static string ConvertToNoSign(string source)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = source.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
    }
}
