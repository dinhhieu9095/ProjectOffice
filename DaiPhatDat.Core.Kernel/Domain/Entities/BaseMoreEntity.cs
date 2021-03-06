using DaiPhatDat.Core.Kernel.Application.Utilities;
using DaiPhatDat.Core.Kernel.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DaiPhatDat.Core.Kernel.Domain.Entities
{
    public class BaseMoreEntity : BaseEntity, IBaseMoreEntity
    {
        public string Name { get; set; }
        public string URL { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public CommonUtility.ActiveFag ActiveFag { get; set; }
    }
}