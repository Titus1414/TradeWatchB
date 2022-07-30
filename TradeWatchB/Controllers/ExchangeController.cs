using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeWatchB.Models;

namespace TradeWatchB.Controllers
{
    public class ExchangeController : Controller
    {
        private readonly TradeWatchDBContext _context;
        public ExchangeController(TradeWatchDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var res = _context.Exchanges.ToList();
            return View(res);
        }
        public async Task<IActionResult> ActiveExch(int Id)
        {

            var sd = _context.Exchanges.Where(a => a.Id == Id).FirstOrDefault();
            if (sd.IsActive == true)
            {
                sd.IsActive = false;
            }
            else
            {
                sd.IsActive = true;
            }
            _context.Exchanges.Update(sd);
            _context.SaveChanges();
            return RedirectToAction("Index","Exchange");
        }
    }
}
