using SurePortal.Core.Kernel.Mapper;
using SurePortal.Core.Kernel.Notifications.Application.Dto;
using SurePortal.Core.Kernel.Orgs.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SurePortal.Core.Kernel.Orgs.Application.Dto
{
    public class GroupDto : IMapping<Group>
    {
        public GroupDto()
        {
            UserGroups = new List<UserGroup>();
            GroupRoles = new List<GroupRole>();
        }
         
        public Guid ID { get; private set; }

        public string Name { get; private set; }

        public bool IsActive { get; private set; }
        public Guid? DepartmentId { get; private set; }
        public virtual List<UserGroup> UserGroups { get; set; }
        public virtual List<GroupRole> GroupRoles { get; set; }
    }
}