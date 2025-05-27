using Domain.Enums;

namespace Domain.Entities
{
    public class MeditationContent : BaseEntity
    {
        public Content Content { get; set; } // Navigation property to Content
        public Guid ContentId { get; set; }
        public string Steps { get; set; } // JSON array of steps
        public MeditationPurpose Purpose { get; set; } // Enum for meditation purpose
    }
}
