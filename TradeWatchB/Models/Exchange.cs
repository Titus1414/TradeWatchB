﻿using System;
using System.Collections.Generic;

#nullable disable

namespace TradeWatchB.Models
{
    public partial class Exchange
    {
        public Exchange()
        {
            Currencies = new HashSet<Currency>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string OperatingMic { get; set; }
        public string Country { get; set; }
        public string Currency { get; set; }
        public string Image { get; set; }
        public int? Orderby { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<Currency> Currencies { get; set; }
    }
}
