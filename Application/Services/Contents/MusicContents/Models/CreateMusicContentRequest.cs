using Application.Services.Contents.MainContent.Models;
using Domain.Enums;

namespace Application.Services.Contents.MusicContents.Models
{
    public class CreateMusicContentRequest
    {
        public required CreateContentRequest ContentRequest { get; set; }
        public required MusicCategory Category { get; set; }
        public required string FilePath { get; set; }
        public int Duration { get; set; } 
    }
}
