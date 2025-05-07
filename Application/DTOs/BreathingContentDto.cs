using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class BreathingContentDto
    {
        public Guid ContentId { get; set; }
        public int StepCount { get; set; }
        public int Duration { get; set; }
        public string Steps { get; set; }
    }
}
