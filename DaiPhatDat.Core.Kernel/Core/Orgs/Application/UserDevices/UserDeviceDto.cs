using DaiPhatDat.Core.Kernel.Mapper;
using DaiPhatDat.Core.Kernel.Orgs.Domain.Entities;
using System;

namespace DaiPhatDat.Core.Kernel.Orgs.Application.Dto
{
    public class UserDeviceDto : IMapping<UserDevice>
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Name { get; set; }

        public string AppName { get; set; }

        public string AppVersion { get; set; }

        public string OSPlatform { get; set; }

        public string OSVersion { get; set; }

        public string SerialNumber { get; set; }

        public string IMEI { get; set; }

        public string FireBaseToken { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime LastUpdatedDate { get; set; }

    }
    public class UserDeviceInfoDto : IMapping<UserDevice>
    {
        public string OSPlatform { get; set; }
        public string FireBaseToken { get; set; }
    }
}
