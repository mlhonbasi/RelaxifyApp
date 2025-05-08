using Application.Services.Contents.MainContent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Contents.MeditationContents.Models
{
    public class CreateMeditationContentRequest
    {
        public required CreateContentRequest ContentRequest { get; set; }
        public required string Steps { get; set; }
    }
}
