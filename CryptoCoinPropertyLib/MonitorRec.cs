using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCoinPropertyLib
{
    public class MonitorRec
    {
        public string dateTime { get; set; }
        public string side { get; set; }
        public string action { get; set; }
        public decimal amount { get; set; }
    }
}
