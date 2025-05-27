namespace Domain.Models.Queries
{
    public class ContentFeedbackSummaryDto
    {
        public int TodayDurationInMinutes { get; set; }
        public MostPlayedContentDto? MostPlayed { get; set; }
        public LastPlayedContentDto? LastPlayed { get; set; }
    }
}
