using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserAchievement : BaseEntity
    {
        public Guid UserId { get; set; }
        public string Key { get; set; } = null!; // örn: "FirstStep", "MindPower"
        public DateTime AchievedAt { get; set; } = DateTime.UtcNow;
        public bool IsSeenByUser { get; set; } = false;
    }
}
