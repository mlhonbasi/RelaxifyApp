﻿using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class LogUsageRequest
    {
        public Guid ContentId { get; set; }
        public ContentCategory Category { get; set; }
        public int DurationInSeconds { get; set; }
    }

}
