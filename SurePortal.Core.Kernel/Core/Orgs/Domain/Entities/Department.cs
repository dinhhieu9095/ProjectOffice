using DaiPhatDat.Core.Kernel.Orgs.Domain.Entities;
using System;

namespace DaiPhatDat.Core.Kernel.Orgs.Domain
{
    public class Department : BaseEntity
    {
        public string Name { get; private set; }
        public Nullable<int> NoUser { get; private set; }
        public Nullable<int> OrderNumber { get; private set; }
        public Nullable<System.Guid> ParentID { get; private set; }
        public string Path { get; private set; }
        public bool IsActive { get; private set; }
        public System.DateTime Created { get; private set; }
        public System.Guid AuthorID { get; private set; }
        public bool IsShow { get; private set; }
        public System.Guid DeptTypeID { get; private set; }
        public System.Guid EditorID { get; private set; }
        public System.DateTime Modified { get; private set; }
        public string ServerAddress { get; private set; }
        public string DatabaseName { get; private set; }
        public string UserAccess { get; private set; }
        public string Password { get; private set; }
        public string Code { get; private set; }
        public Nullable<System.Guid> RootDBID { get; private set; }
        public Nullable<bool> IsPrint { get; private set; }
        public static Department Create(string name,
            string code, Guid parentID, Guid deptTypeID,
            Guid deptGroupId, int orderNumber, bool isPrint,
            bool isShow, Guid userId)
        {
            return new Department()
            {
                Name = name,
                Code = code,
                ParentID = parentID,
                DeptTypeID = deptTypeID,
                RootDBID = deptGroupId,
                OrderNumber = orderNumber,
                IsPrint = isPrint,
                IsActive = true,
                AuthorID = userId,
                EditorID = userId,
                Created = DateTime.Now,
                Modified = DateTime.Now,
                DatabaseName = "",
                Path = "",
                UserAccess = "",
                Password = "",
                IsShow = isShow,
                NoUser = 0,
                ServerAddress = ""
            };
        }

        public void Update(string name, string code, int orderNumber,
            Guid deptTypeId, Guid parentId, Guid deptGroupId, bool isShow, bool isPrint, Guid userId)
        {
            Name = name;
            Code = code;
            OrderNumber = orderNumber;
            ParentID = parentId;
            RootDBID = deptGroupId;
            EditorID = userId;
            IsShow = isShow;
            IsPrint = isPrint;
            DeptTypeID = deptTypeId;
            Modified = DateTime.Now;
        }

        public void UpdateActive(bool isActive, bool isShow, bool isPrint)
        {
            IsActive = isActive;
            IsShow = isShow;
            IsPrint = isPrint;
        }
    }
}