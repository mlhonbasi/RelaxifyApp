using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class BreathingDetailDto
    {
        public Guid ContentId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int StepCount { get; set; }
        public int DurationInSeconds { get; set; }
        public List<BreathingStepDto> Steps { get; set; }
    }
    public class BreathingStepDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
    }
}
