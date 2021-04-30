using SurePortal.Core.Kernel.Mapper;
using SurePortal.Module.Task.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurePortal.Module.Task.Services
{
    public class TaskItemPriorityDto : TaskItemPriority, IMapping<TaskItemPriority>
    {
    }
}