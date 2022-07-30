using System;
using System.Collections.Generic;

#nullable disable

namespace TradeWatchB.Models
{
    public partial class CcrealTimePair
    {
        public CcrealTimePair()
        {
            Notifications = new HashSet<Notification>();
            WatchLists = new HashSet<WatchList>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public long? Timestamp { get; set; }
        public long? Gmtoffset { get; set; }
        public double? Open { get; set; }
        public double? High { get; set; }
        public double? Low { get; set; }
        public double? Close { get; set; }
        public double? Volume { get; set; }
        public double? PreviousClose { get; set; }
        public double? Change { get; set; }
        public double? ChangeP { get; set; }

        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<WatchList> WatchLists { get; set; }
    }
}
