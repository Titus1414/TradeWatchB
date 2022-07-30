using System;
using System.Collections.Generic;

#nullable disable

namespace TradeWatchB.Models
{
    public partial class Fqa
    {
        public Fqa()
        {
            Comments = new HashSet<Comment>();
            LikesAndDisLikes = new HashSet<LikesAndDisLike>();
        }

        public int Id { get; set; }
        public int? Cid { get; set; }
        public int? Uid { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Files { get; set; }
        public DateTime? Date { get; set; }
        public long? Likes { get; set; }
        public long? DisLikes { get; set; }
        public long? Comnts { get; set; }
        public long? Share { get; set; }
        public long? Views { get; set; }
        public int? IsLike { get; set; }
        public bool? IsActive { get; set; }

        public virtual Currency CidNavigation { get; set; }
        public virtual Login UidNavigation { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<LikesAndDisLike> LikesAndDisLikes { get; set; }
    }
}
