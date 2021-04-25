using SurePortal.Core.Kernel.Mapper;
using SurePortal.Core.Kernel.Orgs.Application.Dto;
using SurePortal.Core.Kernel.Orgs.Domain.Entities;
using System;

namespace SurePortal.WebHost.Models.Orgs.Departments
{
    public class DepartmentTypeModel : IMapping<DepartmentTypeDto>
    {
        public Guid Id { get; set; }

        public string Name { get; private set; }

        public string Code { get; private set; }

        public bool IsActive { get; private set; }
    }
}
