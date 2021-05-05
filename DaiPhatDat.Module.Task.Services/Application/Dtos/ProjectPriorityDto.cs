using DaiPhatDat.Module.Task.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaiPhatDat.Module.Task.Services
{
    public class ProjectPriorityDto
    {
        public ProjectPriorityId Id { get; set; }

        public string Name { get; set; }

        public bool? IsActive { get; set; }
    }
}