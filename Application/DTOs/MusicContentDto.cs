using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class MusicContentDto
    {
        public Guid ContentId { get; set; }
        public MusicCategory Category { get; set; }

        public string FilePath { get; set; }
        public int Duration { get; set; } // Duration in seconds

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
