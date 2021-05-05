using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaiPhatDat.Core.Kernel.Orgs.Domain.Entities
{
    public abstract class BaseEntity
    {
        [Column("ID")]
        public Guid Id { get; protected set; }

        public BaseEntity()
        {
            Id = Guid.NewGuid();
        }
    }
}
