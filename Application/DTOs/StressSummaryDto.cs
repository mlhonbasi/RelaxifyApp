using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class StressSummaryDto
    {
        public string Filter { get; set; } = string.Empty; // "daily", "weekly", "monthly"
        public double AverageScore { get; set; }
        public int MaxScore { get; set; }
        public int MinScore { get; set; }
    }
}
