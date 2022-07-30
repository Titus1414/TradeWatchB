using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradeWatchB.Models.DTO
{
    public class SurveyAnsDto
    {
        public int? QuestId { get; set; }
        public int AnsId { get; set; }
        public string Ans { get; set; }
        public long? IsChoose { get; set; }
        public bool DoneByMe { get; set; }
    }
}
