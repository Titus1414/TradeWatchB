using System;
using System.Collections.Generic;

#nullable disable

namespace TradeWatchB.Models
{
    public partial class FqaImage
    {
        public int Id { get; set; }
        public int? Fid { get; set; }
        public string Image { get; set; }

        public virtual Fqa FidNavigation { get; set; }
    }
}
