using System;
using System.Collections.Generic;

#nullable disable

namespace TradeWatchB.Models
{
    public partial class ComentLik
    {
        public int Id { get; set; }
        public int? CnId { get; set; }
        public int? Uid { get; set; }
        public int? Likes { get; set; }
        public DateTime? Date { get; set; }

        public virtual Comment Cn { get; set; }
        public virtual Login UidNavigation { get; set; }
    }
}
