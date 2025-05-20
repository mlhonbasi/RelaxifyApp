using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.StressTestResult.Models
{
    public class StressTestResultRequest
    {
        public int Score { get; set; }
        public string StressLevel { get; set; } = string.Empty;
    }
}
