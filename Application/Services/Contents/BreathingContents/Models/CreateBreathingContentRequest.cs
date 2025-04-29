using Application.Services.Contents.MainContent.Models;

namespace Application.Services.Contents.BreathingContents.Models
{
    public class CreateBreathingContentRequest
    {
        public CreateContentRequest ContentRequest { get; set; }  
        public int StepCount { get; set; }
        public int Duration { get; set; }
        public string Steps { get; set; }
    }
}
