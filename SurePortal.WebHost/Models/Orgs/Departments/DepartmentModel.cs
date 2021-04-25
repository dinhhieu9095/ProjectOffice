using SurePortal.Core.Kernel.Mapper;
using SurePortal.Core.Kernel.Orgs.Application.Dto;
using SurePortal.WebHost.Models.Orgs.Users;
using System;
using System.Collections.Generic;

namespace SurePortal.WebHost.Models.Orgs.Departments
{
    public partial class DepartmentModel : IMapping<DepartmentDto>
    {
        public DepartmentModel()
        {
            Children = new List<DepartmentModel>();
            UserDepartment = new List<UserDepartmentModel>();
        }
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
        public bool HaveChild { get; set; }

        public List<DepartmentModel> Children { get; set; }
        public List<UserDepartmentModel> UserDepartment { get; set; }
          
    }

    public class DepartmentUserViewModel
    {
        #region Constructors
        public DepartmentUserViewModel()
        {
            DepartmentModels = new List<DepartmentModel>();
        }
        #endregion
        public IList<DepartmentModel> DepartmentModels { get; set; }
    }
}