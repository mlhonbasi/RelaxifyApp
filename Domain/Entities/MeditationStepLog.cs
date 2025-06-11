using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class MeditationStepLog : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid ContentId { get; set; }
        public int StepIndex { get; set; }
        public int DurationInSeconds { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}
