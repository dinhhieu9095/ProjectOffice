using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaiPhatDat.Module.Task.Entities
{
    [Table("Task.TaskItemGroup")]
    public class TaskItemGroup
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Guid? CreateBy { get; set; }

        public bool? IsActive { get; set; }

        public void Create(string name,Guid? userId)
        {
            IsActive = true;
            CreateBy = userId;
            Name = name;
        }
        public void Delete()
        {
            IsActive = false;
        }
    }
}