using DaiPhatDat.Core.Kernel.Mapper;
using System;

namespace DaiPhatDat.Core.Kernel.Orgs.Application.Dto
{
    public class DepartmentInfo : IMapping<DepartmentDto>
    {
        public Guid Id { get; set; }
        public int Order { get; set; }
        public string Branch { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
    }
}