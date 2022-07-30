using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using TradeWatchB.Models;
using TradeWatchB.Models.DTO;
using TradeWatchB.Services.WatchListService;

namespace TradeWatchB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WatchListController : ControllerBase
    {
        private readonly IWatchListService _watchListService;
        private readonly TradeWatchDBContext _context;
        
        public WatchListController(IWatchListService watchListService, TradeWatchDBContext context)
        {
            _watchListService = watchListService;
            _context = context;
        }
        [HttpGet]
        [Route("GetCryptoWatchList")]
        public async Task<IActionResult> GetCryptoWatchList(string curr)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    var name = claims.Where(p => p.Type == "ID").FirstOrDefault()?.Value;
                    var dt = _watchListService.Crypto(Convert.ToInt32(name), curr);
                    return Ok(new { res = dt });
                }
                return BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
            
        }
        [HttpGet]
        [Route("GetForexWatchList")]
        public async Task<IActionResult> GetForexWatchList(string curr)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    var name = claims.Where(p => p.Type == "ID").FirstOrDefault()?.Value;
                    var dt = _watchListService.Forex(Convert.ToInt32(name), curr);
                    return Ok(new { res = dt });
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        [HttpGet]
        [Route("GetStockWatchList")]
        public async Task<IActionResult> GetStockWatchList(string curr)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    var name = claims.Where(p => p.Type == "ID").FirstOrDefault()?.Value;
                    var dt = _watchListService.Stocks(Convert.ToInt32(name), curr);

                    return Ok(new { res = dt });
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        [HttpGet]
        [Route("GetCommWatchList")]
        public async Task<IActionResult> GetCommWatchList(string curr)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    var name = claims.Where(p => p.Type == "ID").FirstOrDefault()?.Value;
                    var dt = _watchListService.Comm(Convert.ToInt32(name), curr);

                    return Ok(new { res = dt });
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }
        [HttpPost]
        [Route("SetWatchList")]
        public async Task<IActionResult> SetWatchList([FromBody] WatchListDto dto)
        {
            var identity = User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                var name = claims.Where(p => p.Type == "ID").FirstOrDefault()?.Value;
                var result = _watchListService.SetFav(Convert.ToInt32(name), dto.PairId, dto.tbl, dto.like);
                return Ok(new { res = result });
            }
            return BadRequest();
        }
        [HttpPost]
        [Route("SetNotificationList")]
        public async Task<IActionResult> SetNotifyList([FromBody] WatchListNotify dto)
        {
            var identity = User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                var name = claims.Where(p => p.Type == "ID").FirstOrDefault()?.Value;
                var result = _watchListService.SetNotify(Convert.ToInt32(name), dto.PairId, dto.tbl, dto.notify,dto.Min,dto.Max);
                return Ok(new { res = result });
            }
            return BadRequest();
        }
    }
}
