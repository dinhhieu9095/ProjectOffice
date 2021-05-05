using DaiPhatDat.Core.Kernel.Mapper;
using System;

namespace DaiPhatDat.Core.Kernel.Orgs.Application.Dto
{
    public class SignatureServerDto : IMapping<SignatureServer>
    {
        public Guid Id { get; set; }

        public int Type { get; set; }

        public string Url { get; set; }

        public string Title { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public bool OtpRequired { get; set; }

        public int OtpInterval { get; set; }
    }
}
