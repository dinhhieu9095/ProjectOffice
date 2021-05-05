using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaiPhatDat.Core.Kernel.Application.Utilities
{
    public class DateUtility
    {
        public static DateTime GetDueDate(DateTime startDate, double days)
        {
            var openHours = new OpenHours()
            {
                StartHour = 7,
                EndHour = 17,
            };
            List<DateTime> holidays = new List<DateTime>();
            var calculationDate = new CalculationDate(holidays, openHours);
            return calculationDate.GetDueDate(startDate, (int)(days * (openHours.EndHour - openHours.StartHour) * 60));
        }
    }
}
