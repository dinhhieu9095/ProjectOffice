using DaiPhatDat.Core.Kernel.Mapper;
using DaiPhatDat.Core.Kernel.Orgs.Domain.Entities;
using System;

namespace DaiPhatDat.Core.Kernel.Orgs.Application.Dto
{
    public class DepartmentTypeDto : IMapping<DepartmentType>
    {
        public Guid Id { get; set; }

        public string Name { get; private set; }

        public string Code { get; private set; }

        public bool IsActive { get; private set; }
    }
}
