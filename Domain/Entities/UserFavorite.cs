using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserFavorite : BaseEntity
    {
        public Guid UserId { get; set; }       // Kullanıcı ID'si (JWT'den gelecek)
        public Guid ContentId { get; set; }    // Hangi içerik favori

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
