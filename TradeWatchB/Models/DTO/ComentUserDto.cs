using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradeWatchB.Models.DTO
{
    public class ComentUserDto
    {
        public int Uid { get; set; }
        public int Fid { get; set; }
        public int IsLike { get; set; }
        public string fltr { get; set; }
    }
}
