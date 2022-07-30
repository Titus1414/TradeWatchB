using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeWatchB.Models;
using TradeWatchB.Models.DTO;

namespace TradeWatchB.Services.WatchListService
{
    public interface IWatchListService
    {
        Task<List<WatchListTrenDto>> Crypto(int id, string curr);
        Task<List<WatchListTrenDto>> Comm(int id, string curr);
        Task<List<WatchListTrenDto>> Forex(int id, string curr);
        Task<List<WatchListTrenDto>> Stocks(int id, string curr);
        Task<string> SetFav(int uid, int PairId, string tbl, bool like);
        Task<string> SetNotify(int uid, int PairId, string tbl,bool notify, float min, float max);
        Task<string> SendNotify();
        Task<string> SendFavNotify();
    }
}
