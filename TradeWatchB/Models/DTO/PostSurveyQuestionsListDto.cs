using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradeWatchB.Models.DTO
{
    public class PostSurveyQuestionsListDto
    {
        public string Questions { get; set; }
        public string AnsType { get; set; }
        //public IFormFile Image { get; set; }
        //public List<AnswersListDto> Asns { get; set; }
        public bool IsImage { get; set; }
        public List<string> Asns { get; set; }
    }
}
