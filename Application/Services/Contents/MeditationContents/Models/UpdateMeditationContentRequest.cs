using Application.Services.Contents.MainContent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Contents.MeditationContents.Models
{
    public class UpdateMeditationContentRequest
    {
        public UpdateContentRequest ContentRequest { get; set; } = null!;
        public string Steps { get; set; }
    }
}
