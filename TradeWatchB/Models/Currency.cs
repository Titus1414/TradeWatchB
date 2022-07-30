using System;
using System.Collections.Generic;

#nullable disable

namespace TradeWatchB.Models
{
    public partial class Currency
    {
        public Currency()
        {
            Fqas = new HashSet<Fqa>();
            PostSurveys = new HashSet<PostSurvey>();
        }

        public int Id { get; set; }
        public int? ExId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string FkCode { get; set; }
        public string IconName { get; set; }
        public bool? IsActive { get; set; }
        public int? FkId { get; set; }

        public virtual Exchange Ex { get; set; }
        public virtual ICollection<Fqa> Fqas { get; set; }
        public virtual ICollection<PostSurvey> PostSurveys { get; set; }
    }
}
