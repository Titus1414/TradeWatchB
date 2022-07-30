using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradeWatchB.Models.DTO
{
    public class WatchListDto
    {
        public int PairId { get; set; }
        public string tbl { get; set; }
        public bool like { get; set; }
    }
}
