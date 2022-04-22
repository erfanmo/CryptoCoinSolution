using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CryptoCoinPropertyLib;
using Newtonsoft.Json;

namespace FrameworkBaseService
{
    public abstract class BaseStrategy
    {

        public StrategyInfo strategyInfo = null;
        public MonitorObj monitorObj = null;

        public BaseStrategy(Dictionary<string, string> parameters, 
            string name,
            string basePath)
        {
            try
            {
                StrategyInfo info = ServiceInfo.StrategyDic[name];
                throw new Exception("Strategy with the same name already exists");
            }
            catch
            {}
            strategyInfo = new StrategyInfo();
            strategyInfo.name = name;
            strategyInfo.paramDic = parameters;
            strategyInfo.basePath = basePath;
            strategyInfo.currentLogDate = "";
            strategyInfo.mre = new System.Threading.ManualResetEvent(false);
            strategyInfo.strategy = this;
            strategyInfo.thread = new System.Threading.Thread(this.DoJob);
            strategyInfo.thread.IsBackground = true;
            ServiceInfo.StrategyDic.Add(name, strategyInfo);
            monitorObj = new MonitorObj();
            monitorObj.name = name;
            monitorObj.count = 0;
            Init();
        }

        public abstract void Init();

        public abstract void DoJob();

        public abstract List<string> GetParams();

        public string AddMonitoringInfo(string action, MonitorRec rec)
        {
            string result = "";
            lock (this)
            {
                switch (action)
                {
                    case "GET":
                        result = JsonConvert.SerializeObject(monitorObj);
                        break;
                    case "SET":
                        if (rec.action == "Buy")
                            monitorObj.count++;
                        monitorObj.records.Add(rec);
                        if (rec.amount > 0)
                            monitorObj.totalProfit += rec.amount;
                        else
                            monitorObj.totalLoss += rec.amount;
                        break;
                    default:
                        break;
                }
            }
            return (result);
        }

        public void Start()
        {
            strategyInfo.thread.Start();
        }

        public void Terminate()
        {
            strategyInfo.thread.Abort();
            ServiceInfo.StatisticsDic.Remove(strategyInfo.name);
        }

    }
}
