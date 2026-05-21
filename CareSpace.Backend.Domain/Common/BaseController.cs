using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareSpace.Backend.Domain.Common
{
    public abstract class BaseEntity
    {
        public virtual Guid Id { get; set; }

        public virtual DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual DateTime? UpdatedAt { get; set; }

        public virtual DateTime? DeletedAt { get; set; }
    }
}