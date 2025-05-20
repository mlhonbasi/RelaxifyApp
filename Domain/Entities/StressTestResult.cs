using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class StressTestResult : BaseEntity
    {
        public Guid UserId { get; set; }
        public int Score { get; set; }
        public string StressLevel { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
