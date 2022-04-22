using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCoinPropertyLib
{
    public class CancelOrder
    {
        public string symbol { get; set; }
        public string origClientOrderId { get; set; }
        public string orderId { get; set; }
        public string orderListId { get; set; }
        public string clientOrderId { get; set; }
        public string price { get; set; }
        public string origQty { get; set; }
        public string executedQty { get; set; }
        public string cummulativeQuoteQty { get; set; }
        public string status { get; set; }
        public string timeInForce { get; set; }
        public string type { get; set; }
        public string side { get; set; }
    }
}
