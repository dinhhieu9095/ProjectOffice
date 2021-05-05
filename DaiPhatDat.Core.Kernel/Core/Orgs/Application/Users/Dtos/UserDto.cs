using DaiPhatDat.Core.Kernel.Mapper;
using DaiPhatDat.Core.Kernel.Notifications.Application.Dto;
using DaiPhatDat.Core.Kernel.Orgs.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DaiPhatDat.Core.Kernel.Orgs.Application.Dto
{
    public class UserDto : IMapping<User>
    {
        public UserDto()
        {
        }

        public System.Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
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
        public string AccountName { get; set; }
        public string SocketUrl { get; set; }
        public string Domain { get; set; }
        // additional 
        //public List<string> JobTitles { get; set; }
        //public List<string> DepartmentNames { get; set; }
        public List<DepartmentCompact> Departments { get; set; }
        public List<UserSignatureInfo> SignatureInfos { get; set; }
        public List<string> Permissions { get; set; }

        /// <summary>
        /// Kiểm tra người dùng đang đăng nhập có các quyền trong list hay không
        /// </summary>
        /// <param name="funcs"></param>
        /// <returns></returns>
        public bool HaveAllPermission(List<string> funcs)
        {
            if (this.Permissions != null && !funcs.Except(this.Permissions).Any())
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Kiểm tra người dùng đang đăng nhập có bấ kỳ quyền trong list hay không
        /// </summary>
        /// <param name="funcs"></param>
        /// <returns></returns>
        public bool HaveAnyPermission(List<string> funcs)
        {
            if (this.Permissions != null && funcs.Any(e => this.Permissions.Contains(e)))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Kiểm tra người dùng đang đăng nhập có các quyền trong list hay không
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public bool HavePermission(string func)
        {
            if (this.Permissions != null && this.Permissions.Any(x => x == func))
            {
                return true;
            }
            return false;
        }
    }

    public class CreateUserDto
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

        public List<CreateUserSignatureDto> UserSignatures { get; set; }
        #endregion

        #region User notifications
        public List<CreateNotificationSettingDto> NotificationSettings { get; set; }
        #endregion
    }
    public class UpdateUserDto
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
        public List<CreateUserSignatureDto> AddedUserSignatures { get; set; }

        public List<Guid> RemovedUserSignatures { get; set; }
        #endregion

        #region User notifications
        public List<CreateNotificationSettingDto> AddNotificationSettings { get; set; }

        #endregion

    }
    public class UserFilterInput
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string Key { get; set; }
    }


    public class DepartmentCompact
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string JobTitle { get; set; }
        public int? OrderNumber { get; set; }
    }
}