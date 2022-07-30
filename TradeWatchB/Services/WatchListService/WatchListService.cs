using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TradeWatchB.Models;
using TradeWatchB.Models.DTO;

namespace TradeWatchB.Services.WatchListService
{
    public class WatchListService : IWatchListService
    {
        public readonly TradeWatchDBContext _context;
        private readonly String BASE_URI = "https://free.currconv.com/api/v6";
        private string serverKey = "AAAAn4VGTSw:APA91bE_Ze6Sh1i_NUi0Qr7vzhaXSz9ixFbGF9qiE6FzXKwb8VfrswfsE7SzZs7d62OTyxMr9Y6EsVIjCR6C22YieUh1EqRxaq4WRmxahlYPAx0ip-uNbdLQcUYshwLljkUR60Q8VSzV";
        private string senderId = "685135777068";
        private string webAddr = "https://fcm.googleapis.com/fcm/send";
        public WatchListService(TradeWatchDBContext context)
        {
            _context = context;
        }
        public async Task<List<WatchListTrenDto>> Crypto(int id, string curr)
        {
            try
            {
                decimal vl = GetCurrencyExchange("USD", curr);
                var dt = from t1 in _context.CcrealTimePairs
                         join t2 in _context.WatchLists on t1.Id equals t2.Ccid into g
                         from t2 in g.DefaultIfEmpty()
                         join t3 in _context.Notifications on t1.Id equals t3.Ccid into b
                         from t3 in b.DefaultIfEmpty()
                         where t2.Uid == id || t2.Uid == null
                         orderby t2.Uid descending
                         select new { t1.Change, t1.Close, t1.Code, t1.Gmtoffset, t1.High, t1.Id, t1.Low, t1.Open, t1.PreviousClose, t1.Timestamp, t1.Volume, LikeValue = t2.IsLike.ToString() ?? "", Notify = t3.Notify.ToString() ?? "" };
                dt.ToList();

                List<WatchListTrenDto> lst = new List<WatchListTrenDto>();
                foreach (var item in dt)
                {
                    WatchListTrenDto sd = new WatchListTrenDto();
                    sd.Change = item.Change * (float)vl;
                    sd.Close = item.Close * (float)vl;
                    sd.Code = item.Code;
                    sd.Gmtoffset = item.Gmtoffset;
                    sd.High = item.High * (float)vl;
                    sd.Id = item.Id;
                    sd.LikeValue = item.LikeValue;
                    sd.Low = item.Low * (float)vl;
                    sd.Open = item.Open * (float)vl;
                    sd.PreviousClose = item.PreviousClose * (float)vl;
                    sd.Timestamp = item.Timestamp;
                    sd.Volume = item.Volume * (float)vl;
                    sd.Notify = item.Notify;
                    lst.Add(sd);
                }
                return lst;
            }
            catch (Exception ex)
            {
            }
            return null;
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
        public async Task<List<WatchListTrenDto>> Comm(int id, string curr)
        {
            try
            {
                decimal vl = GetCurrencyExchange("USD", curr);
                var dt = from t1 in _context.CommRealTimePairs
                         join t2 in _context.WatchLists on t1.Id equals t2.CommId into g
                         from t2 in g.DefaultIfEmpty()
                         join t3 in _context.Notifications on t1.Id equals t3.CommId into b
                         from t3 in b.DefaultIfEmpty()
                         where t2.Uid == id || t2.Uid == null
                         orderby t2.Uid descending
                         select new { t1.Change, t1.Close, t1.Code, t1.Gmtoffset, t1.High, t1.Id, t1.Low, t1.Open, t1.PreviousClose, t1.Timestamp, t1.Volume, LikeValue = t2.IsLike.ToString() ?? "", Notify = t3.Notify.ToString() ?? "" };
                dt.ToList();

                List<WatchListTrenDto> lst = new List<WatchListTrenDto>();
                foreach (var item in dt)
                {
                    WatchListTrenDto sd = new WatchListTrenDto();
                    sd.Change = item.Change * (float)vl;
                    sd.Close = item.Close * (float)vl;
                    sd.Code = item.Code;
                    sd.Gmtoffset = item.Gmtoffset;
                    sd.High = item.High * (float)vl;
                    sd.Id = item.Id;
                    sd.LikeValue = item.LikeValue;
                    sd.Low = item.Low * (float)vl;
                    sd.Open = item.Open * (float)vl;
                    sd.PreviousClose = item.PreviousClose * (float)vl;
                    sd.Timestamp = item.Timestamp;
                    sd.Volume = item.Volume * (float)vl;
                    sd.Notify = item.Notify;
                    lst.Add(sd);
                }
                return lst;
            }
            catch (Exception ex)
            {
            }
            return null;
        }
        public async Task<List<WatchListTrenDto>> Forex(int id, string curr)
        {

            try
            {
                decimal vl = GetCurrencyExchange("USD", curr);
                var dt = from t1 in _context.ForexRealTimePairs
                         join t2 in _context.WatchLists on t1.Id equals t2.Forexid into g
                         from t2 in g.DefaultIfEmpty()
                         join t3 in _context.Notifications on t1.Id equals t3.Forexid into b
                         from t3 in b.DefaultIfEmpty()
                         where t2.Uid == id || t2.Uid == null
                         orderby t2.Uid descending
                         select new { t1.Change, t1.Close, t1.Code, t1.Gmtoffset, t1.High, t1.Id, t1.Low, t1.Open, t1.PreviousClose, t1.Timestamp, t1.Volume, LikeValue = t2.IsLike.ToString() ?? "", Notify = t3.Notify.ToString() ?? "" };
                dt.ToList();

                List<WatchListTrenDto> lst = new List<WatchListTrenDto>();
                foreach (var item in dt)
                {
                    WatchListTrenDto sd = new WatchListTrenDto();
                    sd.Change = item.Change * (float)vl;
                    sd.Close = item.Close * (float)vl;
                    sd.Code = item.Code;
                    sd.Gmtoffset = item.Gmtoffset;
                    sd.High = item.High * (float)vl;
                    sd.Id = item.Id;
                    sd.LikeValue = item.LikeValue;
                    sd.Low = item.Low * (float)vl;
                    sd.Open = item.Open * (float)vl;
                    sd.PreviousClose = item.PreviousClose * (float)vl;
                    sd.Timestamp = item.Timestamp;
                    sd.Volume = item.Volume * (float)vl;
                    sd.Notify = item.Notify;
                    lst.Add(sd);
                }
                return lst;
            }
            catch (Exception ex)
            {
            }
            return null;
        }
        public async Task<string> SetFav(int uid, int PairId, string tbl, bool like)
        {
            try
            {
                if (like == false)
                {
                    if (tbl == "crypto")
                    {
                        var dt = _context.WatchLists.Where(a => a.Uid == uid && a.Ccid == PairId).FirstOrDefault();
                        _context.WatchLists.Remove(dt);
                        var ds = _context.Notifications.Where(a => a.Uid == uid && a.Ccid == PairId).FirstOrDefault();
                        _context.Notifications.Remove(ds);
                    }
                    else if (tbl == "forex")
                    {
                        var dt = _context.WatchLists.Where(a => a.Uid == uid && a.Forexid == PairId).FirstOrDefault();
                        _context.WatchLists.Remove(dt);
                        var ds = _context.Notifications.Where(a => a.Uid == uid && a.Forexid == PairId).FirstOrDefault();
                        _context.Notifications.Remove(ds);
                    }
                    else if (tbl == "stock")
                    {
                        var dt = _context.WatchLists.Where(a => a.Uid == uid && a.Stkid == PairId).FirstOrDefault();
                        _context.WatchLists.Remove(dt);
                        var ds = _context.Notifications.Where(a => a.Uid == uid && a.Stkid == PairId).FirstOrDefault();
                        _context.Notifications.Remove(ds);
                    }
                    else if (tbl == "comm")
                    {
                        var dt = _context.WatchLists.Where(a => a.Uid == uid && a.CommId == PairId).FirstOrDefault();
                        _context.WatchLists.Remove(dt);
                        var ds = _context.Notifications.Where(a => a.Uid == uid && a.CommId == PairId).FirstOrDefault();
                        _context.Notifications.Remove(ds);
                    }
                    _context.SaveChanges();
                    return "Success";
                }
                else
                {
                    WatchList watchList = new WatchList();
                    Notification notification = new Notification();
                    watchList.Uid = uid;
                    notification.Uid = uid;
                    if (tbl == "crypto")
                    {
                        watchList.Ccid = PairId;
                        notification.Ccid = PairId;
                    }
                    else if (tbl == "forex")
                    {
                        watchList.Forexid = PairId;
                        notification.Forexid = PairId;
                    }
                    else if (tbl == "stock")
                    {
                        watchList.Stkid = PairId;
                        notification.Stkid = PairId;
                    }
                    else if (tbl == "comm")
                    {
                        watchList.CommId = PairId;
                        notification.CommId = PairId;
                    }
                    watchList.IsLike = like;
                    notification.Notify = false;
                    _context.Notifications.Add(notification);
                    _context.WatchLists.Add(watchList);
                    _context.SaveChanges();

                }
                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public async Task<List<WatchListTrenDto>> Stocks(int id, string curr)
        {
            try
            {
                decimal vl = GetCurrencyExchange("USD", curr);
                var dt = from t1 in _context.NserealTimePairs
                         join t2 in _context.WatchLists on t1.Id equals t2.Stkid into g
                         from t2 in g.DefaultIfEmpty()
                         join t3 in _context.Notifications on t1.Id equals t3.Stkid into b
                         from t3 in b.DefaultIfEmpty()
                         where t2.Uid == id || t2.Uid == null
                         orderby t2.Uid descending
                         select new { t1.Change, t1.Close, t1.Code, t1.Gmtoffset, t1.High, t1.Id, t1.Low, t1.Open, t1.PreviousClose, t1.Timestamp, t1.Volume, LikeValue = t2.IsLike.ToString() ?? "", Notify = t3.Notify.ToString() ?? "" };
                dt.ToList();

                List<WatchListTrenDto> lst = new List<WatchListTrenDto>();
                foreach (var item in dt)
                {
                    WatchListTrenDto sd = new WatchListTrenDto();
                    sd.Change = item.Change * (float)vl;
                    sd.Close = item.Close * (float)vl;
                    sd.Code = item.Code;
                    sd.Gmtoffset = item.Gmtoffset;
                    sd.High = item.High * (float)vl;
                    sd.Id = item.Id;
                    sd.LikeValue = item.LikeValue;
                    sd.Low = item.Low * (float)vl;
                    sd.Open = item.Open * (float)vl;
                    sd.PreviousClose = item.PreviousClose * (float)vl;
                    sd.Timestamp = item.Timestamp;
                    sd.Volume = item.Volume * (float)vl;
                    sd.Notify = item.Notify;
                    lst.Add(sd);
                }
                return lst;
            }
            catch (Exception ex)
            {
            }
            return null;
        }
        public async Task<string> SetNotify(int uid, int PairId, string tbl, bool notify, float min, float max)
        {
            try
            {
                if (notify == false)
                {
                    if (tbl == "crypto")
                    {
                        var dt = _context.Notifications.Where(a => a.Uid == uid && a.Ccid == PairId).FirstOrDefault();
                        _context.Notifications.Remove(dt);
                    }
                    else if (tbl == "forex")
                    {
                        var dt = _context.Notifications.Where(a => a.Uid == uid && a.Forexid == PairId).FirstOrDefault();
                        _context.Notifications.Remove(dt);
                    }
                    else if (tbl == "stock")
                    {
                        var dt = _context.Notifications.Where(a => a.Uid == uid && a.Stkid == PairId).FirstOrDefault();
                        _context.Notifications.Remove(dt);
                    }
                    else if (tbl == "comm")
                    {
                        var dt = _context.Notifications.Where(a => a.Uid == uid && a.CommId == PairId).FirstOrDefault();
                        _context.Notifications.Remove(dt);
                    }
                    _context.SaveChanges();
                    return "Success";
                }
                else
                {
                    Notification watchList = new Notification();
                    watchList.Uid = uid;
                    if (tbl == "crypto")
                    {
                        watchList.Ccid = PairId;
                    }
                    else if (tbl == "forex")
                    {
                        watchList.Forexid = PairId;
                    }
                    else if (tbl == "stock")
                    {
                        watchList.Stkid = PairId;
                    }
                    else if (tbl == "comm")
                    {
                        watchList.CommId = PairId;
                    }
                    watchList.Notify = notify;
                    watchList.MinVal = min;
                    watchList.MaxVal = max;
                    _context.Notifications.Add(watchList);
                    _context.SaveChanges();
                }
                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public async Task<string> SendNotification(string DeviceToken, string title, string msg)
        {
            var result = "-1";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Headers.Add(string.Format("Authorization: key={0}", serverKey));
            httpWebRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
            httpWebRequest.Method = "POST";

            var payload = new
            {
                to = DeviceToken,
                priority = "high",
                content_available = true,
                notification = new
                {
                    body = msg,
                    title = title,
                }
                //,
                //data = new
                //{
                //    tid = tid
                //}
            };

            await using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(payload);
                streamWriter.Write(json);
                streamWriter.Flush();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }
            return result;
        }
        public async Task<string> SendFavNotify()
        {
            var watchLst = await _context.WatchLists.ToListAsync();

            if (watchLst.Count > 0)
            {
                foreach (var item in watchLst)
                {
                    var dt = _context.Notifications.Where(a => a.Uid == item.Id && a.Notify == false).ToList();
                    if (item.Ccid != null)
                    {
                        foreach (var dtitem in dt)
                        {
                            var dtCC = _context.CcrealTimePairs.Where(a => a.Id == dtitem.Ccid).FirstOrDefault();
                            if (dtCC.Close < dtCC.PreviousClose)
                            {
                                var res = SendNotification("/topics/" + item.Id.ToString(), "Change in " + dtCC.Code, "The Value of " + dtCC.Code + " has been Decreased to" + dtCC.Close);
                            }
                            else if (dtCC.Close > dtCC.PreviousClose)
                            {
                                var res = SendNotification("/topics/" + item.Id.ToString(), "Change in " + dtCC.Code, "The Value of " + dtCC.Code + " has been Increased to" + dtCC.Close);
                            }
                        }
                    }
                    else if (item.Forexid != null)
                    {
                        foreach (var dtitem in dt)
                        {
                            var dtCC = _context.ForexRealTimePairs.Where(a => a.Id == dtitem.Ccid).FirstOrDefault();
                            if (dtCC.Close < dtCC.PreviousClose)
                            {
                                var res = SendNotification("/topics/" + item.Id.ToString(), "Change in " + dtCC.Code, "The Value of " + dtCC.Code + " has been Decreased to" + dtCC.Close);
                            }
                            else if (dtCC.Close > dtCC.PreviousClose)
                            {
                                var res = SendNotification("/topics/" + item.Id.ToString(), "Change in " + dtCC.Code, "The Value of " + dtCC.Code + " has been Increased to" + dtCC.Close);
                            }
                        }
                    }
                    else if (item.CommId != null)
                    {
                        foreach (var dtitem in dt)
                        {
                            var dtCC = _context.CommRealTimePairs.Where(a => a.Id == dtitem.Ccid).FirstOrDefault();
                            if (dtCC.Close < dtCC.PreviousClose)
                            {
                                var res = SendNotification("/topics/" + item.Id.ToString(), "Change in " + dtCC.Code, "The Value of " + dtCC.Code + " has been Decreased to" + dtCC.Close);
                            }
                            else if (dtCC.Close > dtCC.PreviousClose)
                            {
                                var res = SendNotification("/topics/" + item.Id.ToString(), "Change in " + dtCC.Code, "The Value of " + dtCC.Code + " has been Increased to" + dtCC.Close);
                            }
                        }
                    }
                    else if (item.Stkid != null)
                    {
                        foreach (var dtitem in dt)
                        {
                            var dtCC = _context.NserealTimePairs.Where(a => a.Id == dtitem.Ccid).FirstOrDefault();
                            if (dtCC.Close < dtCC.PreviousClose)
                            {
                                var res = SendNotification("/topics/" + item.Id.ToString(), "Change in " + dtCC.Code, "The Value of " + dtCC.Code + " has been Decreased to" + dtCC.Close);
                            }
                            else if (dtCC.Close > dtCC.PreviousClose)
                            {
                                var res = SendNotification("/topics/" + item.Id.ToString(), "Change in " + dtCC.Code, "The Value of " + dtCC.Code + " has been Increased to" + dtCC.Close);
                            }
                        }
                    }
                }
            }
            return "Success";
        }
        public async Task<string> SendNotify()
        {
            //Max and Min Val Notify
            var users = await _context.Logins.Where(a => a.IsActive == true).ToListAsync();
            if (users.Count > 0)
            {
                foreach (var item in users)
                {
                    var dt = _context.Notifications.Where(a => a.Uid == item.Id).ToList();
                    if (dt.Count > 0)
                    {
                        foreach (var dtitem in dt)
                        {
                            if (dtitem.Ccid != null)
                            {
                                var dtCC = _context.CcrealTimePairs.Where(a => a.Id == dtitem.Ccid).FirstOrDefault();
                                if (dtCC.Close <= dtitem.MinVal)
                                {
                                    var res = SendNotification("/topics/" + item.Id.ToString(), "Change in " + dtCC.Code, "The Value of " + dtCC.Code + " has been Decreased to " + dtCC.Close);
                                }
                                else if (dtCC.Close >= dtitem.MaxVal)
                                {
                                    var res = SendNotification("/topics/" + item.Id.ToString(), "Change in " + dtCC.Code, "The Value of " + dtCC.Code + "has been Increased to " + dtCC.Close);
                                }
                            }
                            else if (dtitem.CommId != null)
                            {
                                var dtCC = _context.CommRealTimePairs.Where(a => a.Id == dtitem.CommId).FirstOrDefault();
                                if (dtCC.Close <= dtitem.MinVal)
                                {
                                    var res = SendNotification("/topics/" + item.Id.ToString(), "Change in " + dtCC.Code, "The Value of " + dtCC.Code + "has been Decreased to " + dtCC.Close);
                                }
                                else if (dtCC.Close >= dtitem.MaxVal)
                                {
                                    var res = SendNotification("/topics/" + item.Id.ToString(), "Change in " + dtCC.Code, "The Value of " + dtCC.Code + "has been Increased to " + dtCC.Close);
                                }
                            }
                            else if (dtitem.Stkid != null)
                            {
                                var dtCC = _context.NserealTimePairs.Where(a => a.Id == dtitem.Stkid).FirstOrDefault();
                                if (dtCC.Close <= dtitem.MinVal)
                                {
                                    var res = SendNotification("/topics/" + item.Id.ToString(), "Change in " + dtCC.Code, "The Value of " + dtCC.Code + "has been Decreased to " + dtCC.Close);
                                }
                                else if (dtCC.Close >= dtitem.MaxVal)
                                {
                                    var res = SendNotification("/topics/" + item.Id.ToString(), "Change in " + dtCC.Code, "The Value of " + dtCC.Code + "has been Increased to" + dtCC.Close);
                                }
                            }
                            else if (dtitem.Forexid != null)
                            {
                                var dtCC = _context.ForexRealTimePairs.Where(a => a.Id == dtitem.Forexid).FirstOrDefault();
                                if (dtCC.Close <= dtitem.MinVal || dtCC.Close >= dtitem.MaxVal)
                                {
                                    var res = SendNotification("/topics/" + item.Id.ToString(), "Change in " + dtCC.Code, "The Value of " + dtCC.Code + "has been Decreased to " + dtCC.Close);
                                }
                                else if (true)
                                {
                                    var res = SendNotification("/topics/" + item.Id.ToString(), "Change in " + dtCC.Code, "The Value of " + dtCC.Code + "has been Increased to " + dtCC.Close);
                                }
                            }
                        }
                    }
                }
            }
            //WatchList Notify
            
            
            return "Success";
        }
    }
}
