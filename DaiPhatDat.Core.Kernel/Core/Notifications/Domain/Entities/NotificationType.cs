using DaiPhatDat.Core.Kernel.Domain.Entities;
using DaiPhatDat.Core.Kernel.Notifications.Domain.ValueObjects;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaiPhatDat.Core.Kernel.Notifications.Domain.Entities
{
    [Table("NotificationTypes", Schema = "Core")]
    public class NotificationType : BaseEntity
    {
        public string Name { get; private set; }

        public string Description { get; private set; }

        public string Template { get; private set; }

        public string ModuleCode { get; private set; }

        public NotificationActionTypes ActionType { get; private set; }

        public string ActionTypeName { get; private set; }

        public bool IsDeleted { get; set; }

        public static NotificationType Create(string name, string description,
            string template, string moduleCode, NotificationActionTypes actionType)
        {
            return new NotificationType()
            {
                ActionType = actionType,
                ActionTypeName = actionType.ToString(),
                Name = name,
                Description = description,
                IsDeleted = false,
                Template = template,
                ModuleCode = moduleCode
            };
        }

        public void Update(string name, string description,
            string template, string moduleCode, NotificationActionTypes actionType)
        {
            ModifiedDate = DateTime.Now;
            Name = name;
            Description = description;
            Template = template;
            ActionType = actionType;
            ActionTypeName = actionType.ToString();
            ModuleCode = moduleCode;
        }
    }
}
