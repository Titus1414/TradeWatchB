using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradeWatchB.NotificationHub
{
    public class SignalServer: Hub
    {
        private readonly DbContext _context;
        private static ConcurrentDictionary<string, string> _dictionary = new ConcurrentDictionary<string, string>();


    }
}
