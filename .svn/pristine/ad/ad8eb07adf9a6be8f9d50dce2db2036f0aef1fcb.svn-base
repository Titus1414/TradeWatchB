using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeWatchB.Models;

namespace TradeWatchB.Services.News
{
    public interface INewsService
    {
        Task<string> stockNews(List<test> dt);
        Task<string> cryptoNews(List<test> dt);
        Task<string> forexNews(List<test> dt);
        Task<List<StockNews>> googleNews(string q);
        Task<List<StockNews>> GetStockNews();

        Task<dynamic> NewApi(string q);
    }
}
