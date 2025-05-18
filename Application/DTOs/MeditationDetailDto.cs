using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class MeditationDetailDto
    {
        public Guid ContentId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public int Duration { get; set; }
        public List<MeditationStepDto> Steps { get; set; }
    }

    public class MeditationStepDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }

}
