using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurePortal.Module.Task.Entities
{
    public enum ProjectSecretId
    {
        /// <summary>
        /// Bình thường
        /// </summary>
        BINHTHUONG = 0,
        /// <summary>
        /// Mật
        /// </summary>
        MAT = 1,
        /// <summary>
        /// Tuyệt mật
        /// </summary>
        TUYETMAT = 2,
        /// <summary>
        /// Tối mật
        /// </summary>
        TOIMAT=3,
    }
}