using System;

namespace SurePortal.Core.Kernel.Orgs.Domain.Entities
{
    public class ReadUserDepartment
    {
        public Guid UserID { get; set; }

        public Guid DeptID { get; set; }

        public string DeptName { get; set; }

        public string UserName { get; set; }

        public int? OrderNumber { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string HomePhone { get; set; }

        public string Mobile { get; set; }
        public string AccountName { get; set; }

        public string JobTitleName { get; set; }

        public int? JobTitleOrderNumber { get; set; }

        public string JobTitleCode { get; set; }

        public string UserTypeOtp { get; set; }

        public bool HasSignature { get; set; }

        public int TotalRows { get; set; }

    }
}
