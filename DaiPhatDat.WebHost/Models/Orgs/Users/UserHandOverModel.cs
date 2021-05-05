using SurePortal.Core.Kernel.Mapper;
using SurePortal.Core.Kernel.Orgs.Application.Dto;
using SurePortal.Core.Kernel.Orgs.Domain.Entities;
using System;

namespace SurePortal.WebHost.Models.Orgs.Users
{
    public class UserHandOverModel : IMapping<UserHandOverDto>
    {
        public Guid Id { get; set; }

        public Guid FromUserId { get; set; }

        public UserModel FromUser { get; set; }

        public Guid ToUserId { get; set; }

        public UserModel ToUser { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid AuthorId { get; set; }

        public DateTime ModifiedDate { get; set; }

        public Guid EditorId { get; set; }

    }
}
