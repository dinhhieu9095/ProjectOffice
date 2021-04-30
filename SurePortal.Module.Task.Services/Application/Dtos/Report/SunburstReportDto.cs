using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurePortal.Module.Task.Services
{
    public class SunburstReportDto
    {
        public string id { get; set; }
        public string parent { get; set; }
        public string name { get; set; }
        public int value { get; set; }
    }
}
