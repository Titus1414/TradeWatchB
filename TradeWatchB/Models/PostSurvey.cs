using System;
using System.Collections.Generic;

#nullable disable

namespace TradeWatchB.Models
{
    public partial class PostSurvey
    {
        public PostSurvey()
        {
            SurveyQuestions = new HashSet<SurveyQuestion>();
        }

        public int Id { get; set; }
        public int? Uid { get; set; }
        public int? Cid { get; set; }
        public string Title { get; set; }
        public DateTime? UntilActive { get; set; }
        public bool? IsActive { get; set; }
        public int? CompleteCount { get; set; }
        public int? QuestionCount { get; set; }

        public virtual Currency CidNavigation { get; set; }
        public virtual Login UidNavigation { get; set; }
        public virtual ICollection<SurveyQuestion> SurveyQuestions { get; set; }
    }
}
