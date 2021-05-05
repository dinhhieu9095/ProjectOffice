using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DaiPhatDat.Module.Task.Entities
{
    public class SearchProject
    {
        public int Number { get; set; }

        public Guid DocumentID { get; set; }

        public Guid DeptID { get; set; }

        public string SerialNumber { get; set; }

        public string Summary { get; set; }

        public Guid ApprovedBy { get; set; }

        public string ApprovedByName { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public string PrimaryAssignTo { get; set; }

        public string SupportAssignTo { get; set; }

        public string ReadOnlyAssignTo { get; set; }

        public int ProjectStatusId { get; set; }

        public string ProjectStatusCode { get; set; }

        public string ProjectStatusName { get; set; }

        public int TotalPage { get; set; }

        public int TotalRecord { get; set; }
    }
}