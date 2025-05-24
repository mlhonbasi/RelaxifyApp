using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserGoal : BaseEntity
    {
        public Guid UserId { get; set; }

        public ContentCategory Category { get; set; }  // örn: Meditation, Breathing, Music, Game

        public int TargetDays { get; set; }            // Haftalık hedef: kaç gün kullanmalı?

        public int TargetMinutes { get; set; }         // Hedef süre (dakika)

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
