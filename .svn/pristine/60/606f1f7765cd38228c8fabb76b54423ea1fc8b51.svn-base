using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeWatchB.Models;

namespace TradeWatchB.Controllers
{
    public class PairController : Controller
    {
        private readonly TradeWatchDBContext _context;
        public PairController(TradeWatchDBContext context)
        {
            _context = context;
        }
        public IActionResult Index(int? id)
        {
            if (id == 0 || id == null)
            {
                var sds = _context.Exchanges.Where(a => a.Code == "CC").FirstOrDefault();
                id = sds.Id;
            }
            var sd = _context.Currencies.Where(a => a.ExId == id).ToList();
            return View(sd);
        }
        public async Task<IActionResult> ActiveExch(int Id)
        {
            var sd = _context.Currencies.Where(a => a.Id == Id).FirstOrDefault();
            var dt = _context.Exchanges.Where(a => a.Name == sd.Name && a.Currency != "Unknown").FirstOrDefault();
            if (sd != null)
            {
                if (sd.IsActive == true)
                {
                    sd.IsActive = false;
                }
                else
                {
                    sd.IsActive = true;
                }
            }
            if (string.IsNullOrEmpty(sd.FkCode))
            {
                if (dt != null)
                {
                    if (dt.IsActive == true)
                    {
                        dt.IsActive = false;
                    }
                    else
                    {
                        dt.IsActive = true;
                    }
                    _context.Exchanges.Update(dt);
                }
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Pair");
        }
    }
}
