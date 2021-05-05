using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaiPhatDat.Core.Kernel.Orgs.Domain.Entities
{
    [Table("UserDevices", Schema = "Core")]
    public class UserDevice : BaseEntity
    {
        public Guid UserId { get; private set; }

        public string Name { get; private set; }

        public string AppName { get; private set; }

        public string AppVersion { get; private set; }

        public string OSPlatform { get; private set; }

        public string OSVersion { get; private set; }

        public string SerialNumber { get; private set; }

        public string IMEI { get; private set; }

        public string FireBaseToken { get; private set; }

        public bool IsActive { get; private set; }

        public DateTime CreatedDate { get; private set; }

        public DateTime LastUpdatedDate { get; private set; }

        public static UserDevice Create(Guid userId, string name, string appName, string appVersion,
            string osPlatform, string osVersion, string serialNumber, string imei, string fireBaseToken)
        {
            return new UserDevice()
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                AppName = appName,
                AppVersion = appVersion,
                OSPlatform = osPlatform,
                OSVersion = osVersion,
                SerialNumber = serialNumber,
                IMEI = imei,
                FireBaseToken = fireBaseToken,
                IsActive = true,
                CreatedDate = DateTime.Now,
                LastUpdatedDate = DateTime.Now,
                Name = name
            };
        }

        public void Update(string name, string appName, string appVersion,
         string osPlatform, string osVersion, string fireBaseToken)
        {
            IsActive = true;
            Name = name;
            AppName = appName;
            AppVersion = appVersion;
            OSPlatform = osPlatform;
            OSVersion = osVersion;
            FireBaseToken = fireBaseToken;
            LastUpdatedDate = DateTime.Now;
        }

        public void DeActive()
        {
            IsActive = false;
            LastUpdatedDate = DateTime.Now;
        }
    }
}
