namespace Domain.Models.Queries
{
    public class MostPlayedContentDto
    {
        public Guid ContentId { get; set; }
        public string Title { get; set; } = string.Empty;
        public int PlayCount { get; set; }
    }
}
