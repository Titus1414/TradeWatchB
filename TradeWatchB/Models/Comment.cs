using System;
using System.Collections.Generic;

#nullable disable

namespace TradeWatchB.Models
{
    public partial class Comment
    {
        public Comment()
        {
            ComentLiks = new HashSet<ComentLik>();
        }

        public int Id { get; set; }
        public int? Fid { get; set; }
        public int? Uid { get; set; }
        public string Comnts { get; set; }
        public string Image { get; set; }
        public string Video { get; set; }
        public long? Likes { get; set; }
        public long? Dislike { get; set; }
        public int? IsLike { get; set; }
        public DateTime? Date { get; set; }

        public virtual Fqa FidNavigation { get; set; }
        public virtual Login UidNavigation { get; set; }
        public virtual ICollection<ComentLik> ComentLiks { get; set; }
    }
}
