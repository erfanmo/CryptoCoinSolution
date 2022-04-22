using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCoinPropertyLib
{
    public class Trade
    {
        public string id { get; set; }
        public string price { get; set; }
        public string qty { get; set; }
        public string quoteQty { get; set; }
        public string time { get; set; }
        public string isBuyerMaker { get; set; }
        public string isBestMatch { get; set; }
    }
}
