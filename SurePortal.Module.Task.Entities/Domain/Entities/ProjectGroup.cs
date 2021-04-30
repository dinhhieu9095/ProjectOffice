using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SurePortal.Module.Task.Entities
{
    [Table("Task.ProjectGroup")]
    public class ProjectGroup
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Guid? CreateBy { get; set; }
        public bool IsActive { get; set; }
        public void Create(string code,string name,Guid? userId)
        {
            Id = Guid.NewGuid();
            CreateBy = userId;
            Code = code;
            Name = name;
            IsActive = true;
        }
        public void Delete()
        {
            IsActive = false;
        }
    }
}