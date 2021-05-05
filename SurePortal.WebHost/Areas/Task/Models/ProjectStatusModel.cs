using DaiPhatDat.Module.Task.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaiPhatDat.Module.Task.Web
{
    public class ProjectStatusModel
    {
        public ProjectStatusId Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public bool? IsActive { get; set; }

        public string Color { get; set; }
    }
}