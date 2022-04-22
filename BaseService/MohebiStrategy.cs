using BinanceAPICall;
using CryptoCoinPropertyLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkBaseService
{
    public class MohebiStrategy : BaseStrategy
    {

        public MohebiStrategy(Dictionary<string, string> parameters, string name, string basePath) : base(parameters, name, basePath)
        {

        }

        public override void DoJob()
        {
            string result = "";
            while (true)
            {
                try
                {
                    //-- Get Current Price
                    result = BinanceAPI.GetJson("v3/ticker/price", "symbol=" + strategyInfo.paramDic["PairCoins"]);
                    Price priceObj = JsonConvert.DeserializeObject<Price>(result);
                    ServiceInfo.LogStrategyAction(strategyInfo, "Info", strategyInfo.name + ", Current Price for " +
                        strategyInfo.paramDic["PairCoins"] + "=" + priceObj.price);

                    //-- Wait
                    strategyInfo.mre.WaitOne(int.Parse(strategyInfo.paramDic["ScheduleTime"]) * 1000);
                    strategyInfo.mre.Reset();
                }
                catch (Exception ex)
                {
                    ServiceInfo.LogStrategyAction(strategyInfo, "Error", strategyInfo.name + ", " + ex.Message);
                    strategyInfo.mre.WaitOne(int.Parse(strategyInfo.paramDic["ScheduleTime"]) * 1000);
                    strategyInfo.mre.Reset();
                }
            }
        }
        public override List<string> GetParams()
        {
            List<string> parameters = new List<string>();
            parameters.Add("PairCoin");
            parameters.Add("ScheduleTime");
            parameters.Add("Key");
            parameters.Add("Secret");
            return (parameters);
        }

        public override void Init()
        {
            ServiceInfo.LogStrategyAction(strategyInfo, "Initialize", strategyInfo.name + ", Initialized");
        }

    }
}
