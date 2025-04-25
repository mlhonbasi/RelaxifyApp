using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class MusicContent : BaseEntity
    {
        public Content Content { get; set; } // Navigation property to Content
        public Guid ContentId { get; set; }
        public MusicCategory Category { get; set; } // Music category (e.g., Relaxation, Nature, etc.)
        public string FilePath { get; set; } // Path to the music file
        public int Duration { get; set; } // Duration in seconds
    }
}
