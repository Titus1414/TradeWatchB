using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradeWatchB.Models.DTO
{
    public class ForumSurveyDto
    {
        public int Cid { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<IFormFile> Files { get; set; }

    }
}
