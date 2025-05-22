using Application.Services.Contents.MainContent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Contents.BreathingContents.Models
{
    public class UpdateBreathingContentRequest
    {
        public UpdateContentRequest ContentRequest { get; set; } = null!;
        public int StepCount { get; set; }
        public int Duration { get; set; }
        public string Steps { get; set; }
    }
}
