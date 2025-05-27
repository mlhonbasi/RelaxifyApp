namespace Domain.Models.Queries
{
    public class LastPlayedContentDto
    {
        public Guid ContentId { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime PlayedAt { get; set; }
    }
}
