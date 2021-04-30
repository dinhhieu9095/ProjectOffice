using SurePortal.Core.Kernel.Mapper;
using SurePortal.Module.Task.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurePortal.Module.Task.Services
{
    public class TaskItemStatusDto : IMapping<TaskItemStatus>
    {
        public TaskItemStatusId Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public bool? IsActive { get; set; }

        public string Color { get; set; }
    }
}