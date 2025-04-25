namespace Domain.Entities
{
    public class BreathingContent : BaseEntity
    {
        public Content Content { get; set; } // Navigation property to Content
        public Guid ContentId { get; set; }
        public int StepCount { get; set; }
        public int Duration { get; set; } // in seconds
        public string Steps { get; set; } // JSON array of steps
    }
}
