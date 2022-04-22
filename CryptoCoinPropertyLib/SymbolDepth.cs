using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCoinPropertyLib
{
    public class SymbolDepth
    {
        public string lastUpdateId { get; set; }
        public string[,] bids { get; set; }
        public string[,] asks { get; set; }
    }
}
