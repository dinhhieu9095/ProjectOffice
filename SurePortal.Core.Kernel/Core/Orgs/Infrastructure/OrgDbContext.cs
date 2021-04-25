using SurePortal.Core.Kernel.Orgs.Application.Dto;
using SurePortal.Core.Kernel.Orgs.Domain.Entities;
using System.Data.Entity;

namespace SurePortal.Core.Kernel.Orgs.Domain
{
    public class OrgDbContext : Context, IContext
    {
        public OrgDbContext()
             : base()
        {
            // when loading entity, you should you Include method for every navigation properties you need
            Configuration.LazyLoadingEnabled = false;
            // we do not use lazy load, so proxy creation not necessary anymore
            Configuration.ProxyCreationEnabled = false;
        }

        /// <summary>
        /// Map to db set of Users
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Map to db set of tbUsers
        /// </summary>
        public DbSet<UserJobTitle> UserJobTitles { get; set; }

        /// <summary>
        /// Map to db set of Departments
        /// </summary>
        public DbSet<Department> Departments { get; set; }

        /// <summary>
        /// Map to db set of tbDepartments
        /// </summary>
        public DbSet<DepartmentType> DepartmentTypes { get; set; }

        ///// <summary>
        ///// Map to db set of tbDepartmentGroups
        ///// </summary>
        //public DbSet<DepartmentGroup> DepartmentGroups { get; set; }

        /// <summary>
        /// Map to db set of UserDepartments
        /// </summary>
        public DbSet<UserDepartment> UserDepartments { get; set; }

        public DbSet<UserSignature> UserSignatures { get; set; }

        public DbSet<SystemConfig> SystemConfigs { get; set; }

        public DbSet<UserDevice> UserDevices { get; set; }

        public DbSet<UserDelegation> UserDelegations { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<GroupRole> GroupRoles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Permission> Permissions { get; set; }
    }
}