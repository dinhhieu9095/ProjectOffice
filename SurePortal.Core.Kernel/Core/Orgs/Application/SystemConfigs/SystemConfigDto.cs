using DaiPhatDat.Core.Kernel.Mapper;
using DaiPhatDat.Core.Kernel.Orgs.Domain.Entities;

namespace DaiPhatDat.Core.Kernel.Orgs.Application.Dto
{
    public class SystemConfigDto : IMapping<SystemConfig>
    {

        public string Name { get; set; }

        public string Code { get; set; }

        public string Value { get; set; } = string.Empty;
    }
    public enum SystemConfigKey
    {
        VNPT_Token,
        VNPT_Api,
        TimeServerUrl,
    }
}
