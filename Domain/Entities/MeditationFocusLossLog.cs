using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class MeditationFocusLossLog : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid ContentId { get; set; }
        public int FocusLossInSeconds { get; set; }
        public double FocusLossRate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
