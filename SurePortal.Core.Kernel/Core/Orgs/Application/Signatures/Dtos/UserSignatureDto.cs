using SurePortal.Core.Kernel.Mapper;
using System;

namespace SurePortal.Core.Kernel.Orgs.Application.Dto
{
    public class UserSignatureDto : IMapping<UserSignature>
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public byte[] Certificate { get; set; }

        public byte[] SignatureImage { get; set; }

        public string SignatureImageType { get; set; }

        public byte[] StampImage { get; set; }

        public string StampImageType { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Otp { get; set; }

        public DateTime? OtpCreated { get; set; }

        public Guid? UserId { get; set; }

        public Guid SignServerId { get; set; }
        public string SendOtpType { get; set; }
    }

    public class UserSignatureInfo : IMapping<UserSignatureDto>
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        //public byte[] SignatureImage { get; set; }

        //public string SignatureImageType { get; set; }

        //public byte[] StampImage { get; set; }

        //public string StampImageType { get; set; }

        public string Email { get; set; }

        public Guid SignServerId { get; set; }

        public string SignServerName { get; set; }
    }

    public class CreateUserSignatureDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Account { get; set; }

        public string Password { get; set; }

        public Guid SignServerId { get; set; }

        public string SignServerName { get; set; }

        public Guid UserId { get; set; }

        public string Certificate { get; set; }

        public byte[] CertificateBin
        {
            get
            {
                if (!string.IsNullOrEmpty(Certificate))
                {
                    return Convert.FromBase64String(Certificate);
                }
                else
                {
                    return null;
                }
            }
        }

        public byte[] SignImage { get; set; }

        public string SignImageType { get; set; }

        public byte[] StampImage { get; set; }

        public string StampImageType { get; set; }

    }

}
