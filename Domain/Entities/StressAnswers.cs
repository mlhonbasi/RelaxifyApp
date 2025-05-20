using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class StressAnswer : BaseEntity
    {
        public string Text { get; set; } = string.Empty;
        public int Value { get; set; } // 0-4 arası değer
        public int Order { get; set; } // Sıralama için
        public StressQuestion? Question { get; set; } // İlişkilendirilmiş soru
    }
}
