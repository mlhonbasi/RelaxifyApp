using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class BreathingContentListDto
    {
        public Guid ContentId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public int StepCount { get; set; }
        public int DurationInSeconds { get; set; }
        public bool IsFavorite { get; set; }
    }
}
