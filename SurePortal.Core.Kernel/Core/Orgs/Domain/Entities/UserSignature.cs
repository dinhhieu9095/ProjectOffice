using SurePortal.Core.Kernel.Application;
using SurePortal.Core.Kernel.Helper;
using SurePortal.Core.Kernel.Orgs.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurePortal.Core.Kernel.Orgs.Application.Dto
{
    [Table("Signature", Schema = "Core")]
    public class UserSignature : BaseEntity
    {
        public string Title { get; private set; }

        public byte[] Certificate { get; private set; }

        public byte[] SignatureImage { get; private set; }

        public string SignatureImageType { get; private set; }

        public byte[] StampImage { get; private set; }

        public string StampImageType { get; private set; }

        public string Email { get; private set; }

        public string Password { get; private set; }

        public string Otp { get; private set; }

        public DateTime? OtpCreated { get; private set; }

        public Guid? UserId { get; private set; }

        public Guid SignServerId { get; private set; }
        public string SendOtpType { get; private set; }

        public static UserSignature CreateUserSignature(Guid signServerId, Guid userId,
            string title, byte[] certificate, byte[] signImage, string signImageType,
            byte[] stampImage, string stampImageType, string account, string password)
        {
            byte[] encryptCertificate = null;
            if (certificate != null)
            {
                encryptCertificate = SecurityHelper.EncryptFile(certificate, AppSettings.EncryptKey);
            }
            string encryptPassword = null;
            if (!string.IsNullOrEmpty(password))
            {
                encryptPassword = SecurityHelper.EncryptText(password, AppSettings.EncryptKey);
            }

            return new UserSignature()
            {
                SignServerId = signServerId,
                UserId = userId,
                Title = title,
                Certificate = encryptCertificate,
                SignatureImage = signImage,
                SignatureImageType = signImageType,
                StampImage = stampImage,
                StampImageType = stampImageType,
                Email = account,
                Password = encryptPassword
            };
        }

        public void DecryptData()
        {
            if (Certificate != null)
            {
                Certificate = SecurityHelper.DecryptFile(Certificate, AppSettings.EncryptKey);
            }

            if (Password != null)
            {
                Password = SecurityHelper.DecryptText(Password, AppSettings.EncryptKey);
            }
        }
    }
}
