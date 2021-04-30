using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurePortal.Module.Task.Entities
{
    public enum ProcessTaskActionId
    {
        /// <summary>
        ///  xử lý
        /// </summary>
        Process = 1,
        /// <summary>
        /// Báo cáo
        /// </summary>
        Report = 2,
        /// <summary>
        /// Kết thúc
        /// </summary>
        Finish = 4,
        /// <summary>
        /// Gia hạn
        /// </summary>
        Extend =5,
        /// <summary>
        /// Tra lai
        /// </summary>
        Cancel =6,
    }
}