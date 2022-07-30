using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradeWatchB.Models.DTO
{
    public class PostSurveyQuestDto
    {
        public int Cid { get; set; }
        public string Title { get; set; }
        public int ActiveTill { get; set; }
        public List<IFormFile> Images { get; set; }
        public List<PostSurveyQuestionsListDto> Questions { get; set; }
        

    }
}
