using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class UsageStatsDto
    {
        public int TotalMinutes { get; set; }
        public string MostUsedCategory { get; set; } = string.Empty;
        public int DailyAverageMinutes { get; set; }
    }

}
