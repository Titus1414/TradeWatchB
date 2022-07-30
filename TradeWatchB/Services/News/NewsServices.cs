
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using TradeWatchB.Models;

namespace TradeWatchB.Services.News
{
    public class NewsServices : INewsService
    {
        public readonly TradeWatchDBContext _context;
        public static dynamic myObj = "";
        public static List<string> lisStk = new List<string>(new string[] { });
        public NewsServices(TradeWatchDBContext context)
        {
            _context = context;

        }
        public Task<string> cryptoNews(List<test> dt)
        {
            try
            {
                foreach (var item in dt)
                {
                    var sd = _context.CryptoNews.Where(a => a.Date == item.date).FirstOrDefault();
                    if (sd == null)
                    {
                        CryptoNews stockNews = new CryptoNews();
                        stockNews.Date = Convert.ToDateTime(item.date).Date.ToString();
                        stockNews.ImageUrl = item.image_url;
                        stockNews.Sentiment = item.sentiment;
                        stockNews.NewsUrl = item.news_url;
                        stockNews.SourceName = item.source_name;
                        stockNews.Text = item.text;
                        stockNews.Tickers = String.Join(",", item.tickers.ToList());
                        stockNews.Title = item.title;
                        stockNews.Topics = String.Join(",", item.topics.ToList());
                        stockNews.Type = item.type;
                        _context.CryptoNews.Add(stockNews);
                    }
                }
                _context.SaveChanges();

            }
            catch (Exception)
            {

                throw;
            }
            return null;
        }
        public Task<string> forexNews(List<test> dt)
        {
            try
            {
                foreach (var item in dt)
                {
                    var sd = _context.ForexNews.Where(a => a.Date == item.date).FirstOrDefault();
                    if (sd == null)
                    {
                        ForexNews stockNews = new ForexNews();
                        stockNews.Date = Convert.ToDateTime(item.date).Date.ToString();
                        stockNews.ImageUrl = item.image_url;
                        stockNews.Sentiment = item.sentiment;
                        stockNews.NewsUrl = item.news_url;
                        stockNews.SourceName = item.source_name;
                        stockNews.Text = item.text;
                        stockNews.Tickers = String.Join(",", item.tickers.ToList());
                        stockNews.Title = item.title;
                        stockNews.Topics = String.Join(",", item.topics.ToList());
                        stockNews.Type = item.type;
                        _context.ForexNews.Add(stockNews);
                    }
                }
                _context.SaveChanges();

            }
            catch (Exception)
            {

                throw;
            }
            return null;
        }
        public async Task<List<StockNews>> GetStockNews()
        {

            var sd = DateTime.Now.ToString("ddd, dd MMM yyyy");
            var es = DateTime.Now.ToString("M/dd/yyyy");
            var dt = _context.StockNews.Where(a => a.Date.Contains(sd) || a.Date.Contains(es)).ToList();

            return dt;
        }
        async Task<dynamic> INewsService.NewApi(string q)
        {
            
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://news.google.com/rss/search?hl=en-PK&gl=PK&ceid=PK:en&q=" + q);
            HttpContent responseContent = response.Content;
            using (var reader = new StreamReader(await responseContent.ReadAsStreamAsync()))
            {
                var sd = await reader.ReadToEndAsync();
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(sd);
                var jsonText = JsonConvert.SerializeXmlNode(doc).Replace("link", "news_url").Replace("source", "source_name").Replace("description","text").Replace("pubDate","date");
                
                dynamic myObja = JsonConvert.DeserializeObject<dynamic>(jsonText);
                return await myObja;
            }
        }
        public async Task<List<StockNews>> googleNews(string q)
        {

            try
            {
                var dts = _context.StockNews.Take(25).OrderByDescending(a => a.Id).ToList();
                return dts;
            }
            catch (Exception )
            {
                throw;
            }
        }
        public Task<string> stockNews(List<test> dt)
        {
            try
            {
                foreach (var item in dt)
                {
                    var sd = _context.StockNews.Where(a => a.Date == item.date).FirstOrDefault();
                    if (sd == null)
                    {
                    }
                }
                _context.SaveChanges();

            }
            catch (Exception)
            {

                throw;
            }
            return null;
        }

        
    }
}
