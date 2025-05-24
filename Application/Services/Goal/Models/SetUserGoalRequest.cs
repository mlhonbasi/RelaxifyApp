using Domain.Enums;

namespace Application.Services.Goal.Models
{
    public class SetUserGoalRequest
    {
        public ContentCategory Category { get; set; }

        public int TargetDays { get; set; }

        public int TargetMinutes { get; set; }
    }
}
