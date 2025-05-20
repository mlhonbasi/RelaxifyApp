using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class StressQuestionDto
    {
        public Guid Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public bool IsReversed { get; set; }
        public List<StressAnswerDto> Answers { get; set; } = new();
    }
}
