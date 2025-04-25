using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class GameContent : BaseEntity
    {
        public Content Content { get; set; } // Navigation property to Content
        public Guid ContentId { get; set; }  
        public string KeyName { get; set; } // Unique key for the game
        public GameCategory Category { get; set; }
    }
}
