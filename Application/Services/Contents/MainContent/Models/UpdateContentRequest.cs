using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Contents.MainContent.Models
{
    public class UpdateContentRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string? ImagePath { get; set; }
        public MeditationPurpose Purpose { get; set; } // Default to Focus purpose
    }
}
