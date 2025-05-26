using Domain.Enums;

namespace Domain.Entities
{
    public class ContentFeedbackLog : BaseEntity
    {
        public Guid ContentId { get; set; }
        public ContentCategory Category { get; set; } // Music, Meditation, Game, etc.
        public FeedbackType Feedback { get; set; } // relaxed, neutral, stressed
        public int Duration { get; set; } // saniye
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid? UserId { get; set; } // opsiyonel
    }
}
