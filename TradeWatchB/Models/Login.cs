using System;
using System.Collections.Generic;

#nullable disable

namespace TradeWatchB.Models
{
    public partial class Login
    {
        public Login()
        {
            ComentLiks = new HashSet<ComentLik>();
            Comments = new HashSet<Comment>();
            Fqas = new HashSet<Fqa>();
            LikesAndDisLikes = new HashSet<LikesAndDisLike>();
            Notifications = new HashSet<Notification>();
            PostSurveys = new HashSet<PostSurvey>();
            SqAnsDoneByUsers = new HashSet<SqAnsDoneByUser>();
            WatchLists = new HashSet<WatchList>();
        }

        public int Id { get; set; }
        public string UniqueId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string LoginFrom { get; set; }
        public string PhoneNo { get; set; }
        public string ReffralCode { get; set; }
        public string Password { get; set; }
        public string Image { get; set; }
        public string Country { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<ComentLik> ComentLiks { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Fqa> Fqas { get; set; }
        public virtual ICollection<LikesAndDisLike> LikesAndDisLikes { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<PostSurvey> PostSurveys { get; set; }
        public virtual ICollection<SqAnsDoneByUser> SqAnsDoneByUsers { get; set; }
        public virtual ICollection<WatchList> WatchLists { get; set; }
    }
}
