namespace Domain.Entities
{
    public class StressQuestion : BaseEntity
    {
        public string QuestionText { get; set; } = string.Empty;
        public bool IsReversed { get; set; } // Ters puanlı mı? (Pozitif içerik)
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<StressAnswer> Answers { get; set; } = new List<StressAnswer>();
    }
}
