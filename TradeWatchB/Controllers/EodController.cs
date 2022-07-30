﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeWatchB.Models;
using TradeWatchB.Models.DTO;
using TradeWatchB.Services.EodService;

namespace TradeWatchB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EodController : ControllerBase
    {
        private readonly IEodService _eodService;
        public readonly TradeWatchDBContext _context;
        public EodController(IEodService eodService, TradeWatchDBContext context)
        {
            _eodService = eodService;
            _context = context;
        }

        [HttpGet]
        [Route("GetExchanges")]
        public async Task<IActionResult> GetExchange()
        {
            try
            {
                var result = await _eodService.GetExchanges();
                return Ok(new { res = result });
            }
            catch (Exception )
            {

                throw;
            }
        }
        [HttpGet]
        [Route("GetCodes")]
        public async Task<IActionResult> GetCode()
        {
            try
            {
                var result = await _eodService.GetCodes();
                return Ok(new { res = result });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("LoadRealTimeData")]
        public async Task<IActionResult> LoadRealTime()
        {
            try
            {
                var result = await _eodService.GetRealTimePairs();
                return Ok(new { res = result });
            }
            catch (Exception )
            {

                throw;
            }
        }
        [HttpGet]
        [Route("CCGetRealTimeData")]
        public async Task<IActionResult> GetRealTime()
        {
            try
            {
                var result = await _eodService.CcRealTimePair();
                return Ok(new { res = result });
            }
            catch (Exception )
            {

                throw;
            }
        }
        [HttpGet]
        [Route("NSEGetRealTimeData")]
        public async Task<IActionResult> NSEGetRealTime()
        {
            try
            {
                var result = await _eodService.NseRealTimePair();
                return Ok(new { res = result });
            }
            catch (Exception )
            {

                throw;
            }
        }
        [HttpGet]
        [Route("ForexGetRealTimeData")]
        public async Task<IActionResult> ForexGetRealTime()
        {
            try
            {
                var result = await _eodService.ForexRealTimePair();
                return Ok(new { res = result });
            }
            catch (Exception )
            {

                throw;
            }
        }
        [HttpGet]
        [Route("CommGetRealTimeData")]
        public async Task<IActionResult> CommGetRealTime()
        {
            try
            {
                var result = await _eodService.CommRealTimePair();
                return Ok(new { res = result });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("GetHistorialData")]
        public async Task<IActionResult> GetHistoricalData([FromBody] HistoricalDataDTO dto)
        {
            try
            {
                var result = _eodService.GetHistoricalPairs(dto.TimePeriod, dto.StkId, dto.PairId, dto.curr);
                if (result.Result.ToString() == "Not yet computed")
                {
                    return null; 
                }

                return Ok(new { res = result });
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        [Route("AllCurrencies")]
        public async Task<IActionResult> AllCurr()
        {
            try
            {
                var result = await _eodService.AllCurrencies();
                return Ok(new { res = result });
            }
            catch (Exception )
            {

                throw;
            }
        }
        [HttpPost]
        [Route("GetStockPairs")]
        public async Task<IActionResult> StockPairs(int id)
        {
            try
            {
                var result = await _eodService.GetStockPairs(id);
                return Ok(new { res = result });
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
