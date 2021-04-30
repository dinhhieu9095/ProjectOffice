using SurePortal.Core.Kernel.Mapper;
using SurePortal.Module.Task.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurePortal.Module.Task.Web
{
    public class QueryCommonModel : IMapping<QueryCommonDto>
    {
        public Guid? UserId { get; set; }
        public Guid? ProjectId { get; set; }
        public Guid? TaskItemId { get; set; }
        public Guid? TaskItemAssignId { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
