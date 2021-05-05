using DaiPhatDat.Core.Kernel.Mapper;
using DaiPhatDat.Core.Kernel.Orgs.Domain;
using System;
using System.Collections.Generic;

namespace DaiPhatDat.Core.Kernel.Orgs.Application.Dto
{
    public partial class DepartmentDto : IMapping<Department>
    {
        public System.Guid Id { get; set; }

        public string Name { get; set; }


        public Nullable<int> OrderNumber { get; set; }

        public Nullable<System.Guid> ParentID { get; set; }

        public string Path { get; set; }

        public Nullable<bool> IsActive { get; set; }

        public Guid RootDBID { get; set; }

        public string ResxName { get; set; }

        public string Code { get; set; }

        public string DeptIndex { get; set; }

        public int TreeLevel { get; set; }

        public string HierarchyName { get; set; }

        public List<DepartmentDto> Children { get; set; }
    }

    public class CreateDepartmentDto
    {
        public string Name { get; set; }
        public Nullable<int> OrderNumber { get; set; }
        public System.Guid ParentID { get; set; }
        public bool IsShow { get; set; }
        public System.Guid DeptTypeID { get; set; }
        public string Code { get; set; }
        public System.Guid DeptGroupID { get; set; }
        public bool IsPrint { get; set; }
        public Guid CreatedBy { get; set; }
    }

    public class UpdateDepartmentDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public Nullable<int> OrderNumber { get; set; }
        public System.Guid ParentID { get; set; }
        public bool IsShow { get; set; }
        public System.Guid DeptTypeID { get; set; }
        public string Code { get; set; }
        public System.Guid DeptGroupID { get; set; }
        public bool IsPrint { get; set; }

        public Guid ModifiedBy { get; set; }

    }
    public class DeleteDepartmentDto
    {
        public Guid Id { get; set; }
    }


}