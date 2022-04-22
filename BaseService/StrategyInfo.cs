using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace FrameworkBaseService
{
    public class StrategyInfo
    {
        public string name = "";
        public Dictionary<string, string> paramDic = null;
        public StreamWriter writer = null;
        public string currentLogDate = "";
        public string basePath = "";
        public BaseStrategy strategy = null;
        public Thread thread = null;
        public ManualResetEvent mre = null;
        public Notification notification = null;
    }
}
