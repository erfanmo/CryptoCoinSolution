using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCoinPropertyLib
{
    public class ExchangeInfo
    {
        public string timezone { get; set; }
        public string serverTime { get; set; }
        public List<RateLimits> rateLimits { get; set; }
        public string[] exchangeFilters { get; set; }
        public List<Symbol> symbols { get; set; }
    }
}
