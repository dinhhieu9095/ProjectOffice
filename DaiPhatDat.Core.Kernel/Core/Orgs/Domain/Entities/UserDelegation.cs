using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaiPhatDat.Core.Kernel.Orgs.Domain.Entities
{
    //[Table("UserDelegations", Schema = "Core")]
    public class UserDelegation
    {

        public System.Guid ID { get; set; }
        public Nullable<System.Guid> FromUserID { get; set; }
        public Nullable<System.Guid> ToUserID { get; set; }
        public Nullable<System.DateTime> FromDate { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public Nullable<System.Guid> AuthorID { get; set; }
        public Nullable<System.Guid> EditorID { get; set; }
        public Nullable<System.DateTime> Modified { get; set; }
        public string CcUserID { get; set; }

        public static UserDelegation Create(Guid fromUserID, Guid toUserID, DateTime fromDate, DateTime toDate,
            DateTime createdDate, Guid authorID)
        {
            var userDelegation = new UserDelegation();
            userDelegation.ID = Guid.NewGuid();
            userDelegation.FromUserID = fromUserID;
            userDelegation.ToUserID = toUserID;
            userDelegation.FromDate = fromDate;
            userDelegation.ToDate = toDate;
            userDelegation.Created = createdDate;
            userDelegation.AuthorID = authorID;
            userDelegation.Modified = createdDate;
            userDelegation.EditorID = authorID;
            return userDelegation;
        }


        public void Update(Guid fromUserID, Guid toUserID, DateTime fromDate, DateTime toDate, Guid editorID)
        {
            FromUserID = fromUserID;
            ToUserID = toUserID;
            FromDate = fromDate;
            ToDate = toDate;
            Modified = DateTime.Now;
            EditorID = editorID;
        }
    }
}
