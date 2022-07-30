using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradeWatchB.Models.DTO
{
    public class GetPostSurveyTotatQA
    {
        public int Id { get; set; }
        public string image { get; set; }
        public string title { get; set; }
        public int? Count { get; set; }
        public int? QuestionsCount { get; set; }
    }
}
