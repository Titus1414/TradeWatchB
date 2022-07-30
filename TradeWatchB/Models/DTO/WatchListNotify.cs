using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradeWatchB.Models.DTO
{
    public class WatchListNotify
    {
        public int PairId { get; set; }
        public string tbl { get; set; }
        public bool notify { get; set; }
        public float Min { get; set; }
        public float Max { get; set; }
    }
}
