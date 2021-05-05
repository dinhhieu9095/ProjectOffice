using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaiPhatDat.Module.Task.Web.Utils
{
    public class NumberUtils
    {
        public static string ToN0Text(object number)
        {
            return string.Format("{0:N0}", number);
        }
    }
}
