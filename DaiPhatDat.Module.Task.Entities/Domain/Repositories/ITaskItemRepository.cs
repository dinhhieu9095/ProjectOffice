using DaiPhatDat.Core.Kernel.AmbientScope;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaiPhatDat.Module.Task.Entities
{
    public interface ITaskItemRepository : IRepository<TaskItem>
    {
    }
    public interface ITaskItemAssignRepository : IRepository<TaskItemAssign>
    {
    }
}
