using DaiPhatDat.Core.Kernel.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaiPhatDat.Core.Kernel.Domain.Entities
{
   public interface IBaseMoreEntity
    {
         Guid Id { get; set; }
        string Name { get; set; }
        string URL { get; set; }
        string CreatedBy { get; set; }
        string ModifiedBy { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime ModifiedDate { get; set; }
        CommonUtility.ActiveFag ActiveFag { get; set; }
    }
}
