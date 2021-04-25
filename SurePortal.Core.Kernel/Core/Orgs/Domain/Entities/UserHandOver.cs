using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurePortal.Core.Kernel.Orgs.Domain.Entities
{
    //[Table("UserHandOvers", Schema = "Core")]
    public class UserHandOver
    {
        public System.Guid ID { get; set; }
        public Nullable<System.Guid> FromUserID { get; set; }
        public Nullable<System.Guid> ToUserID { get; set; }
        public Nullable<System.DateTime> FromDate { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public Nullable<System.Guid> AuthorID { get; set; }
        public string CcUserId { get; set; }
    }
}
