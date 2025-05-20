using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class StressAnswerDto
    {
        public Guid Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public int Value { get; set; }
        public int Order { get; set; }
    }
}
