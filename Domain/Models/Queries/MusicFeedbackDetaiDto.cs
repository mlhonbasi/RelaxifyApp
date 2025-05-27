using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Queries
{
    public class MusicFeedbackDetailDto
    {
        public Guid ContentId { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Count { get; set; }
    }
}
