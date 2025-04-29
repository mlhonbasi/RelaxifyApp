using Domain.Enums;

namespace Application.DTOs
{
    public class ContentDto
    {
       public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
        public ContentCategory Category { get; set; }
        public string ImageUrl { get; set; }
    }
}
