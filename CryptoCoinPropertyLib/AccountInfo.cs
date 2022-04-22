using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCoinPropertyLib
{
    public class AccountInfo
    {
        public string makerCommission { get; set; }
        public string takerCommission { get; set; }
        public string buyerCommission { get; set; }
        public string sellerCommission { get; set; }
        public string canTrade { get; set; }
        public string canWithdraw { get; set; }
        public string canDeposit { get; set; }
        public string updateTime { get; set; }
        public string accountType { get; set; }
        public List<Balances> balances { get; set; }
    }
}
