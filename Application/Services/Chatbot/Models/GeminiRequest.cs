using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Services.Chatbot.Models
{
    public class GeminiRequestModel
    {
        public string Page { get; set; }
        public JsonElement Context { get; set; }
        public string UserMessage { get; set; }
    }
}
