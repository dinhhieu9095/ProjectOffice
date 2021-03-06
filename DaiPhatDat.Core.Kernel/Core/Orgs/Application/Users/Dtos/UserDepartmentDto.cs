using DaiPhatDat.Core.Kernel.Mapper;
using DaiPhatDat.Core.Kernel.Orgs.Domain;
using DaiPhatDat.Core.Kernel.Orgs.Domain.Entities;
using System;

namespace DaiPhatDat.Core.Kernel.Orgs.Application.Contract
{
    public partial class UserDepartmentDto : IMapping<UserDepartment>
    {
        public string Id { get { return $"{UserID}_{DeptID}"; } }

        public int RecordID { get; set; }

        public Guid DeptID { get; set; }

        public string DeptName { get; set; }

        public int? DeptOrderNumber { get; set; }

        public string DeptCode { get; set; }

        public string DatabaseName { get; set; }

        public Guid UserID { get; set; }

        public string UserName { get; set; }

        public string FullName { get; set; }
        public string AccountName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public string JobDescription { get; set; }
        public int? OrderNumber { get; set; }

        public string JobTitleName { get; set; }

        public int? JobTitleOrderNumber { get; set; }

        public string JobTitleCode { get; set; }

        public Guid? JobTitleID { get; set; }

        public bool IsManager { get; set; }

        public Guid RootDBID { get; set; }

        public string UserCode { get; set; }

        public string JobTitleNameResx { get; set; }

        public string DeptNameResx { get; set; }

        public string UserIndex { get; set; }

        public string DeptIndex { get; set; }
        public Guid ParentID { get; set; }

        public string HomePhone { get; set; }
    }

    public class ReadUserDepartmentDto : IMapping<ReadUserDepartment>
    {
        public string Id { get { return $"{UserID}_{DeptID}"; } }

        public int RecordID { get; set; }

        public Guid UserID { get; set; }

        public Guid DeptID { get; set; }

        public string DeptName { get; set; }

        public string UserName { get; set; }

        public int? OrderNumber { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string HomePhone { get; set; }

        public string Mobile { get; set; }


        public string JobTitleName { get; set; }

        public int? JobTitleOrderNumber { get; set; }

        public string JobTitleCode { get; set; }

        public int TotalRows { get; set; }
    }

    public class CreateUserDepartmentDto
    {

    }

    public class UpdateUserDepartmentDto
    {

    }
}