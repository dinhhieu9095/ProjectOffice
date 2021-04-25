using SurePortal.Core.Kernel.Orgs.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurePortal.Core.Kernel.Orgs.Domain
{
    public class UserDepartment
    {
        [Key]
        [Column(Order = 2)]
        public Guid DeptID { get; private set; }

        [Key]
        [Column(Order = 1)]
        public Guid UserID { get; private set; }

        public string JobDescription { get; private set; }

        public int? OrderNumber { get; private set; }


        public Guid? JobTitleID { get; private set; }

        public bool IsManager { get; private set; }

        [ForeignKey("DeptID")]
        public virtual Department Department { get; private set; }

        [ForeignKey("UserID")]
        public virtual User User { get; private set; }

        [ForeignKey("JobTitleID")]
        public virtual UserJobTitle UserJobTitle { get; private set; }

        public static UserDepartment CreateUserDepartment(Guid userId,
            Guid deptId, string jobDescription, int userDeptOrder,
            Guid jobTitleId, bool isManager)
        {
            return new UserDepartment()
            {
                UserID = userId,
                DeptID = deptId,
                JobDescription = jobDescription,
                OrderNumber = userDeptOrder,
                JobTitleID = jobTitleId,
                IsManager = isManager
            };
        }

        public void Update(string jobDescription, int userDeptOrder,
            Guid jobTitleId, bool isManager)
        {
            JobDescription = jobDescription;
            OrderNumber = userDeptOrder;
            JobTitleID = jobTitleId;
            IsManager = isManager;
        }
    }
}