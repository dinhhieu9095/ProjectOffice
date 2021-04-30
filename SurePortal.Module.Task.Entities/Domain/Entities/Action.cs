using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurePortal.Module.Task.Entities
{
    [Table("Task.Action")]
    public partial class Action
    {
        public bool IsActive { get; set; }
        [StringLength(128)]
        public string Name { get; set; }
        public ActionId Id { get; set; }
    }
}
