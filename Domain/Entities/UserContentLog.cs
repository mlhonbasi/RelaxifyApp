using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserContentLog : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid ContentId { get; set; }
        public ContentCategory Category { get; set; }
        public int DurationInSeconds { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
