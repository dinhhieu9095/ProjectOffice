using DaiPhatDat.Core.Kernel.Mapper;
using DaiPhatDat.Module.Task.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaiPhatDat.Module.Task.Services
{
    public class TaskItemPriorityDto : TaskItemPriority, IMapping<TaskItemPriority>
    {
    }
}