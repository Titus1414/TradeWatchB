using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradeWatchB.Models.DTO
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string comment { get; set; }
        public string userName { get; set; }
        public string userImage { get; set; }
        public string image { get; set; }
        public string Video { get; set; }
        public int like { get; set; }
        public int dislike { get; set; }
        public int Islike { get; set; }
        public DateTime? Date { get; set; }
    }
}
