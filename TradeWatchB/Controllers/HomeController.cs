﻿using EODHistoricalData.NET;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TradeWatchB.Models;
using Nancy.Json;
using Newtonsoft.Json.Linq;
using TradeWatchB.Services.EodService;
using TradeWatchB.Services.AuthService;
using TradeWatchB.Models.DTO;
using System.Net.Http;
using System.IO;
using System.Xml;
using Newtonsoft.Json;
using Google.Apis.YouTube.v3;
using Google.Apis.Services;
using TradeWatchB.Services.WatchListService;

namespace TradeWatchB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TradeWatchDBContext _context;
        private readonly IEodService _eodService;
        private readonly IAuth _AuthService;
        private readonly IWatchListService _watchListService;

        private readonly String BASE_URI = "https://free.currconv.com/api/v6";
        public HomeController(ILogger<HomeController> logger, TradeWatchDBContext context, IEodService eodService, IAuth AuthService, IWatchListService watchListService)
        {
            _logger = logger;
            _context = context;
            _eodService = eodService;
            _AuthService = AuthService;
            _watchListService = watchListService;
        }
        public Decimal GetCurrencyExchange(String localCurrency, String foreignCurrency)
        {
            var code = $"{localCurrency}_{foreignCurrency}";
            var newRate = FetchSerializedData(code);
            return newRate;
        }
        private Decimal FetchSerializedData(String code)
        {
            var key = "17af86db3713331c737f";
            var url = $"{BASE_URI}/convert?compact=ultra?&q={code}&apiKey={key}";
            var webClient = new WebClient();
            var jsonData = String.Empty;

            var conversionRate = 1.0m;
            try
            {
                jsonData = webClient.DownloadString(url);
                dynamic data = JObject.Parse(jsonData);
                decimal se = data["results"][code]["val"];
                conversionRate = se;
            }
            catch (Exception) { }

            return conversionRate;
        }
        public IActionResult Login()
        {
            GetCurrencyExchange("CUP", "USD");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string Name, string Pass)
        {
            var dt = _context.Logins.Where(a => a.Name == Name && a.Password == Pass && a.IsActive == false).FirstOrDefault();
            if (dt != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public IActionResult PasswordRest(int id, string email)
        {
            var dt = _context.Logins.Where(a => a.Id == id && a.Email == email).FirstOrDefault();
            if (dt != null)
            {
                ViewBag.Email = dt.Email;
            }

            return View();
        }
        [HttpPost]
        public IActionResult PasswordRest(passResetDto dto)
        {
            try
            {
                if (dto.pass == dto.cpass)
                {
                    var sds = _AuthService.SetPass(dto);
                    if (sds.Result.ToString() == "Success")
                    {
                        return RedirectToAction("Success", "Home");
                    }
                }
                else
                {
                    return RedirectToAction("PasswordRest", "Home");
                }
            }
            catch (Exception)
            {

                throw;
            }

            return View();
        }
        public IActionResult Success()
        {
            return View();
        }
        private static IEnumerable<JToken> AllChildren(JToken json)
        {
            foreach (var c in json.Children())
            {
                yield return c;
                foreach (var cc in AllChildren(c))
                {
                    yield return cc;
                }
            }
        }
        public IActionResult Index()
        {
            //_watchListService.SendNotify();
            //string baseURL = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            //var s = "sdds sdsv s ".Replace(" ","");

            //var sd = DateTime.Now.ToString("ddd, dd MMM yyyy HH:mm:ss zzz");

            //EODHistoricalDataClient client = new EODHistoricalDataClient("60d210c432ea20.32945490", true);
            //EODHistoricalDataClient client = new EODHistoricalDataClient(ApiKeys.EodApiKey, true);
            //List<HistoricalPrice> prices = client.GetHistoricalPrices("IGPL.NSE", DateTime.Now.AddYears(-1), DateTime.Now);
            //IList<Dividend> dividends = client.GetDividends("IGPL.NSE", DateTime.Now.AddYears(-1), DateTime.Now).ToArray();

            //EODHistoricalDataClient client = new EODHistoricalDataClient(ApiKeys.EodApiKey, true);
            //FundamentalStock fundamental = client.GetFundamentalStock("AAPL.US");

            ///string[] sdl = { "AAPL.US", "BTC-USD.CC" };

            ///var list = _context.Currencies.Where(a => a.Id >= 1 && a.Id <= 5).Select(s => s.Code + '.' + s.FkCode).ToList();

            ///string[] jlst0 = list.ToArray();



            ///var ss = jlst0;
            ///

            //var lst = _context.Exchanges.Where(a => a.Currency != "Unknown" && a.Id > 45).ToList();
            //foreach (var item in lst)
            //{
            //    List<Instrument> prices = client.GetExchangeInstruments(item.Code);
            //    foreach (var prs in prices)
            //    {
            //        Currency ss = new Currency();
            //        ss.ExId = item.Id;
            //        ss.Code = prs.Code;
            //        ss.Name = prs.Name;
            //        _context.Currencies.Add(ss);
            //    }
            //    _context.SaveChanges();
            //}

            ///RealTimePrice prices3 = client.GetRealTimePrice("kar.AU");
            //Throw error because of unpaid api
            //List<Instrument> instruments = client.GetExchangeInstruments("CC");
            //foreach (var item in instruments.Where(a => a.Code == "0xBTC-USD"))
            //{
            //    var sd = item.Code;
            //}

            //FundamentalStock fundamental = client.GetFundamentalStock(new[] { "0xBTC-USD", "$ANRX-USD.CC" });
            ///IList<FundamentalStock> fundamental = client.GetFundamentalStock(new[] { "JPY.FOREX", "0xBTC-USD.CC", "$ANRX-USD.CC" });


            //var newsApiClient = new NewsApiClient(ApiKeys.NewsApiKey);
            //var articlesResponse = newsApiClient.GetEverything(new EverythingRequest
            //{
            //    Q = "Apple",
            //    SortBy = SortBys.Popularity,
            //    Language = Languages.EN,
            //    From = new DateTime(2021, 6, 26)
            //});
            //if (articlesResponse.Status == Statuses.Ok)
            //{
            //    // total results found
            //    Console.WriteLine(articlesResponse.TotalResults);
            //    // here's the first 20
            //    foreach (var article in articlesResponse.Articles)
            //    {
            //        // title
            //        var sd = article.Title;
            //        // author
            //        var se = article.Author;
            //        // description
            //        var vsew = article.Description;
            //        // url
            //        var sts = article.Url;
            //        // published at
            //        var see = article.PublishedAt;
            //    }
            //}

            return View();
        }
        public IActionResult Check()
        {
            _eodService.GetRealTimePairs();

            return View("Index");
        }
        public IActionResult Privacy([FromBody] List<test> test)
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddCurrency(Currency dto)
        {

            try
            {
                if (dto.Id > 0)
                {

                }
                else
                {
                    Currency sd = new Currency();
                    sd.Name = dto.Name;
                    sd.ExId = dto.ExId;
                    sd.Code = dto.Code;
                    sd.FkCode = dto.FkCode;
                    _context.Currencies.Add(sd);
                    _context.SaveChanges();
                }

            }
            catch (Exception)
            {

                throw;
            }

            return RedirectToAction("Index", "Home");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
