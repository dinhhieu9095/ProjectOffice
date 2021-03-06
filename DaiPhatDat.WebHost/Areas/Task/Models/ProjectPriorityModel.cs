using System.ComponentModel.DataAnnotations.Schema;

namespace DaiPhatDat.Module.Task.Entities
{
    public class ProjectPriorityModel
    {
        public ProjectPriorityId Id { get; set; }

        public string Name { get; set; }

        public bool? IsActive { get; set; }
    }
}