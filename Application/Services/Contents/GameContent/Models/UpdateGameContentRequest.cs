using Application.Services.Contents.MainContent.Models;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Contents.GameContent.Models
{
    public class UpdateGameContentRequest
    {
        public UpdateContentRequest ContentRequest { get; set; } = null!;
        public GameCategory GameCategory { get; set; }
        public string KeyName { get; set; }
    }
}
