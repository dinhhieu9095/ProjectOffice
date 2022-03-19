using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;

namespace DaiPhatDat.Module.Task.Entities
{
    [Table("Task.AdminCategory")]
    public class AdminCategory
    {
        public AdminCategory()
        {
        }

        public Guid Id { get; set; }

        public string Summary { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CreatedDate { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public Guid? ModifiedBy { get; set; }
    }
}