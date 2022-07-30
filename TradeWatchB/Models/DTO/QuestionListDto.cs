using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradeWatchB.Models.DTO
{
    public class QuestionListDto
    {
        public int QuestId { get; set; }
        public int PSId { get; set; }
        public List<int> AnsIds { get; set; }
    }
}
