using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ContentFeedback.Models
{
    public class CreateFeedbackRequest
    {
        public Guid ContentId { get; set; }
        public int Duration { get; set; }
        public FeedbackType Feedback { get; set; }
        public int Category { get; set; }
    }
}
