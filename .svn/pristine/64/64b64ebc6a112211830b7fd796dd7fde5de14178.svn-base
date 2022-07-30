using EODHistoricalData.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeWatchB.Models;
using TradeWatchB.Models.DTO;

namespace TradeWatchB.Services.EodService
{
    public interface IEodService
    {
        Task<List<Exchange>> GetExchanges();
        Task<List<Currency>> GetCodes();
        Task<string> GetRealTimePairs();
        Task<Tuple<List<HistoricalPrice>, List<DividendsDto>, float>> GetHistoricalPairs(string TimePeriod,int StkId, int PairId, string curr);
        Task<List<NserealTimePair>> NseRealTimePair();
        Task<List<CcrealTimePair>> CcRealTimePair();
        Task<List<ForexRealTimePair>> ForexRealTimePair();
        Task<List<CommRealTimePair>> CommRealTimePair();
        Task<List<AllCurrency>> AllCurrencies();
        Task<List<Currency>> GetStockPairs(int id);
        Task<int> GetId(int id);
    }
}
