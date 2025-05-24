using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ProgressStatsDto
    {
        public ContentCategory Category { get; set; }

        public int UsedDays { get; set; }

        public int UsedMinutes { get; set; }

        public int TargetDays { get; set; }

        public int TargetMinutes { get; set; }

        public double CompletionRateDays => TargetDays == 0 ? 0 : Math.Min((UsedDays * 100.0) / TargetDays, 100);

        public double CompletionRateMinutes => TargetMinutes == 0 ? 0 : Math.Min((UsedMinutes * 100.0) / TargetMinutes, 100);

        public bool GoalCompleted => CompletionRateDays >= 100 && CompletionRateMinutes >= 100;
    }
}
