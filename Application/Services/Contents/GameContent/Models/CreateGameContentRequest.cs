using Application.Services.Contents.MainContent.Models;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Contents.GameContent.Models
{
    public class CreateGameContentRequest
    {
        public required CreateContentRequest ContentRequest { get; set; }
        public string KeyName { get; set; }
        public GameCategory GameCategory { get; set; }
    }
}
