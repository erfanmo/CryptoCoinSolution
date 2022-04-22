using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCoinPropertyLib
{
    public class WatchOrderObj
    {
        public string OrderNo { get; set; }
        public string Amount { get; set; }
        public string Price { get; set; }
        public string Status { get; set; }
        public OrderStatus BuyStatusObj { get; set; }
        public OrderStatus SellStatusObj { get; set; }
        public string LossProfit { get; set; }
    }
}
