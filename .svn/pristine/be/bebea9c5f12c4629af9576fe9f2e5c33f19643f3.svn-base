using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using TradeWatchB.Models;
using TradeWatchB.Services.News;

namespace TradeWatchB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;
        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }
        [HttpPost]
        [Route("StockNews")]
        public IActionResult StockNews(List<test> dt)
        {
            var result = _newsService.stockNews(dt);
            return Ok("Success");
        }
        [HttpPost]
        [Route("ForexkNews")]
        public IActionResult ForexNews(List<test> dt)
        {
            var result = _newsService.forexNews(dt);
            return Ok("Success");
        }
        [HttpPost]
        [Route("CryptoNews")]
        public IActionResult CryptoNews(List<test> dt)
        {
            var result = _newsService.cryptoNews(dt);
            return Ok("Success");
        }
        [HttpGet]
        [Route("StockNewsGet")]
        public async Task<ActionResult> GetStkNews(string fkCode)
        {
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://news.google.com/rss/search?hl=en-PK&gl=PK&ceid=PK:en&q=" + fkCode);
            HttpContent responseContent = response.Content;
            using (var reader = new StreamReader(await responseContent.ReadAsStreamAsync()))
            {
                var sda = await reader.ReadToEndAsync();
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(sda);
                var jsonText = JsonConvert.SerializeXmlNode(doc).Replace("link", "news_url").Replace("source", "source_name").Replace("description", "text").Replace("pubDate", "date");

                dynamic myObja = JsonConvert.DeserializeObject<dynamic>(jsonText);
                return Ok(myObja);
            }
           
        }
    }
}
