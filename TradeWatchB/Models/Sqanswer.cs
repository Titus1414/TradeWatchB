using System;
using System.Collections.Generic;

#nullable disable

namespace TradeWatchB.Models
{
    public partial class Sqanswer
    {
        public Sqanswer()
        {
            SqAnsDoneByUsers = new HashSet<SqAnsDoneByUser>();
        }

        public int Id { get; set; }
        public int? Sqid { get; set; }
        public string Ans { get; set; }
        public long? IsChoose { get; set; }

        public virtual SurveyQuestion Sq { get; set; }
        public virtual ICollection<SqAnsDoneByUser> SqAnsDoneByUsers { get; set; }
    }
}
