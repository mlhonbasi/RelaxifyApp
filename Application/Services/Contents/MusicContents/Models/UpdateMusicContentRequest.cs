using Domain.Enums;

namespace Application.Services.Contents.MusicContents.Models
{
    public class UpdateMusicContentRequest
    {
        public MusicCategory Category { get; set; }
        public string FilePath { get; set; }
        public int Duration { get; set; } // Duration in seconds
    }
}
