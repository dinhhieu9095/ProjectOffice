using SurePortal.Core.Kernel.Mapper;
using System;

namespace SurePortal.WebHost.Models.Orgs.Departments
{
    public class DepartmentInfoModel : IMapping<DepartmentModel>
    {
        public Guid Id { get; set; }
        public int Order { get; set; }
        public string Branch { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
    }
}