namespace Application.DTOs
{
    public class GameContentListDto
    {
        public Guid ContentId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public bool IsFavorite { get; set; }

    }
}
