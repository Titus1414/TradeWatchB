using System;
using System.Collections.Generic;

#nullable disable

namespace TradeWatchB.Models
{
    public partial class SqAnsDoneByUser
    {
        public int Id { get; set; }
        public int? AnsId { get; set; }
        public int? Qid { get; set; }
        public int? Uid { get; set; }

        public virtual Sqanswer Ans { get; set; }
        public virtual SurveyQuestion QidNavigation { get; set; }
        public virtual Login UidNavigation { get; set; }
    }
}
