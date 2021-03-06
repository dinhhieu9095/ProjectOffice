using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaiPhatDat.Module.Task.Services
{
    public partial class ActionDto
    {
        public bool IsActive { get; set; }
        [StringLength(128)]
        public string Name { get; set; }
        public Guid Id { get; set; }
    }
    public class ItemActionDto
    {
        public string Code { get; set; }

        public string Name { get; set; }
    }
}
