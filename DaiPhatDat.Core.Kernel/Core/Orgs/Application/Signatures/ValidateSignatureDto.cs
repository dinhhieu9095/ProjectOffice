using System;

namespace DaiPhatDat.Core.Kernel.Orgs.Application.Dto
{
    public class ValidateSignatureDto
    {
        public string Account { get; set; }

        public string Password { get; set; }

        public byte[] Certificate { get; set; }

        public Guid SignServerId { get; set; }
    }

    public class ValidateSignatureResultDto
    {
        public string CommonName { get; set; }

        public string IssuerName { get; set; }

        public DateTime ValidFrom { get; set; }

        public DateTime ValidTo { get; set; }

        public string SerialNumber { get; set; }
    }
}
