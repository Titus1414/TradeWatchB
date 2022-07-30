using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradeWatchB.Models.DTO
{
    public class ComentPostDto
    {
        public int Fid { get; set; }
        public string coment { get; set; }
        public IFormFile image { get; set; }
        public IFormFile video { get; set; }
    }
}
