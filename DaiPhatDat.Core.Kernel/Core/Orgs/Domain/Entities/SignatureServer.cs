using DaiPhatDat.Core.Kernel.Orgs.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaiPhatDat.Core.Kernel.Orgs.Application.Dto
{
    [Table("SignServer", Schema = "Core")]
    public class SignatureServer : BaseEntity
    {
        public int Type { get; set; }

        public string Url { get; set; }

        public string Title { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public bool OtpRequired { get; set; }

        public int OtpInterval { get; set; }
    }
}
