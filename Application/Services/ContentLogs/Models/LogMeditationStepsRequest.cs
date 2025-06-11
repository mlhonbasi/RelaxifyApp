using Application.DTOs;

namespace Application.Services.ContentLogs.Models
{
    public class LogMeditationStepsRequest
    {
        public Guid ContentId { get; set; }
        public List<StepDurationDto> StepDurations { get; set; }
    }
}
