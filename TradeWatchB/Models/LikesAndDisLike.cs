using System;
using System.Collections.Generic;

#nullable disable

namespace TradeWatchB.Models
{
    public partial class LikesAndDisLike
    {
        public int Id { get; set; }
        public int? Fid { get; set; }
        public int? Uid { get; set; }
        public int? Likes { get; set; }
        public DateTime? Date { get; set; }

        public virtual Fqa FidNavigation { get; set; }
        public virtual Login UidNavigation { get; set; }
    }
}
