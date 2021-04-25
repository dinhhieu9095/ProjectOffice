using SurePortal.Core.Kernel.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace SurePortal.Core.Kernel.Firebase.Models
{
    public class SendMessageData
    {
        protected SendMessageData()
        {

        }
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Body { get; private set; }
        public string ImageUrl { get; private set; }
        public SurePortalModules Module { get; private set; }
        public ActionType Action { get; private set; }
        public Dictionary<string, string> CustomData { get; private set; }
        public static SendMessageData CreateDisplayMessage(string title, string body, string imageUrl,
            SurePortalModules module, Guid? objectId = null)
        {
            var message = new SendMessageData()
            {
                Id = Guid.NewGuid(),
                Action = ActionType.Display,
                Title = title,
                Body = body,
                ImageUrl = imageUrl,
                CustomData = new Dictionary<string, string>(),
                Module = module
            };
            message.CustomData.Add("Module", module.ToString());
            if (objectId.HasValue)
            {
                message.CustomData.Add("ObjectId", objectId.Value.ToString());
            }
            return message;
        }

        public static SendMessageData CreateSyncDataMessage(string body, string imageUrl,
            SurePortalModules module, Guid? objectId = null)
        {
            var message = new SendMessageData()
            {
                Id = Guid.NewGuid(),
                Action = ActionType.SyncData,
                Title = $"{ActionType.SyncData.ToString()}-{DateTime.Now.ToString("dd/MM/yyyy HH:mm")}",
                Body = body,
                ImageUrl = imageUrl,
                CustomData = new Dictionary<string, string>(),
                Module = module
            };

            message.CustomData.Add("Module", module.ToString());
            if (objectId.HasValue)
            {
                message.CustomData.Add("ObjectId", objectId.Value.ToString());
            }
            return message;
        }
    }

    public enum ActionType
    {
        /// <summary>
        /// Hiển thị trên thiết bị
        /// </summary>
        Display,

        /// <summary>
        /// Đồng bộ dữ liệu
        /// </summary>
        SyncData
    }
}
