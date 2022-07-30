using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradeWatchB.Models.DTO
{
    public class GetPostSurveyQueDto
    {
        public int questId { get; set; }
        public string Question { get; set; }
        public string Image { get; set; }
        public string Anstype { get; set; }
        //public List<SurveyAnsDto> Ansr { get; set; }
    }
}
