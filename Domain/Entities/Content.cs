using Domain.Enums;

namespace Domain.Entities
{
    public class Content
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ContentCategory Category { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
        public string ImagePath { get; set; }
    }
}
