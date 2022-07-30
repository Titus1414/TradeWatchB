using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeWatchB.Models;
using TradeWatchB.Services.YouTube;

namespace TradeWatchB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YouTubeController : ControllerBase
    {
        private readonly IYouTubeSearch _youTubeSearch;
        public YouTubeController(IYouTubeSearch youTubeSearch)
        {
            _youTubeSearch = youTubeSearch;
        }
        [HttpGet]
        [Route("YouTubeSearch")]
        public async Task<IActionResult> YouTubeSearch(string q)
        {
            var result = await _youTubeSearch.SearchByKeywword(q);
            return Ok(new { res = result });
        }
    }
}
