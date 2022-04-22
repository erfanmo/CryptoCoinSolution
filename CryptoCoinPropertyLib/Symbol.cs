using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCoinPropertyLib
{
    public class Symbol
    {
        public string symbol { get; set; }
        public string status { get; set; }
        public string baseAsset { get; set; }
        public string baseAssetPrecision { get; set; }
        public string quoteAsset { get; set; }
        public string quotePrecision { get; set; }
        public string quoteAssetPrecision { get; set; }
        public string baseCommissionPrecision { get; set; }
        public string quoteCommissionPrecision { get; set; }
        public string[] orderTypes { get; set; }
        public string icebergAllowed { get; set; }
        public string ocoAllowed { get; set; }
        public string quoteOrderQtyMarketAllowed { get; set; }
        public string isSpotTradingAllowed { get; set; }
        public string isMarginTradingAllowed { get; set; }
        public string[] permissions { get; set; }
    }
}
