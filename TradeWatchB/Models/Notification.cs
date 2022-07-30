﻿using System;
using System.Collections.Generic;

#nullable disable

namespace TradeWatchB.Models
{
    public partial class Notification
    {
        public int Id { get; set; }
        public int? Uid { get; set; }
        public int? Ccid { get; set; }
        public int? Forexid { get; set; }
        public int? Stkid { get; set; }
        public int? CommId { get; set; }
        public double? MinVal { get; set; }
        public double? MaxVal { get; set; }
        public bool? Notify { get; set; }

        public virtual CcrealTimePair Cc { get; set; }
        public virtual CommRealTimePair Comm { get; set; }
        public virtual ForexRealTimePair Forex { get; set; }
        public virtual NserealTimePair Stk { get; set; }
        public virtual Login UidNavigation { get; set; }
    }
}