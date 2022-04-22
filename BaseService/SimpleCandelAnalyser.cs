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
    public class SimpleCandelAnalyser : BaseStrategy
    {
        public SimpleCandelAnalyser(Dictionary<string, string> parameters, string name, string basePath) : base(parameters, name, basePath)
        {

        }

        public override void Init()
        {
            ServiceInfo.LogStrategyAction(strategyInfo, "Initialize", strategyInfo.name + ", Initialized");
        }

        public override void DoJob()
        {
            string state = "BUY";
            decimal buyPrice = 0;
            decimal buyAmount = 0;
            decimal sellPrice = 0;
            const int quarterLoopCount = 4;
            int oneMinLoopCount = quarterLoopCount + 1;
            string result = "";
            decimal quarterNeg = 0;
            decimal quarterPos = 0;
            decimal positiveNegetiveRatioQuarter = 0;
            decimal oneMinNeg = 0;
            decimal oneMinPos = 0;
            decimal positiveNegetiveRatioOneMin = 0;
            bool reachOneThird = false;
            ServiceInfo.LogStrategyAction(strategyInfo, "Start Thread", strategyInfo.name + ", Started");
            while (true)
            {
                try
                {
                    //********************* BUY ********************
                    if (state == "BUY")
                    {
                        //-- Get Quarter Candels
                        if (oneMinLoopCount > quarterLoopCount)
                        {
                            result = BinanceAPI.GetJson("v3/klines", "symbol=" + strategyInfo.paramDic["PairCoins"] + "&" +
                                "interval=1m" + "&" + "limit=" + int.Parse(strategyInfo.paramDic["TimeWindowQuarter"]) * 2 * quarterLoopCount);
                            string[][] objQuarter = JsonConvert.DeserializeObject<string[][]>(result);
                            quarterNeg = 0;
                            quarterPos = 0;
                            //-- calculate ratio for quarter candels
                            foreach (string[] item in objQuarter)
                            {
                                decimal dif = decimal.Parse(item[4]) - decimal.Parse(item[1]);
                                if (dif > 0)
                                    quarterPos += dif;
                                if (dif < 0)
                                    quarterNeg += Math.Abs(dif);
                            }
                            positiveNegetiveRatioQuarter = quarterNeg / (quarterNeg + quarterPos);
                            oneMinLoopCount = 0;
                            ServiceInfo.LogStrategyAction(strategyInfo, "Info", strategyInfo.name + ", Next Quarter Loop, RatioQuarter = " + positiveNegetiveRatioQuarter);
                        }
                        //if (positiveNegetiveRatioQuarter >= decimal.Parse(strategyInfo.paramDic["PositiveNegetiveRatioQuarter"]) ||
                        //    positiveNegetiveRatioQuarter <= decimal.Parse(strategyInfo.paramDic["NegetivePositiveRatioQuarter"]))
                        if (positiveNegetiveRatioQuarter <= decimal.Parse(strategyInfo.paramDic["NegetivePositiveRatioQuarter"]))
                        {
                            //-- Get One Min Candels
                            result = BinanceAPI.GetJson("v3/klines", "symbol=" + strategyInfo.paramDic["PairCoins"] + "&" +
                                "interval=1m" + "&" + "limit=" + strategyInfo.paramDic["TimeWindowOneMin"]);
                            string[][] objOneMin = JsonConvert.DeserializeObject<string[][]>(result);
                            //-- calculate ratio for One Min candels
                            oneMinNeg = 0;
                            oneMinPos = 0;
                            decimal dif = 0;
                            foreach (string[] item in objOneMin)
                            {
                                dif = decimal.Parse(item[4]) - decimal.Parse(item[1]);
                                if (dif > 0)
                                    oneMinPos += dif;
                                if (dif < 0)
                                    oneMinNeg += Math.Abs(dif);
                            }
                            positiveNegetiveRatioOneMin = oneMinPos / (oneMinPos + oneMinNeg);
                            ServiceInfo.LogStrategyAction(strategyInfo, "Info", strategyInfo.name + ", Next One Min Loop, RatioOneMin = " + positiveNegetiveRatioOneMin);
                            //--Buy if both ratios are reached to target
                            if ((oneMinPos + oneMinNeg) > decimal.Parse(strategyInfo.paramDic["AccumulationIgnore"]) && 
                                dif > 0 &&
                                positiveNegetiveRatioOneMin >= decimal.Parse(strategyInfo.paramDic["PositiveNegetiveRatioOneMin"]))
                            {
                                result = BinanceAPI.GetJson("v3/ticker/price", "symbol=" + strategyInfo.paramDic["PairCoins"]);
                                Price priceObj = JsonConvert.DeserializeObject<Price>(result);
                                buyPrice = decimal.Parse(priceObj.price) +
                                    decimal.Parse(strategyInfo.paramDic["BuyGapAmount"]);
                                buyAmount = decimal.Parse(strategyInfo.paramDic["Amount"]) / buyPrice;
                                ///\/\/\/\/\/\/\/\/\ BUY API CALL IMPLEMENTED HERE
                                
                                MonitorRec rec = new MonitorRec();
                                rec.action = "Buy";
                                rec.amount = decimal.Parse(strategyInfo.paramDic["Amount"]);
                                rec.dateTime = DateTime.Now.ToString();
                                rec.side = "Ask";
                                AddMonitoringInfo("SET", rec);
                                ServiceInfo.LogStrategyAction(strategyInfo, "BUY", strategyInfo.name + ", Reach Buy condition, Price=" + buyPrice +
                                    ", PNQ=" + positiveNegetiveRatioQuarter + ", PNO=" + positiveNegetiveRatioOneMin);
                                state = "SELL";
                            }
                        }
                    }
                    //********************* SELL ********************
                    if (state == "SELL")
                    {
                        result = BinanceAPI.GetJson("v3/ticker/price", "symbol=" + strategyInfo.paramDic["PairCoins"]);
                        Price priceObj = JsonConvert.DeserializeObject<Price>(result);
                        sellPrice = decimal.Parse(priceObj.price) -
                                    decimal.Parse(strategyInfo.paramDic["SellGapAmount"]);
                        decimal currentLossProfit = (sellPrice * buyAmount) - (buyPrice * buyAmount);
                        ServiceInfo.LogStrategyAction(strategyInfo, "Info", strategyInfo.name + ", Sell Condition Current Loss Profit value=" + currentLossProfit);
                        if (currentLossProfit > (decimal.Parse("0.33") * decimal.Parse(strategyInfo.paramDic["ProfitLimit"])))
                            reachOneThird = true;
                        //-- Sell Save Profit
                        if (currentLossProfit >= decimal.Parse(strategyInfo.paramDic["ProfitLimit"]))
                        {
                            decimal LossProfit1 = currentLossProfit;
                            decimal LossProfit2 = currentLossProfit;
                            while (true)
                            {
                                decimal oneSecChange = LossProfit2 - LossProfit1;
                                ServiceInfo.LogStrategyAction(strategyInfo, "Info", strategyInfo.name + ", One Second Change in Ready State=" + oneSecChange);
                                if (oneSecChange < 0 && Math.Abs(oneSecChange) >= (decimal.Parse("0.1") * decimal.Parse(strategyInfo.paramDic["ProfitLimit"])))
                                {
                                    if (bool.Parse(strategyInfo.paramDic["JustLog"]) == false)
                                    {
                                        ///\/\/\/\/\/\/\/\/\ SELL HERE

                                        MonitorRec rec = new MonitorRec();
                                        rec.action = "Sell_Profit";
                                        rec.amount = currentLossProfit;
                                        rec.dateTime = DateTime.Now.ToString();
                                        rec.side = "Bid";
                                        AddMonitoringInfo("SET", rec);
                                    }
                                    ServiceInfo.LogStrategyAction(strategyInfo, "SELL", strategyInfo.name + ", Sell Save Profit, Price=" + sellPrice + ", Profit=" + currentLossProfit);
                                    state = "BUY";
                                    oneMinLoopCount = quarterLoopCount + 1;
                                    reachOneThird = false;
                                    ServiceInfo.LogStrategyAction(strategyInfo, "Info", strategyInfo.name + ", Wait After Profit=" + strategyInfo.paramDic["WaitAfterProfit"] + " Seconds");
                                    strategyInfo.mre.WaitOne(int.Parse(strategyInfo.paramDic["WaitAfterProfit"]) * 1000);
                                    strategyInfo.mre.Reset();
                                    break;
                                }
                                strategyInfo.mre.WaitOne(500);
                                strategyInfo.mre.Reset();
                                LossProfit1 = LossProfit2;
                                result = BinanceAPI.GetJson("v3/ticker/price", "symbol=" + strategyInfo.paramDic["PairCoins"]);
                                priceObj = JsonConvert.DeserializeObject<Price>(result);
                                sellPrice = decimal.Parse(priceObj.price) -
                                    decimal.Parse(strategyInfo.paramDic["SellGapAmount"]);
                                currentLossProfit = (sellPrice * buyAmount) - (buyPrice * buyAmount);
                                LossProfit2 = currentLossProfit;
                            }
                        }
                        //-- One Third Rule
                        else if (reachOneThird && currentLossProfit < (decimal.Parse("0.33") * decimal.Parse(strategyInfo.paramDic["ProfitLimit"])))
                        {
                            if (bool.Parse(strategyInfo.paramDic["JustLog"]) == false)
                            {
                                ///\/\/\/\/\/\/\/\/\ SELL HERE
                                
                                MonitorRec rec = new MonitorRec();
                                rec.action = "Sell_OneThird";
                                rec.amount = currentLossProfit;
                                rec.dateTime = DateTime.Now.ToString();
                                rec.side = "Bid";
                                AddMonitoringInfo("SET", rec);
                            }
                            ServiceInfo.LogStrategyAction(strategyInfo, "SELL", strategyInfo.name + ", Sell One Third Rule, Price=" + sellPrice + ", One Third Rule=" + currentLossProfit);
                            state = "BUY";
                            oneMinLoopCount = quarterLoopCount + 1;
                            reachOneThird = false;
                            ServiceInfo.LogStrategyAction(strategyInfo, "Info", strategyInfo.name + ", Wait After One Third Rule=" + strategyInfo.paramDic["WaitAfterLoss"] + " Seconds");
                            strategyInfo.mre.WaitOne(int.Parse(strategyInfo.paramDic["WaitAfterLoss"]) * 1000);
                            strategyInfo.mre.Reset();
                        }
                        //-- Sell Prevent Loss
                        else if (currentLossProfit < 0 && Math.Abs(currentLossProfit) >= decimal.Parse(strategyInfo.paramDic["LossLimit"]))
                        {
                            if (bool.Parse(strategyInfo.paramDic["JustLog"]) == false)
                            {
                                ///\/\/\/\/\/\/\/\/\ SELL HERE

                                MonitorRec rec = new MonitorRec();
                                rec.action = "Sell_Loss";
                                rec.amount = currentLossProfit;
                                rec.dateTime = DateTime.Now.ToString();
                                rec.side = "Bid";
                                AddMonitoringInfo("SET", rec);
                            }
                            ServiceInfo.LogStrategyAction(strategyInfo, "SELL", strategyInfo.name + ", Sell Prevent Loss, Price=" + sellPrice + ", Loss=" + currentLossProfit);
                            state = "BUY";
                            oneMinLoopCount = quarterLoopCount + 1;
                            reachOneThird = false;
                            ServiceInfo.LogStrategyAction(strategyInfo, "Info", strategyInfo.name + ", Wait After Loss=" + strategyInfo.paramDic["WaitAfterLoss"] + " Seconds");
                            strategyInfo.mre.WaitOne(int.Parse(strategyInfo.paramDic["WaitAfterLoss"]) * 1000);
                            strategyInfo.mre.Reset();
                        }
                    }
                    //-- Wait for next loop
                    ServiceInfo.LogStrategyAction(strategyInfo, "Info", strategyInfo.name + ", Next One Min Loop");
                    if (state == "BUY")
                        strategyInfo.mre.WaitOne(int.Parse(strategyInfo.paramDic["ScheduleTime"]) * 1000);
                    else if(state == "SELL")
                        strategyInfo.mre.WaitOne((int.Parse(strategyInfo.paramDic["ScheduleTime"])/2) * 1000);
                    strategyInfo.mre.Reset();
                    oneMinLoopCount++;
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
            parameters.Add("PairCoins");
            parameters.Add("Amount");
            parameters.Add("FeeRate");
            parameters.Add("BuyGapAmount");
            parameters.Add("SellGapAmount");
            parameters.Add("ScheduleTime");
            parameters.Add("TimeWindowOneMin");
            parameters.Add("TimeWindowQuarter");
            parameters.Add("LossLimit");
            parameters.Add("WaitAfterLoss");
            parameters.Add("ProfitLimit");
            parameters.Add("WaitAfterProfit");
            parameters.Add("PositiveNegetiveRatioQuarter");
            parameters.Add("NegetivePositiveRatioQuarter");
            parameters.Add("PositiveNegetiveRatioOneMin");
            parameters.Add("AccumulationIgnore");
            parameters.Add("Key");
            parameters.Add("Secret");
            return (parameters);
        }

    }
}
