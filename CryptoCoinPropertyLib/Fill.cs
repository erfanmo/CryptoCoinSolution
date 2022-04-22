using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCoinPropertyLib
{
    public class Fill
    {
        public string price { get; set; }
        public string qty { get; set; }
        public string commission { get; set; }
        public string commissionAsset { get; set; }
        public string tradeId { get; set; }
    }
}
