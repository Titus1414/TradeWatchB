﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeWatchB.Models;

namespace TradeWatchB.Controllers
{
    public class StockPairController : Controller
    {
        private readonly TradeWatchDBContext _context;
        public StockPairController(TradeWatchDBContext context)
        {
            _context = context;
        }
        public IActionResult Index(int? id, string? search)
        {
            if (id == 0 || id == null)
            {
                var sds = _context.Exchanges.Where(a => a.Code == "US").FirstOrDefault();
                id = sds.Id;
            }
            object sd = null;
            if (string.IsNullOrEmpty(search))
            {
                sd = _context.Currencies.Where(a => a.ExId == id).Take(1000).ToList();
            }
            else
            {
                sd = _context.Currencies.Where(a => a.Name.Contains(search)).ToList();
            }
            
            return View(sd);
        }
        public async Task<IActionResult> ActiveExch(int Id)
        {

            var sd = _context.Currencies.Where(a => a.Id == Id).FirstOrDefault();
            if (sd.IsActive == true)
            {
                sd.IsActive = false;
            }
            else
            {
                sd.IsActive = true;
            }
            _context.Currencies.Update(sd);
            _context.SaveChanges();
            return RedirectToAction("Index", "StockPair");
        }
    }
}
