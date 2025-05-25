using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class AchievementDto
    {
        public string Name { get; set; } = "";
        public string Icon { get; set; } = "";
        public string Description { get; set; } = "";
        public bool Achieved { get; set; } = false;
        public DateTime? AchievedAt { get; set; }
    }
}
