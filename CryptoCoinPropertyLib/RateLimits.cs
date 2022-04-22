using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCoinPropertyLib
{
    public class RateLimits
    {
        public string rateLimitType { get; set; }
        public string interval { get; set; }
        public string intervalNum { get; set; }
        public string limit { get; set; }
    }
}
