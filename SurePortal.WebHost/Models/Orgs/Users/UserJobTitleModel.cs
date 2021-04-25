using SurePortal.Core.Kernel.Mapper;
using SurePortal.Core.Kernel.Orgs.Application.Dto;
using SurePortal.Core.Kernel.Orgs.Domain.Entities;
using System;

namespace SurePortal.WebHost.Models.Orgs.Users
{
    public class UserJobTitleModel : IMapping<UserJobTitleDto>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int? OrderNumber { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }
    }
}
