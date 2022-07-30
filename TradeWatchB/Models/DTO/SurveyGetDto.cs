using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradeWatchB.Models.DTO
{
    public class SurveyGetDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public string imgs { get; set; }
        public string Name { get; set; }
        public string userImg { get; set; }
        //public string cmnt { get; set; }
        public int? CountLike { get; set; }
        public int? CountView { get; set; }
        public int? CountComnt { get; set; }
        public int? CountDislike { get; set; }
        public int? CountShare { get; set; }
        public int? IsLike { get; set; }
        public DateTime? Date { get; set; }

    }
}
