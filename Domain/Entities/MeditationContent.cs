using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class MeditationContent
    {
        public Guid Id { get; set; }
        public Content Content { get; set; } // Navigation property to Content
        public Guid ContentId { get; set; }
        public string Steps { get; set; } // JSON array of steps
    }
}
