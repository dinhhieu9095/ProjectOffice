using DaiPhatDat.Core.Kernel.Models;
using DaiPhatDat.Core.Kernel.Orgs.Application.Dto;
using System;

namespace DaiPhatDat.Core.Kernel.Orgs.Models
{
    public class CrudSystemConfigInput
    {
        public string Key { get; set; }
        public string Action { get; set; }
        public SystemConfigDto Value { get; set; }
    }
    public class DeleteUserDepartmentInput
    {
        public Guid UserId { get; set; }
        public Guid DeptId { get; set; }
    }
    public class GetUserDepartment
    {
        public Guid DeptId { get; set; }
        public string Keyword { get; set; }
        public KtPaging Pagination { get; set; }
        public KtSort Sort { get; set; }

    }
}
