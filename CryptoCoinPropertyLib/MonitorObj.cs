using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCoinPropertyLib
{
    public class MonitorObj
    {
        public string name { get; set; }
        public int count { get; set; }
        public decimal totalProfit { get; set; }
        public decimal totalLoss { get; set; }

        public List<MonitorRec> records = new List<MonitorRec>();
    }
}
