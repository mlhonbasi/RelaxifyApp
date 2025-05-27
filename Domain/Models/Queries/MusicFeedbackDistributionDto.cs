using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Queries
{
    public class MusicFeedbackDistributionDto
    {
        public int RelaxedCount { get; set; }
        public int NeutralCount { get; set; }
        public int StressedCount { get; set; }
    }
}
