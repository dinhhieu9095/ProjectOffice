using SurePortal.Core.Kernel.Mapper;
using SurePortal.Core.Kernel.Orgs.Application.Dto;
using SurePortal.Core.Kernel.Orgs.Domain;
using SurePortal.WebHost.Models.Notifications.Notifications;
using SurePortal.WebHost.Models.Orgs.Signatures;
using System;
using System.Collections.Generic;

namespace SurePortal.WebHost.Models.Orgs.Users
{
    public class UserModel : IMapping<UserDto>
    {
        public UserModel()
        {
        }

        public System.Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
        public string HomePhone { get; set; } = string.Empty;
        public string UserCode { get; set; } = string.Empty;
        public string LanguageCulture { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public string Address { get; set; } = string.Empty;
        public DateTime? BirthDate { get; set; }
        public string Ext { get; set; }
        public int? Gender { get; set; }
        public string UserIndex { get; set; } = string.Empty;
        // additional 
        public List<string> JobTitles { get; set; }
        public List<string> DepartmentNames { get; set; }
        public List<UserSignatureInfo> SignatureInfos { get; set; }
    }

    public class CreateUserModel
    {
        #region User Info

        public string UserName { get; set; }
        public string FullName { get; set; }
        public int? Gender { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string HomePhone { get; set; }
        public string Ext { get; set; }
        public DateTime? BirthDate { get; set; }
        public string UserCode { get; set; }
        public string UserIndex { get; set; }
        public string Language { get; set; }
        #endregion

        #region User Department Info

        public List<Guid> Departments { get; set; }

        public Guid JobTitleId { get; set; }

        public string JobDescription { get; set; }

        public int OrderNumber { get; set; }

        public bool IsManager { get; set; }

        #endregion

        #region User Signatures

        public List<CreateUserSignatureModel> UserSignatures { get; set; }
        #endregion

        #region User notifications
        public List<CreateNotificationSettingModel> NotificationSettings { get; set; }
        #endregion
    }


    public class UpdateUserModel
    {
        #region User Info
        public System.Guid UserId { get; set; }

        public string UserName { get; set; }
        public string FullName { get; set; }
        public int? Gender { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string HomePhone { get; set; }
        public string Ext { get; set; }
        public DateTime? BirthDate { get; set; }
        public string UserCode { get; set; }
        public string UserIndex { get; set; }
        public string Language { get; set; }
        #endregion

        #region User Depts
        public List<Guid> RemovedDepartments { get; set; }

        public List<Guid> AddedDepartments { get; set; }

        public Guid JobTitleId { get; set; }

        public string JobDescription { get; set; }

        public int OrderNumber { get; set; }

        public bool IsManager { get; set; }

        #endregion

        #region User Signatures
        public List<CreateUserSignatureModel> AddedUserSignatures { get; set; }

        public List<Guid> RemovedUserSignatures { get; set; }
        #endregion

        #region User notifications
        public List<CreateNotificationSettingModel> AddNotificationSettings { get; set; }

        #endregion

    }
    public class UserFilterInput
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string Key { get; set; }
    }


}