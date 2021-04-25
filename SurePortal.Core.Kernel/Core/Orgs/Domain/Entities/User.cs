using SurePortal.Core.Kernel.Orgs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SurePortal.Core.Kernel.Orgs.Domain
{
    public class User : BaseEntity
    {
        public User()
        {
            UserRoles = new HashSet<UserRole>();
        }

        public string UserName { get; private set; }
        public string FullName { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public Nullable<int> Gender { get; private set; }
        public string Email { get; private set; }
        public string Mobile { get; private set; }
        public string Address { get; private set; }
        public string HomePhone { get; private set; }
        public string Ext { get; private set; }
        public Nullable<System.DateTime> BirthDate { get; private set; }
        public string UserCode { get; private set; }
        public byte[] Avartar { get; private set; }
        public string LanguageCulture { get; private set; }
        public bool IsActive { get; private set; }
        public string UserIndex { get; private set; }
        public string AccountName { get; private set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }

        public static User CreateUser(string userName, string fullName,
            int? gender, string email, string mobile, string address,
            string homePhone, string ext, DateTime? birthDate, string userCode,
            byte[] avatar, string language, string userIndex)
        {
            var names = fullName.Trim()
                .Split(new char[] { }, StringSplitOptions.RemoveEmptyEntries).ToList();

            var firstName = names.Last();
            names.Remove(firstName);
            var lastName = string.Join(" ", names);

            return new User()
            {
                UserName = userName,
                FullName = fullName,
                Gender = gender,
                Email = email,
                Mobile = mobile,
                Address = address,
                HomePhone = homePhone,
                Ext = ext,
                BirthDate = birthDate,
                UserCode = userCode,
                Avartar = avatar,
                LanguageCulture = language,
                UserIndex = userIndex,
                IsActive = true,
                FirstName = firstName,
                LastName = lastName
            };
        }

        public void Update(string fullName,
            int? gender, string email, string mobile, string address,
            string homePhone, string ext, DateTime? birthDate, string userCode,
            byte[] avatar, string language, string userIndex)
        {
            var names = fullName.Trim()
                .Split(new char[] { }, StringSplitOptions.RemoveEmptyEntries).ToList();

            FullName = fullName;
            Gender = gender;
            Email = email;
            Mobile = mobile;
            Address = address;
            HomePhone = homePhone;
            Ext = ext;
            BirthDate = birthDate;
            UserCode = userCode;
            Avartar = avatar;
            LanguageCulture = language;
            UserIndex = userIndex;
            IsActive = true;
            FirstName = names.Last();
            names.Remove(FirstName);
            LastName = string.Join(" ", names);
        }

        public void UpdateAdInfo(string email, string mobile, string ext, string userCode, byte[] avatar)
        {
            Email = email;
            Mobile = mobile;
            UserCode = userCode;
            Ext = ext;
            if (avatar != null)
            {
                Avartar = avatar;
            }
        }

        public void UpdateActive(bool isActive)
        {
            IsActive = isActive;
        }
    }
}