using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradeWatchB.Models.DTO
{
    public class WatchListTrenDto
    {
        public double? Change { get; set; }
        public double? Close { get; set; }
        public string Code { get; set; }
        public long? Gmtoffset { get; set; }
        public double? High { get; set; }
        public int Id { get; set; }
        public double? Low { get; set; }
        public double? Open { get; set; }
        public double? PreviousClose { get; set; }
        public long? Timestamp { get; set; }
        public double? Volume { get; set; }
        public string LikeValue { get; set; }
        public string Notify { get; set; }
        
    }
}
