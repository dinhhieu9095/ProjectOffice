using SurePortal.Core.Kernel.Orgs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurePortal.Core.Kernel.Orgs.Domain
{
    public class Group
    {
        public Group()
        {
            UserGroups = new HashSet<UserGroup>();
            GroupRoles = new HashSet<GroupRole>();
        }

        [Key]
        public Guid ID { get; private set; }

        public string Name { get; private set; }

        public bool IsActive { get; private set; }
        public Guid? DepartmentId { get; private set; }
        public virtual ICollection<UserGroup> UserGroups { get; set; }
        public virtual ICollection<GroupRole> GroupRoles { get; set; }

    }
    public class UserGroup
    {
        public UserGroup()
        {
        }

        [Key]
        [Column(Order = 1)]
        public Guid UserID { get; private set; }
        [Key]
        [Column(Order = 2)]
        public Guid GroupID { get; private set; }
        public bool? IsManager { get; private set; }
        public User User { get; private set; }
    }
    public class GroupRole
    {
        [Key]
        [Column(Order = 1)]
        public Guid GroupID { get; private set; }
        [Key]
        [Column(Order = 2)]
        public Guid RoleID { get; private set; }
        [Key]
        [Column(Order = 3)]
        public Guid ModuleID { get; private set; }
        public string Description { get; private set; }
        public Role Role { get; private set; }
    }
    public class UserRole
    {
        [Key]
        [Column(Order = 1)]
        public Guid UserID { get; private set; }
        [Key]
        [Column(Order = 2)]
        public Guid RoleID { get; private set; }
        public string Description { get; private set; }
        public Role Role { get; private set; }
    }
    public class Role
    {
        public Role()
        {
            RolePermissions = new HashSet<RolePermission>();
        }

        [Key]
        public Guid ID { get; private set; }
        public Guid? ModuleId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }
    public class RolePermission
    {
        [Key]
        [Column(Order = 1)]
        public Guid RoleID { get; private set; }
        [Key]
        [Column(Order = 2)]
        public Guid PermissionID { get; private set; }
        public Permission Permission { get; private set; }
        public string Description { get; private set; }
    }
    public class Permission
    {
        [Key]
        public Guid ID { get; private set; }
        public Guid? ModuleID { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
    }
}