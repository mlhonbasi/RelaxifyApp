using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class BreathingContent
    {
        public Guid Id { get; set; }
        public Content Content { get; set; } // Navigation property to Content
        public Guid ContentId { get; set; }
        public int StepCount { get; set; }
        public int Duration { get; set; } // in seconds
        public string Steps { get; set; } // JSON array of steps
    }
}
