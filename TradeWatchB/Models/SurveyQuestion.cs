using System;
using System.Collections.Generic;

#nullable disable

namespace TradeWatchB.Models
{
    public partial class SurveyQuestion
    {
        public SurveyQuestion()
        {
            SqAnsDoneByUsers = new HashSet<SqAnsDoneByUser>();
            Sqanswers = new HashSet<Sqanswer>();
        }

        public int Id { get; set; }
        public int? Psid { get; set; }
        public string Quest { get; set; }
        public string Image { get; set; }
        public long? Views { get; set; }
        public string AnsType { get; set; }

        public virtual PostSurvey Ps { get; set; }
        public virtual ICollection<SqAnsDoneByUser> SqAnsDoneByUsers { get; set; }
        public virtual ICollection<Sqanswer> Sqanswers { get; set; }
    }
}
