using Domain.Enums;

namespace Application.DTOs
{
    public class MeditationContentDto
    {
        public Guid ContentId { get; set; }
        public string Steps { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public MeditationPurpose Purpose { get; set; } // Default to Focus purpose
    }
}
