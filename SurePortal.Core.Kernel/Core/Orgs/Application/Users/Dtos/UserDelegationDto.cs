using DaiPhatDat.Core.Kernel.Mapper;
using DaiPhatDat.Core.Kernel.Orgs.Domain.Entities;
using System;

namespace DaiPhatDat.Core.Kernel.Orgs.Application.Dto
{
    public class UserDelegationDto : IMapping<UserDelegation>
    {
        public Guid Id { get; set; }

        public Guid FromUserId { get; set; }

        public UserDto FromUser { get; set; }

        public Guid ToUserId { get; set; }

        public UserDto ToUser { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid AuthorId { get; set; }

        public DateTime ModifiedDate { get; set; }

        public Guid EditorId { get; set; }
    }
}
