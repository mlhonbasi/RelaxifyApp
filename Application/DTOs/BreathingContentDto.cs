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

        // Content tablosundan gelen alanlar
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
