using System.ComponentModel.DataAnnotations.Schema;

namespace DaiPhatDat.Core.Kernel.Orgs.Domain.Entities
{
    [Table("JobTitles", Schema = "dbo")]
    public class UserJobTitle : BaseEntity
    {
        public string Name { get; set; }

        public int? OrderNumber { get; set; }

        public string Code { get; set; }

        public bool IsActive { get; set; }
    }
}
